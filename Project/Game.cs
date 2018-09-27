using System;
using System.Collections.Generic;

namespace CastleGrimtol.Project
{
    public class Game : IGame
    {
        public Room CurrentRoom { get; set; }
        public Player CurrentPlayer { get; set; }
        public bool playing = true;
        Color color = new Color();

        public void Setup()
        {
            //CREATE ROOMS W/ NAMES AND DESCRTIPTIONS
            Room bedroom = new Room("Bedroom", "You are in your bedroom. There's a WINDOW on one wall and on the opposite is the DOOR.  There's a DESK and chair near your bed.");
            Room hallway = new Room("Hallway", "You are at the end of the hallway from which there are several places to go. You can hear your parents in the LIVINGROOM. If they were't there you'd be able to exit through the front door. The BACKDOOR is unlocked and looks like a good escape route. No one is in the KITCHEN.");
            Room kitchen = new Room("Kitchen", "In kitchen the light is off you can't see much. You can't turn it on because your parents would be alerted of your presence. You make out the outlines of the FRIDGE, STOVE and dining TABLE. You can exit back to the HALLWAY.");
            Room livingroom = new Room("Livingroom", "In your livingroom you see the COUCH and the FRONTDOOR.");
            Room endOfBlock = new Room("End of Block", "You successfully snuck out of your house and made it to the end of your block! But you aren't quite at the full size candy bar neighborhood yet. From where you're standing you can go to the PUMPKIN PATCH, CORN MAZE or continue down the STREET.");
            Room pumkinPatch = new Room("Pumkin Patch", "The pumpkin patch is dimly lit by the lights of the jack-o-lantern's. The hay crunches under your feet as you move about. You notice an unidenifiable SHAPE near a pumpkin through the darkness.  There's also a super large PUMPKIN and your very own JACK-O-LANTERN you carved the day before. You can exit to the CORN MAZE or the STREET.");
            Room street = new Room("Street", "You are walking down the street which which will end at the neighborhood that hands out full size candy bars. However, you aren't even halfway to the neighborhood yet and it's getting late enough that the houses will stop greeting trick-or-treaters. You can continue down the STREET or go BACK to the end of your block.");
            Room cornMaze = new Room("Corn Maze", "In the middle of the corn maze you can't see above the tall corn stalks. You remember how to get BACK but don't think you'll be able to navigate the rest of the maze in the dim light.");
            Room neighborhood = new Room("Neighborhood", "You've made it to the full size candy bar neighborhood in time to get all the candy bars!");

            //GIVE ROOMS THEIR EXITS
            bedroom.Exits.Add("hallway", hallway);
            hallway.Exits.Add("kitchen", kitchen);
            hallway.Exits.Add("livingroom", livingroom);
            kitchen.Exits.Add("hallway", hallway);
            livingroom.Exits.Add("frontdoor", endOfBlock);
            endOfBlock.Exits.Add("pumpkinpatch", pumkinPatch);
            endOfBlock.Exits.Add("cornmaze", cornMaze);
            endOfBlock.Exits.Add("street", street);
            pumkinPatch.Exits.Add("cornmaze", cornMaze);
            pumkinPatch.Exits.Add("street", street);
            cornMaze.Exits.Add("back", endOfBlock);
            cornMaze.Exits.Add("neighborhood", neighborhood);
            street.Exits.Add("back", endOfBlock);

            //CREATE ITEMS
            Item jacket = new Item("Jacket", "Your go to black hoody.");
            Item jackOLantern = new Item("Jack-O-Lantern", "A buck toothed pumpkin head emmiting light through the big old smile.");

            //GIVE ROOMS THEIR ITEMS
            bedroom.Items.Add(jacket);
            pumkinPatch.Items.Add(jackOLantern);

            //SETUP CURRENTROOM
            CurrentRoom = bedroom;
        }
        public void GetUserInput()
        {
            while(true)
            {
                string action = Console.ReadLine().ToLower();
                if (action.Length < 4)
                {
                    continue;
                }
                if (action == "help")
                {
                    Help();
                    break;
                }
                if (action == "quit")
                {
                    playing = false;
                    break;
                }
            }

        }

        public void Enter(string direction)
        {
            
        }
        public void TakeItem(string itemName)
        {
            
        }
        public void UseItem(string itemName)
        {
            
        }
        public void Inventory()
        {
            if(CurrentPlayer.Inventory.Count >= 1)
            {
                System.Console.WriteLine($"{CurrentPlayer.PlayerName}'s Inventory:\n");
                foreach(Item item in CurrentPlayer.Inventory)
                {
                    System.Console.WriteLine(item.Name);
                    System.Console.WriteLine(item.Description +"\n");
                }
            }
            color.Red("You don't have any items in your inventory.");
        }
        public void Look()
        {
            
        }

        public void Help()
        {
            Console.Clear();
            color.White("Game Instructions and Command Help:\n\n");
            color.Magenta($@"Your mission: 
                Sneak out of your house and get to the full size candy bar neighborhood 
                early enough to get all the candy.");
            color.White("\n\nSubmit 'Help' at anytime to display these instructions.\n\n");
            color.White("Submit 'Look' at anytime to display your current location description.\n\n");
            color.Red("Submit 'Quit' at anytime to quit gameplay.\n\n");
            color.Yellow("Submit 'Enter' + a valid destination to go to that location.\n\n");
            color.Cyan("Submit 'Take' + a valid item to add that item to your inventory\n\n");
            color.Cyan("Submit 'Use' + a valid item name in your inventory to use that item.\n\n");
            System.Console.WriteLine("Press enter to continue");
            Console.ReadLine();
        }



        public void Quit()
        {
            
        }

        public void Reset()
        {
            
        }

        public void StartGame(Player player)
        {
            Console.Clear();
            Setup();
            CurrentPlayer = player;
            Help();
            while(playing)
            {
                Console.Clear();
                System.Console.WriteLine(CurrentRoom.Description);
                System.Console.WriteLine("\nWhat do you do?");
                GetUserInput();
            }
        }
    }
}