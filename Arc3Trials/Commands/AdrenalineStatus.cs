using System;
using System.Text;
using Arc3Trials.Arenaline;
using CommandSystem;
using LabApi.Features.Wrappers;

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
            StringBuilder active = new StringBuilder();
            StringBuilder cooldown = new StringBuilder();
            StringBuilder ready = new StringBuilder();

            foreach (Player player in Player.ReadyList)
            {
                if (player.IsHost)
                {
                    continue;
                }
                string name = player.DisplayName;
                switch (AdrenalineManager.GetState(player.UserId))
                {
                    case AdrenalineState.Active:
                        active.AppendLine($"  {name}");
                        break;
                    case AdrenalineState.Cooldown:
                        cooldown.AppendLine($"  {name}");
                        break;
                    case AdrenalineState.Ready:
                        ready.AppendLine($"  {name}");
                        break;
                }
            }

            response = $"\n[ACTIVE]\n{(active.Length > 0 ? active.ToString() : "  none")}\n" +
                       $"[COOLDOWN]\n{(cooldown.Length > 0 ? cooldown.ToString() : "  none")}\n" +
                       $"[READY]\n{(ready.Length > 0 ? ready.ToString() : "  none")}";
            return true;
        }
    }
}
