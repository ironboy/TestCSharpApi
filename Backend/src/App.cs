// Global settings
using System.Net;

Globals = Obj(new
{
    debugOn = true,
    detailedAclDebug = false,
    aclOn = true,
    isSpa = true,
    port = 3001,
    serverName = "Ironboy's Minimal API Server",
    frontendPath = FilePath("..", "Frontend"),
    sessionLifeTimeHours = 2
});

Server.Start();

// WebApp.Utils.CreateMockUsers();

// bWebApp.Utils.CountDomainsFromUserEmails();

// WebApp.Utils.DeleteMockUsers();
