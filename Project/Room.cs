using System.Collections.Generic;
using System.Text.RegularExpressions;
using System;

namespace CastleGrimtol.Project
{
    public class Room : IRoom
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Losable { get; set; }
        public string AltDescription { get; set; }
        public Dictionary<string, Item> Items { get; set; }
        public Dictionary<string, Room> Exits { get; set; }
        Color color = new Color();

        public void ColoredDescription(Game game)
        {
            string description = "";
            if (game.CurrentRoom.Name == "Hallway" && game.timerSet == true)
            {
                description = game.CurrentRoom.AltDescription;
                game.CurrentRoom.Exits["livingroom"].Losable = false;
            }
            else if (!game.CurrentRoom.Losable)
            {
                description = game.CurrentRoom.Description;
            }
            else
            {
                description = game.CurrentRoom.AltDescription;
                game.playing = false;
            }
            string pattern = @"\s+";
            string[] words = Regex.Split(description, pattern);
            for (int i = 0; i < words.Length; i++)
            {
                string word = words[i];
                if (game.CurrentRoom.Items.ContainsKey(word))
                {
                    if (!game.CurrentRoom.Items[word].Takeable)
                    {
                        color.Cyan(word);
                        continue;
                    }
                    else
                    {
                        color.Magenta(word);
                        continue;
                    }
                }
                if (game.CurrentRoom.Exits.ContainsKey(word))
                {
                    color.Yellow(word);
                    continue;
                }
                Console.Write(word + " ");
            }
        }
        public Room (string name, string description, bool losable = false, string altDescription = null)
        {
            Name = name;
            Description = description;
            Losable = losable;
            AltDescription = altDescription;
            Items = new Dictionary<string, Item>();
            Exits = new Dictionary<string, Room>();
        }
    }
}