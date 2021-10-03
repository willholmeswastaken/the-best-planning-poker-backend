using System.Collections.Generic;
using System.Threading.Tasks;

namespace TBPP.API.Hubs
{
    public interface IPokerRoomHubHandler
    {
        Task AddUser(string sessionId, string userName);

        Task AddUsers(string connectionId, IEnumerable<string> userNames);
    }
}
