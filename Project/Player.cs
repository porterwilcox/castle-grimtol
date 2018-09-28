using System.Collections.Generic;

namespace CastleGrimtol.Project
{
    public class Player : IPlayer
    {
        public string PlayerName { get; set; }
        public Dictionary<string, Item> Inventory { get; set; }

        public Player (string playerName)
        {
            PlayerName = playerName;
            Inventory = new Dictionary<string, Item>();
        }
    }
}