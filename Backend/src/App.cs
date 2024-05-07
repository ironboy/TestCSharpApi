// Global settings
using Xunit.Abstractions;

Globals = Obj(new
{
    debugOn = false,
    detailedAclDebug = false,
    aclOn = true,
    isSpa = true,
    port = 3001,
    serverName = "Benny's Minimal API Server",
    frontendPath = Path.Combine("..", "Frontend"),
    sessionLifeTimeHours = 2
});

//Server.Start();
