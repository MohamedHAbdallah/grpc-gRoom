using Grpc.Net.Client;
using Protos.RoomRegistration;

using var channel = GrpcChannel.ForAddress("http://localhost:5162");

var client = new RoomRegistrationServiceDef.RoomRegistrationServiceDefClient(channel);

Console.Write("Enter Tour Room Name to Register : ");
var roomName = Console.ReadLine();
var req = new RoomRegistrationRequestMsgDef { RoomName = roomName };
var res = client.RegisterationToRoom(req);
Console.WriteLine($"Room Id : {res.RoomId}");

Console.Read();