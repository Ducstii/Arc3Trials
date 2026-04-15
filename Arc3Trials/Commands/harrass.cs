using System;
using CommandSystem;
using LabApi.Features.Wrappers;
using Arc3Trials.Adreniline;

namespace Arc3Trials.Commands;

public class harrass
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class harrasscommand : ICommand
    {
        public string Command => "harrass";
        public string[] Aliases => null;
        public string Description => "harrass people.";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (sender == null)
            {
                response = "You are unable to run this command.";
                return false;
            }
            if (arguments.Count < 2)
            {
                response = "Usage: harrass <playerID> <flashCount>";
                return false;
            }

            if (!int.TryParse(arguments.At(0), out int playerId))
            {
                response = "Player ID must be a number";
                return false;
            }

            Player player = Player.Get(playerId);
            if (player == null)
            {
                response = $"Could not find player: {arguments.At(0)}";
                return false;
            }

            if (!int.TryParse(arguments.At(1), out int flashCount))
            {
                response = "Flash count must be a number";
                return false;
            }

            player.Harrass(flashCount);
            response = $"Harassing {player.Nickname}";
            return true;
        }
    }
}