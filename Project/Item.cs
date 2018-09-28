using System.Collections.Generic;

namespace CastleGrimtol.Project
{
    public class Item : IItem
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Takeable { get; set; }
        public bool Losable { get; set; }

        public Item (string name, string description, bool takeable = false, bool losable = false)
        {
            Name = name;
            Description = description;
            Takeable = takeable;
            Losable = losable;
        }
    }
}