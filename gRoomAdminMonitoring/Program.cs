using Grpc.Net.Client;
using Protos.Monitoring;
using Google.Protobuf.WellKnownTypes;

using var channel = GrpcChannel.ForAddress("http://localhost:5162");
var client = new MonitoringServiceDef.MonitoringServiceDefClient(channel);

Console.WriteLine("**** Admin Console Started ****");
Console.WriteLine("Listening ....");

// ADD THE MONITORING CODE BELOW THIS LINE

using var call = client.StartMonitoring(new Empty());
var cts = new CancellationTokenSource();

while (await call.ResponseStream.MoveNext(cts.Token))
{
    var msg = call.ResponseStream.Current;
    Console.WriteLine($"Monitoring: {msg.Contents}, user: {msg.User}, at: {msg.MsgTime}");
}