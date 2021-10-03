using TBPP.API.Session.Models;

namespace TBPP.API.Session
{
    public interface IPokerRoomSessionHandler
    {
        void Handle(IPokerRoomSessionMessage message);
    }
}
