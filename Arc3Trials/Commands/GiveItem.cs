using System;
using CommandSystem;
using InventorySystem.Items;
using LabApi.Features.Wrappers;

namespace Arc3Trials.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class GiveItem : ICommand
    {
        public string Command => "GiveItem";
        public string Description => "Gives an item";
        public string[] Aliases =>["giveitem"];

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            // make sure we actually have a sender
            if (sender == null)
            {
                response = "You are nothing";
                return false;
            }

            // need at least the item id
            if (arguments.Count < 1)
            {
                response = "Usage: giveitem <itemID> [playerID]";
                return false;
            }

            // item id has to parse as an int
            if (!int.TryParse(arguments.At(0), out int itemId))
            {
                response = "ItemID is a number";
                return false;
            }

            // check the item id is actually a valid itemtype enum value
            if (!Enum.IsDefined(typeof(ItemType), itemId))
            {
                response = "ItemID is an invalid item type";
                return false;
            }

            // cast the int to the itemtype enum
            ItemType itemType = (ItemType)itemId;

            Player player;
            // if a player id was provided, target that player, otherwise just target the sender
            if (arguments.Count >= 2)
            {
                string playertar = arguments.At(1);
                if (!int.TryParse(playertar, out int targetId))
                {
                    response = "Player ID must be a number";
                    return false;
                }
                player = Player.Get(targetId);
                if (player == null)
                {
                    response = $"Could not find player: {playertar}";
                    return false;
                }
            }
            else
            {
                player = Player.Get(sender);
                if (player == null)
                {
                    response = "You are not a player";
                    return false;
                }
            }
            // if the inventory is full just drop it at their feet, otherwise add it normally
            if (player.IsInventoryFull)
                Pickup.Create(itemType, player.Position);
            else
                player.AddItem(itemType);

            response = $"Gave {player.Nickname} a {itemType.GetName()}";
            return true;
        }
    }
}