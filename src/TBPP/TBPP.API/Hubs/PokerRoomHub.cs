using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using TBPP.API.Session;
using TBPP.API.Session.Models;

namespace TBPP.API.Hubs
{
    public class PokerRoomHub : Hub
    {
        private readonly ILogger<PokerRoomHub> _logger;
        private readonly IPokerRoomSessionHandler _sessionHandler;

        public PokerRoomHub(ILogger<PokerRoomHub> logger, IPokerRoomSessionHandler sessionHandler)
        {
            _logger = logger;
            _sessionHandler = sessionHandler;
        }

        public async Task JoinSession(string sessionId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, sessionId);

            // some handler to retrieve state
            _sessionHandler.Handle(new JoinPokerRoomSession(sessionId, Context.ConnectionId, Context.UserIdentifier ?? "Unknown User"));
        }

        public async Task CreateSession(string sessionId, string name, string currentTopic, bool isLeaderOnlyAllowedToShow)
        {
            _sessionHandler.Handle(new CreatePokerRoomSession(sessionId, name, currentTopic, isLeaderOnlyAllowedToShow));

            await JoinSession(sessionId);
        }

        public override Task OnConnectedAsync()
        {
            _logger.LogInformation("Received connection from [{0}->{1}]", Context.ConnectionId, Context.UserIdentifier);
            return base.OnConnectedAsync();
        }
    }
}
