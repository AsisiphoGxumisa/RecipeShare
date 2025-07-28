namespace RecipeShare.Services
{
    public class ServiceResult<T>
    {
        public bool IsSuccess { get; set; }
        public T? Data { get; set; }
        public string? ErrorMessage { get; set; }

        public static ServiceResult<T> Success(T data) => new() { IsSuccess = true, Data = data };
        public static ServiceResult<T> Fail(string error) => new() { IsSuccess = false, ErrorMessage = error };
    }

}
