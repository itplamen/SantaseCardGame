namespace SantaseCardGame.Data.Models
{
    public class PlayerActionResult
    {
        public PlayerActionResult(bool isSuccess)
            : this(isSuccess, null)
        {
        }

        public PlayerActionResult(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }

        public bool IsSuccess { get; set; }

        public string Message { get; set; }
    }
}
