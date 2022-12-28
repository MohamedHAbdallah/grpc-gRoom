using Grpc.Core;
using Protos.FlashBot;
using gRoom.gRPC.Utils;

namespace gRoom.grpc.Services;

public class FlashBotService : FlashBotServiceDef.FlashBotServiceDefBase
{
    private readonly ILogger<FlashBotService> _logger;
    public FlashBotService(ILogger<FlashBotService> logger)
    {
        _logger = logger;
    }

    public override async Task<NewsStreamResponseMsgDef> SendNewsFlash(IAsyncStreamReader<NewsFlashMsgDef> newsStream, ServerCallContext context)
    {
        Console.WriteLine("strat recieve steaming from FlashBot....");
        while (await newsStream.MoveNext())
        {
            var news = newsStream.Current;
            MessagesQueue.AddNewsToQueue(news);
            Console.WriteLine(news.NewsItem);
        }
        Console.WriteLine("end recieve steaming from FlashBot.");
        return new NewsStreamResponseMsgDef { Success = true, Message = "Message from Server : client streaming channel ends" };
    }
}