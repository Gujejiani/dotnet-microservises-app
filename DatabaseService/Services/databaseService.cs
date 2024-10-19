using StackExchange.Redis;

namespace databaseService.Services;

class DatabaseService: IDatabaseService {
    private readonly IConnectionMultiplexer _redis;

    public DatabaseService(IConnectionMultiplexer redis)
    {
        _redis = redis;
       
        Console.WriteLine("Database service started");
    }

    public void SubscribeToUpdates()
    {
        Console.WriteLine("Waiting FOR MESSAGES FROM REDIS MICROSERVICE !!! !!! !!! !!! !!! !!!");
        var sub = _redis.GetSubscriber();
        sub.Subscribe("db-updates", (channel, message) =>
        {
            Console.WriteLine($"Received update: {message}");
            // Handle the database update here
        });
    }
}
