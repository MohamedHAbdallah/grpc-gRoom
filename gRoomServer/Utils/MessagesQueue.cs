using Protos.FlashBot;
using Protos.Monitoring;

using Google.Protobuf.WellKnownTypes;

namespace gRoom.gRPC.Utils;

public class MessagesQueue
{
    private static Queue<ReceivedMessageDef> _queue;

    static MessagesQueue()
    {
        _queue = new Queue<ReceivedMessageDef>();
    }

    public static void AddNewsToQueue(NewsFlashMsgDef news)
    {
        var msg = new ReceivedMessageDef();
        msg.Contents = news.NewsItem;
        msg.User = "NewsBot";
        msg.MsgTime = Timestamp.FromDateTime(DateTime.UtcNow);
        _queue.Enqueue(msg);
    }

    public static ReceivedMessageDef GetNextMessage()
    {
        return _queue.Dequeue();
    }

    public static int GetMessagesCount()
    {
        return _queue.Count;
    }

}