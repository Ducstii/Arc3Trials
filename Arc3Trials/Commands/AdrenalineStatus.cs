using System;
using System.Text;
using Arc3Trials.Adreniline;
using CommandSystem;
using LabApi.Features.Wrappers;
using NorthwoodLib.Pools;

namespace Arc3Trials.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class AdrenalineStatus : ICommand
    {
        public string Command => "adrenstatus";
        public string Description => "Shows the adren status";
        public string[] Aliases => new[] { "adren" };

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            StringBuilder active = StringBuilderPool.Shared.Rent();
            StringBuilder cooldown = StringBuilderPool.Shared.Rent();
            StringBuilder ready = StringBuilderPool.Shared.Rent();

            foreach (Player player in Player.ReadyList)
            {
                if (player.IsHost)
                {
                    continue;
                }
                switch (AdrenalineManager.GetState(player.PlayerId))
                {
                    case AdrenalineState.Active:
                        active.AppendLine($"  {player.DisplayName}, ID: {player.PlayerId}");
                        break;
                    case AdrenalineState.Cooldown:
                        cooldown.AppendLine($"  {player.DisplayName}, ID: {player.PlayerId}");
                        break;
                    case AdrenalineState.Ready:
                        ready.AppendLine($"  {player.DisplayName}, ID: {player.PlayerId}");
                        break;
                }
            }

            string activeStr = StringBuilderPool.Shared.ToStringReturn(active);
            string cooldownStr = StringBuilderPool.Shared.ToStringReturn(cooldown);
            string readyStr = StringBuilderPool.Shared.ToStringReturn(ready);

            response = $"\nActive\n{(activeStr.Length > 0 ? activeStr : "  none\n")}" +
                       $"Cooldown\n{(cooldownStr.Length > 0 ? cooldownStr : "  none\n")}" +
                       $"Ready\n{(readyStr.Length > 0 ? readyStr : "  none")}";
            return true;
        }
    }
}
