using Grpc.Core;
using gRoom.grpc.Messages;

namespace gRoom.grpc.Services;

public class GRoomService : GRoom.GRoomBase
{
    private readonly ILogger<GRoomService> _logger;
    public GRoomService(ILogger<GRoomService> logger)
    {
        _logger = logger;
    }
    public override Task<RoomRegistrationResponse> RegisterationToRoom(RoomRegistrationRequest request, ServerCallContext context)
    {
        _logger.LogInformation("Service Called...");
        var rnd = new Random();
        var roomNum = rnd.Next(1, 100);
        _logger.LogInformation($"Room no. {roomNum}");
        var res = new RoomRegistrationResponse { RoomId = roomNum };
        return Task.FromResult(res);
    }

    public override async Task<NewsStreamStatus> SendNewsFlash(IAsyncStreamReader<NewsFlash> newsStream, ServerCallContext context)
    {
        while (await newsStream.MoveNext())
        {
            var news = newsStream.Current;
            Console.WriteLine($"News Flash : {news.NewsItem}");
        }
        return new NewsStreamStatus { Success = true };
    }
}