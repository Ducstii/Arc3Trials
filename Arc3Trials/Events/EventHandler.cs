using Arc3Trials.Adreniline;
using LabApi.Events.Arguments.PlayerEvents;
using PlayerRoles;

namespace Arc3Trials.Events
{
    public class EventHandler
    {
        public static void OnChangedRole(PlayerChangedRoleEventArgs args)
        {
            // checks if the new role is 049-2, if true it runs givezombienames. if it is human, it justs runs giverandomname
            if (args.NewRole.RoleTypeId == RoleTypeId.Scp0492)
                NameHandler.NameHandler.GiveZombieName(args.Player);
            else if (args.Player.IsHuman)
                NameHandler.NameHandler.GiveRandomName(args.Player);
        }

        public static void OnDied(PlayerDeathEventArgs args)
        {
            // sets the players name back to their steam name
            string name = args.Player.Nickname;
            args.Player.DisplayName = name;
            // sets adrenlinine back to ready
            AdrenalineManager.ResetPlayer(args.Player.PlayerId);
        }

        public static void OnHurt(PlayerHurtEventArgs args)
        {
            // runs a check for HP
            AdrenalineManager.CheckAdrenaline(args.Player);
        }
    }
}