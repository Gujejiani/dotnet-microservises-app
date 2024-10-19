


using System.Text.Json;
using StackExchange.Redis;

class RedisService: IRedisService   {
    private readonly IDatabase _db;

    private readonly IConnectionMultiplexer redis;
    
    public RedisService( IConnectionMultiplexer connectionMultiplexer) {
        _db =  connectionMultiplexer.GetDatabase();
        redis = connectionMultiplexer;
    }


    

     public async Task<bool> SetDataAsync<T>(string key, T data)
    {
        // Serialize the object to JSON
        var jsonData = JsonSerializer.Serialize(data);

        // Store JSON string in Redis
       var stored =  await _db.StringSetAsync(key, jsonData);

        Console.WriteLine($"Data stored in Redis with key: {key}");

        if(stored){
            PublishUpdate($"Data stored in Redis with key: {key}");
         return true;
        }

        return false;

        
      
    }

    public async Task<string> ReceiveDataAsync(string key)
    {
        // Simulate data reception
        await Task.Delay(500); // Simulating a delay

        // Replace with actual data you receive
        var receivedData = _db.StringGet(key);

        // Store the received data in Redis
        await SetDataAsync("receivedDataKey", receivedData);

        // Return the received data
        return receivedData.HasValue ? receivedData.ToString() : string.Empty;
    }

    public void PublishUpdate(string message)
    {
        var pubsub = redis.GetSubscriber();
        pubsub.Publish("db-updates", message);
    }

}