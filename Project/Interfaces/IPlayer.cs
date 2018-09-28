using System.Collections.Generic;

namespace CastleGrimtol.Project
{
  public interface IPlayer
  {
    string PlayerName { get; set; }
    Dictionary<string, Item> Inventory { get; set; }
  }
}