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
    
    public abstract class BaseStorage<TModel> : IStorage<TModel>
         where TModel : class
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
            list.Add(model);

            await Save(list);
        }

        public TModel Get(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TModel>> GetAll()
        {
            var json = await jsRuntime.InvokeAsync<string>("get", key);

            if (!string.IsNullOrEmpty(json))
            {
                var models = JsonSerializer.Deserialize<IEnumerable<TModel>>(json);

                return models;
            }

            return new List<TModel>();
        }

        public abstract Task Remove(string id);

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
