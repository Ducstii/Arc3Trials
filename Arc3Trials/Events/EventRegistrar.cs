using LabApi.Events.Handlers;

namespace Arc3Trials.Events
{
    public class EventRegistrar
    {
        // hooks up all the event handlers
        public static void Register()
        {
            PlayerEvents.ChangedRole += EventHandler.OnChangedRole;
            PlayerEvents.Death += EventHandler.OnDied;
            PlayerEvents.Hurt += EventHandler.OnHurt;
        }

        // unhooks all the event handlers, called on plugin disable
        public static void Unregister()
        {
            PlayerEvents.ChangedRole -= EventHandler.OnChangedRole;
            PlayerEvents.Death -= EventHandler.OnDied;
            PlayerEvents.Hurt -= EventHandler.OnHurt;
        }
    }
}
