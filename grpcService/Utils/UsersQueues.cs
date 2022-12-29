using Protos.Monitoring;
using Google.Protobuf.WellKnownTypes;

namespace gRoom.gRPC.Utils;

public class UsersQueues
{
    private static List<UserQueue> _queues;
    private static Queue<ReceivedMessageDef> _adminQueue;

    static UsersQueues()
    {
        _queues = new List<UserQueue>();
        _adminQueue = new Queue<ReceivedMessageDef>();
    }

    public static void CreateUserQueue(String room, String user)
    {
        _queues.Add(new UserQueue(room, user));
    }

    public static void AddMessageToRoom(ReceivedMessageDef msg, string room)
    {
        // Add message only to users in this room 
        foreach (var queue in _queues.Where(q => q.Room == room))
        {
            queue.AddMessageToQueue(msg);
        }
        _adminQueue.Enqueue(msg);
    }

    public static ReceivedMessageDef? GetMessageForUser(string user)
    {
        var userQueue = _queues.Where(q => q.User == user).First();
        if (userQueue.GetMessagesCount() > 0)
        {
            return userQueue.GetNextMessage();
        }
        else
        {
            return null;
        }
    }

    public static int GetAdminQueueMessageCount()
    {
        return _adminQueue.Count;
    }

    public static ReceivedMessageDef GetNextAdminMessage()
    {
        return _adminQueue.Dequeue();
    }
}

class UserQueue
{
    private Queue<ReceivedMessageDef> queue { get; }
    public string Room { get; }
    public string User { get; }

    public UserQueue(string room, string user)
    {
        Room = room;
        User = user;
        this.queue = new Queue<ReceivedMessageDef>();
    }

    public void AddMessageToQueue(ReceivedMessageDef msg)
    {
        this.queue.Enqueue(msg);
    }

    public ReceivedMessageDef GetNextMessage()
    {
        return this.queue.Dequeue();
    }

    public int GetMessagesCount()
    {
        return this.queue.Count;
    }
}