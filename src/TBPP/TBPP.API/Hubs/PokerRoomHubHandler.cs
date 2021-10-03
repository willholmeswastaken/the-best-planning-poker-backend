using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TBPP.API.Hubs
{
    public class PokerRoomHubHandler : IPokerRoomHubHandler
    {
        private readonly IHubContext<PokerRoomHub> _pokerRoomHub;

        public PokerRoomHubHandler(IHubContext<PokerRoomHub> pokerRoomHub)
        {
            _pokerRoomHub = pokerRoomHub;
        }

        public async Task AddUser(string sessionId, string userName)
        {
            await _pokerRoomHub.Clients.Group(sessionId).SendAsync("AddUser", userName).ConfigureAwait(false);
        }

        public async Task AddUsers(string connectionId, IEnumerable<string> userNames)
        {
            foreach(var user in userNames)
            {
                await _pokerRoomHub.Clients.Client(connectionId).SendAsync("AddUser", user).ConfigureAwait(false);
            }
        }
    }
}
