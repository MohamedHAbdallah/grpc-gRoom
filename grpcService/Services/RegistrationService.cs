using gRoom.gRPC.Utils;
using Grpc.Core;
using Protos.RoomRegistration;

namespace gRoom.grpc.Services;

public class RoomRegistrationService : RoomRegistrationServiceDef.RoomRegistrationServiceDefBase
{
    private readonly ILogger<RoomRegistrationService> _logger;
    public RoomRegistrationService(ILogger<RoomRegistrationService> logger)
    {
        _logger = logger;
    }
    public override Task<RoomRegistrationResponseMsgDef> RegisterationToRoom(RoomRegistrationRequestMsgDef request, ServerCallContext context)
    {
        UsersQueues.CreateUserQueue(request.RoomName, request.UserName);
        var res = new RoomRegistrationResponseMsgDef { Joined = true };
        return Task.FromResult(res);
    }
}