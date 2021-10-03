using Akka.Actor;
using Akka.Event;
using System;
using System.Collections.Generic;
using TBPP.API.Hubs;
using TBPP.API.Session.Models;

namespace TBPP.API.Actors
{
    public class PokerRoomInstanceActor : ReceiveActor
    {
        private readonly ILoggingAdapter _logger = Context.GetLogger();

        private readonly IPokerRoomHubHandler _hubHandler;
        private readonly string _sessionId;
        private readonly TimeSpan _idleTimeout = TimeSpan.FromMinutes(20);
        private readonly List<string> _users = new();
        
        public PokerRoomInstanceActor(string sessionId, IPokerRoomHubHandler hubHandler)
        {
            _sessionId = sessionId;
            _hubHandler = hubHandler;

            Receive<JoinPokerRoomSession>(message => Join(message));
            Receive<ReceiveTimeout>(timeout =>
            {
                _logger.Info("Terminated Painting Session [{0}] after [{1}]", _sessionId, _idleTimeout);
                Context.Stop(Self);
            });
        }

        private void Join(JoinPokerRoomSession message)
        {
            _logger.Debug("User [{0}] joined [{1}]", message.ConnectionId, message.InstanceId);

            // syncs the new user into the session
            _hubHandler.AddUsers(message.ConnectionId, _users);

            // lets all users know about the new user
            _users.Add(message.UserId);
            _hubHandler.AddUser(_sessionId, message.UserId);
        }
    }
}
