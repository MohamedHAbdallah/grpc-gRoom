using Grpc.Net.Client;
using gRoom.grpc.Messages;

using var channel = GrpcChannel.ForAddress("http://localhost:5162");

var client = new GRoom.GRoomClient(channel);

Console.Write("Enter Tour Room Name to Register : ");
var roomName = Console.ReadLine();
var req = new RoomRegistrationRequest { RoomName = roomName };
var res = client.RegisterationToRoom(req);
Console.WriteLine($"Room Id : {res.RoomId}");

Console.Read();