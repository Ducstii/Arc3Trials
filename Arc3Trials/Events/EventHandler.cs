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
                
                NameHandler.NameHandler.GiveRandomName(args.Player);
            }
        }

        public static void OnDied(PlayerDeathEventArgs args)
        {
            NameHandler.NameHandler.ResetName(args.Player);
            AdrenalineManager.ResetPlayer(args.Player.PlayerId);
        }

        public static void OnResurrected(Scp049ResurrectedBodyEventArgs args)
        {
            NameHandler.NameHandler.GiveZombieName(args.Target);
        }

        public static void OnHurt(PlayerHurtEventArgs args)
        {
            AdrenalineManager.CheckAdrenaline(args.Player);
        }
        
    }
}