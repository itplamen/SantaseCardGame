namespace SantaseCardGame.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.Json;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Configuration;
    using Microsoft.JSInterop;
    
    using SantaseCardGame.Data.Contracts;
    using SantaseCardGame.Data.Models.Contracts;

    public abstract class BaseStorage<TModel> : IStorage<TModel>
         where TModel : ISavable
    {
        private readonly string key;
        private readonly IJSRuntime jsRuntime;

        protected BaseStorage(IJSRuntime jsRuntime, IConfiguration configuration, string storageKey)
        {
            this.jsRuntime = jsRuntime;
            this.key = configuration[storageKey];
        }

        public async Task Add(TModel model)
        {
            var models = await GetAll();
            var list = models.ToList();
            var modelToRemove = list.FirstOrDefault(x => x.Id == model.Id);

            if (modelToRemove != null)
            {
                model.IsSaved = true;
                list.Remove(modelToRemove);
            }

            list.Add(model);

            await Save(list);
        }

        public TModel Get(string id)
        {
            var modes = GetAll().ConfigureAwait(false).GetAwaiter().GetResult();

            return modes.FirstOrDefault(x =>x.Id == id);
        }

        public async Task<IEnumerable<TModel>> GetAll(Func<TModel, bool> predicate = null)
        {
            var json = await jsRuntime.InvokeAsync<string>("get", key);

            if (!string.IsNullOrEmpty(json))
            {
                var models = JsonSerializer.Deserialize<IEnumerable<TModel>>(json);

                if (predicate != null)
                {
                    return models.Where(predicate);
                }

                return models;
            }

            return new List<TModel>();
        }

        public async Task Remove(string id, bool removePermanentlySaved)
        {
            var models = await GetAll();
            var list = models.ToList();
            var model = list.FirstOrDefault(x => x.Id == id);

            if (model != null && model.IsSaved == removePermanentlySaved)
            {
                list.Remove(model);

                await Save(list);
            }
        }

        protected async Task Save(IEnumerable<TModel> models)
        {
            using (var stream = new MemoryStream())
            {
                await JsonSerializer.SerializeAsync(stream, models);
                stream.Position = 0;

                using var reader = new StreamReader(stream);
                string json = await reader.ReadToEndAsync();

                await jsRuntime.InvokeVoidAsync("save", key, json);
            }
        }
    }
}
