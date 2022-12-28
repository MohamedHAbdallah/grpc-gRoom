const grpc = require("@grpc/grpc-js");
const protoLoader = require("@grpc/proto-loader");
const PROTO_PATH = "./Protos/FlashBot/flashBot.proto";
const options = {
  keepCase: true,
  longs: String,
  enums: String,
  defaults: true,
  oneofs: true,
};

// The news item to broadcast
const newsItems = [
  "Hello i am Jon Steve",
  "I Love Programming",
  "I Intersting about open source tech",
  "my dream Learning grpc",
  "love Liverpool club",
  "my favorite player is xavi",
];

// Loading the protobuf contents
var grpcObj = protoLoader.loadSync(PROTO_PATH, options);
const FlashBotServiceDef =
  grpc.loadPackageDefinition(grpcObj).Protos.FlashBot.FlashBotServiceDef;

// Creating the service client
const clientStub = new FlashBotServiceDef(
  "localhost:5162",
  grpc.credentials.createInsecure()
);

// Creating the call to the service - not executing yet!
var call = clientStub.sendNewsFlash(function (error, newsStatus) {
  if (error) {
    console.error(error);
  }
  console.log(`Stream success ${newsStatus.success}`);
  console.log(newsStatus.message);
});

var itemsCount = 0;

console.log("start sending news Flash ... ");
// Calling the  service in 1sec intervals
var intervalId = setInterval(() => {
  var itemIndex = Math.floor(Math.random() * 5);
  call.write({ news_item: `message from client : ${newsItems[itemIndex]}` });
  itemsCount++;
  // After 10 calls - end the Stream
  if (itemsCount == 10) {
    clearInterval(intervalId);
    call.end();
  }
}, 1000);
