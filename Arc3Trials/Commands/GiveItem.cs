using System;
using System.Linq;
using CommandSystem;
using InventorySystem.Items;
using InventorySystem.Items.Pickups;
using LabApi.Features.Wrappers;
using UnityEngine;

namespace Arc3Trials.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class GiveItem : ICommand
    {
        public string Command => "GiveItem";
        public string Description => "Gives an item";
        public string[] Aliases => new[] {"giveitem"};

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (sender == null)
            {
                response = "You are nothing";
                return false;
            }

            if (arguments.Count < 1)
            {
                response = "Usage: giveitem <itemID> [playerID]";
                return false;
            }

            if (!int.TryParse(arguments.Array[arguments.Offset + 0], out int itemId))
            {
                response = "ItemID is a number";
                return false;
            }

            if (!Enum.IsDefined(typeof(ItemType), itemId))
            {
                response = "ItemID is an invalid item type";
                return false;
            }

            ItemType itemType = (ItemType)itemId;

            Player player;
            if (arguments.Count >= 2)
            {
                string targetArg = arguments.Array[arguments.Offset + 1];
                player = Player.Get(targetArg);
                if (player == null)
                {
                    response = $"Could not find player: {targetArg}";
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
            if (player.IsInventoryFull)
            {
                Vector3 playerlocation = player.Position;
                Pickup.Create(itemType, playerlocation);


            }
            player.AddItem(itemType);
            response = $"Gave {player.Nickname} a {itemType.GetName()}";
            return true;
        }
    }
}