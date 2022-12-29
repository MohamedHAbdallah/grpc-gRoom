using Grpc.Core;
using Protos.Chat;
using Protos.Monitoring;
using gRoom.gRPC.Utils;

namespace gRoom.grpc.Services;

public class ChatService : ChatServiceDef.ChatServiceDefBase
{
    private readonly ILogger<ChatService> _logger;
    public ChatService(ILogger<ChatService> logger)
    {
        _logger = logger;
    }

    public override async Task StartChat(IAsyncStreamReader<ChatMessageDef> incomingStream, IServerStreamWriter<ChatMessageDef> outgoingStream, ServerCallContext context)
    {

        // Wait for the first message to get the user name
        while (!await incomingStream.MoveNext())
        {
            await Task.Delay(100);
        }

        string userName = incomingStream.Current.User;
        string room = incomingStream.Current.Room;
        Console.WriteLine($"User {userName} connected to room {incomingStream.Current.Room}");

        // TEST TEST TEST TEST - TO USE ONLY WHEN TESTING WITH BLOOMRPC
        //UsersQueues.CreateUserQueue(room, userName);
        // END TEST END TEST END TEST

        // Get messages from the user
        var reqTask = Task.Run(async () =>
        {
            while (await incomingStream.MoveNext())
            {
                Console.WriteLine($"Message received: {incomingStream.Current.Contents}");
                UsersQueues.AddMessageToRoom(ConvertToReceivedMessage(incomingStream.Current), incomingStream.Current.Room);
            }
        });


        // Check for messages to send to the user
        var respTask = Task.Run(async () =>
        {
            while (true)
            {
                var userMsg = UsersQueues.GetMessageForUser(userName);
                if (userMsg != null)
                {
                    var userMessage = ConvertToChatMessage(userMsg, room);
                    await outgoingStream.WriteAsync(userMessage);
                }
                if (MessagesQueue.GetMessagesCount() > 0)
                {
                    var news = MessagesQueue.GetNextMessage();
                    var newsMessage = ConvertToChatMessage(news, room);
                    await outgoingStream.WriteAsync(newsMessage);
                }

                await Task.Delay(200);
            }
        });

        // Keep the method running
        while (true)
        {
            await Task.Delay(10000);
        }
    }


    private ReceivedMessageDef ConvertToReceivedMessage(ChatMessageDef chatMsg)
    {
        var rcMsg = new ReceivedMessageDef();
        rcMsg.Contents = chatMsg.Contents;
        rcMsg.MsgTime = chatMsg.MsgTime;
        rcMsg.User = chatMsg.User;

        return rcMsg;
    }

    private ChatMessageDef ConvertToChatMessage(ReceivedMessageDef rcMsg, string room)
    {
        var chatMsg = new ChatMessageDef();
        chatMsg.Contents = rcMsg.Contents;
        chatMsg.MsgTime = rcMsg.MsgTime;
        chatMsg.User = rcMsg.User;
        chatMsg.Room = room;

        return chatMsg;
    }


}