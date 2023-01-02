const {
  RoomRegistrationRequestMsgDef,
  RoomRegistrationResponseMsgDef,
} = require("./room-registeration.messages_pb.js");
const {
  RoomRegistrationServiceDefClient,
} = require("./room-registeration.services_grpc_web_pb.js");

var client = new RoomRegistrationServiceDefClient("http://localhost:5162");

var request = new RoomRegistrationRequestMsgDef();
request.setRoomName("Cars");
request.setUserName("Memi");

client.registerationToRoom(request, {}, (err, response) => {
  console.log(response.getJoined());
  alert("Joined: " + response.getJoined());
});
