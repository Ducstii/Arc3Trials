using Arc3Trials.Adreniline;
using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Events.Arguments.Scp049Events;

namespace Arc3Trials.Events
{
    


    public class EventHandler
    {

        public static void OnSpawned(PlayerSpawnedEventArgs args)
        {
            if (args.Player.IsHuman)
            {
                NameHandler.NameHandler.GiveZombieName(args.Player);
            }
        }

        public static void OnDied(PlayerDeathEventArgs args)
        {
            NameHandler.NameHandler.ResetName(args.Player);
            AdrenalineManager.ResetPlayer(args.Player.UserId);
        }

        public static void OnResurrected(Scp049ResurrectedBodyEventArgs args)
        {
            NameHandler.NameHandler.GiveZombieName(args.Target);
        }

        public static void OnHurt(PlayerHurtEventArgs args)
        {
            AdrenalineManager.CheckAdrenaline(args.Player);
        }

        public static void RoundStarted()
        {
            NameHandler.NameHandler.ResetZombieCounter();
        }
    }
}