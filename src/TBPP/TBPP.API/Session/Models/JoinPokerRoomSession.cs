namespace TBPP.API.Session.Models
{
    public class JoinPokerRoomSession : IPokerRoomSessionMessage
    {
        public JoinPokerRoomSession(string instanceId, string connectionId, string userId)
        {
            InstanceId = instanceId;
            ConnectionId = connectionId;
            UserId = userId;
        }

        public string InstanceId { get; set; }

        public string ConnectionId { get; set; }

        public string UserId { get; set; }
    }
}
