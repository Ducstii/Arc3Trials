using Arc3Trials.Adreniline;
using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Events.Arguments.Scp049Events;
using PlayerRoles;

namespace Arc3Trials.Events
{
    public class EventHandler
    {
        private static readonly Team[] HumanTeams =
        {
            Team.ClassD,
            Team.Scientists,
            Team.FoundationForces,
            Team.ChaosInsurgency,
        };

        public static void OnSpawned(PlayerSpawnedEventArgs args)
        {
            foreach (Team team in HumanTeams)
            {
                if (args.Role.Team == team)
                {
                    NameHandler.NameHandler.GiveRandomName(args.Player);
                }
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