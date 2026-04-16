using LabApi.Events.Handlers;

namespace Arc3Trials.Events
{
    public class EventRegistrar
    {
        public static void Register()
        {
            PlayerEvents.Spawned += EventHandler.OnSpawned;
            PlayerEvents.Death += EventHandler.OnDied;
            Scp049Events.ResurrectedBody += EventHandler.OnResurrected;
            PlayerEvents.Hurt += EventHandler.OnHurt;
        }

        public static void Unregister()
        {
            PlayerEvents.Spawned -= EventHandler.OnSpawned;
            PlayerEvents.Death -= EventHandler.OnDied;
            Scp049Events.ResurrectedBody -= EventHandler.OnResurrected;
            PlayerEvents.Hurt -= EventHandler.OnHurt;
        }
    }
}