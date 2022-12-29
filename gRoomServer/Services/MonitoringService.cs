using Grpc.Core;
using Protos.Monitoring;
using gRoom.gRPC.Utils;
using Google.Protobuf.WellKnownTypes;

namespace gRoom.grpc.Services;

public class MonitoringService : MonitoringServiceDef.MonitoringServiceDefBase
{
    private readonly ILogger<MonitoringService> _logger;
    public MonitoringService(ILogger<MonitoringService> logger)
    {
        _logger = logger;
    }

    public override async Task StartMonitoring(Empty request, IServerStreamWriter<ReceivedMessageDef> streamWriter, ServerCallContext context)
    {
        Console.WriteLine("strat sending steaming from Server....");
        while (true)
        {
            //await streamWriter.WriteAsync(new RecievedMessageDef { MsgTime = Timestamp.FromDateTime(DateTime.UtcNow), User = "1", Contents = "Test msg" });
            if (MessagesQueue.GetMessagesCount() > 0)
            {
                await streamWriter.WriteAsync(MessagesQueue.GetNextMessage());
            }
            if (UsersQueues.GetAdminQueueMessageCount() > 0)
            {
                await streamWriter.WriteAsync(UsersQueues.GetNextAdminMessage());
            }
            await Task.Delay(1000);
        }
    }
}