namespace TBPP.API.Session.Models
{
    public class CreatePokerRoomSession : IPokerRoomSessionMessage
    {
        public CreatePokerRoomSession(string instanceId, string name, string currentTopic, bool isLeaderOnlyAllowedToShow)
        {
            InstanceId = instanceId;
            Name = name;
            CurrentTopic = currentTopic;
            IsLeaderOnlyAllowedToShow = isLeaderOnlyAllowedToShow;
        }

        public string InstanceId { get; set; }

        public string Name { get; set; }

        public string CurrentTopic { get; set; }

        public bool IsLeaderOnlyAllowedToShow { get; set; }
    }
}
