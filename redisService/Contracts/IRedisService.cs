public interface IRedisService
{
    public  Task<bool> SetDataAsync<T>(string key, T data);
    public  Task<string> ReceiveDataAsync(string key);
}