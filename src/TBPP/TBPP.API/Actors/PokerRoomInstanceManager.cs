using Akka.Actor;
using Akka.DependencyInjection;
using TBPP.API.Session.Models;

namespace TBPP.API.Actors
{
    public class PokerRoomInstanceManager : UntypedActor
    {
        private ServiceProvider _sp;

        protected override void PreStart()
        {
            _sp = ServiceProvider.For(Context.System);
        }

        protected override void OnReceive(object message)
        {
            switch(message)
            {
                case IPokerRoomSessionMessage m:
                    var childActor = Context.Child(m.InstanceId)
                        .GetOrElse(() => Context.ActorOf(_sp.Props<PokerRoomInstanceActor>(m.InstanceId), m.InstanceId));
                    childActor.Forward(m);
                    break;
                default:
                    Unhandled(message);
                    break;
            }
        }
    }
}
