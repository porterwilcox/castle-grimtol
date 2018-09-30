using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;

namespace CastleGrimtol.Project
{
    public class Game : IGame
    {
        public Room CurrentRoom { get; set; }
        public Player CurrentPlayer { get; set; }
        public bool win = false;
        public bool quit = false;
        public bool playing = true;
        Color color = new Color();
        public int counter = 0;
        public bool timerSet = false;

        public void Setup()
        {
            //CREATE ROOMS W/ NAMES AND DESCRTIPTIONS
            Room bedroom = new Room("Bedroom", "You are in your bedroom. There's a window on one wall and a door to the hallway on the other. Your jacket is hung over the office chair near your bed.");
            Room hallway = new Room("Hallway", "You sneak to the end of the hallway from which there are several places to go. You can hear your parents in the livingroom watching TV. If they were't there you'd be able to exit through the front door. The backdoor is unlocked and looks like a good escape route. It sounds like the kitchen is empty.", false, "You return to the hallway and hide yourself in the shadow of the corner. As the stove timer sounds you hear both your parents move to the kitchen. You hear them discussing their confusion of the seemingly autonomous stove timer. You decide to use the backdoor or enter the livingroom in this brief moment of distraction you've created.");
            Room kitchen = new Room("Kitchen", "In kitchen the light is off so you can't see much. You can't turn it on because your parents would be alerted of your presence. You faintly see the fridge and dining table outlines from the stove clock light. You can go all the way through the kitchen to livingroom or turn back to the hallway again.");
            Room livingroom = new Room("Livingroom", "In the vacant livingroom you see the couch and a way outside through the front door.", true, $"'{CurrentPlayer.PlayerName.ToUpper()}! What are you doing out of bed?! You know you're grounded,' your parents sternly say to you as you enter the livingroom.");
            Room outside = new Room("Outside", "You successfully snuck out of your house and made it to the end of your block! But you aren't at the full size candy bar lane yet. Good thing you brought your jacket to keep you warm. You see the pumpkinpatch and cornmaze to your left and beyond them the continuation of the street illuminated by a flickering traffic light.", true, "You snuck out of your house! But by the time you make it to the end of the block you are freezing in the cold October night. You realize that you won't be able to continue without your jacket and head back home to turn yourself in.");
            Room pumkinPatch = new Room("Pumpkin Patch", "The pumpkin patch is dimly lit by the lights of the jack-o-lantern's. The hay crunches under your feet as you move about. You notice an unidenifiable shape near a pumpkin through the darkness. A few feet away is your very own jackolantern you carved the day before. The cornmaze or the street are your two available exits.");
            Room cornMaze = new Room("Corn Maze", "In the middle of the corn maze you can't see above the tall corn stalks. You remember how to get back to the end of your block but don't think you'll be able to navigate the rest of the maze in the dim light.");
            Room street = new Room("Street", "You are walking down the street which which will end at the neighborhood that hands out full size candy bars. However, since the road follows the perimeter of the corn field you aren't even halfway to the neighborhood yet. It's getting late enough that the houses will stop greeting trick-or-treaters soon. You can continue down the block or go back to the end of your block.");
            Room block = new Room("block", "", true, "You continue on down the block in hopes of getting to the neighborhood in time. But all the porch lights are off and people are asleep by the time you make it to the neighborhood, so you don't get of the delicious full size candy bars.");
            Room neighborhood = new Room("Neighborhood", "You've made it to the full size candy bar neighborhood in time to get all the candy bars!");

            //GIVE ROOMS THEIR EXITS
            bedroom.Exits.Add("hallway", hallway);
            hallway.Exits.Add("kitchen", kitchen);
            hallway.Exits.Add("livingroom", livingroom);
            kitchen.Exits.Add("livingroom", livingroom);
            kitchen.Exits.Add("hallway", hallway);
            livingroom.Exits.Add("outside", outside);
            outside.Exits.Add("pumpkinpatch", pumkinPatch);
            outside.Exits.Add("cornmaze", cornMaze);
            outside.Exits.Add("street", street);
            pumkinPatch.Exits.Add("cornmaze", cornMaze);
            pumkinPatch.Exits.Add("street", street);
            cornMaze.Exits.Add("back", outside);
            cornMaze.Exits.Add("specialTopSecretKey", neighborhood);
            street.Exits.Add("back", outside);
            street.Exits.Add("block", block);

            //CREATE ITEMS
            Item window = new Item("window", "You quietly open the window that you hope to escape through. The cool night breeze flows in and you realize that the shrubs have grown out of control and are blocking your escape. You close the window.");
            Item jacket = new Item("jacket", "your go to black hoody.", true);
            Item backdoor = new Item("backdoor", "You open the backdoor quietly so your parents don't hear you. As you begin to slide out the opening your dog comes running up and bolts inside the house. Your parents investigate how the dog got inside and catch you in the act of escaping!", false, true);
            Item fridge = new Item("fridge", "Out of seemingly habit you open up the fridge. *THUD!* sounds the falling ketchap as it hits the ground. Your parents investigate the sound and send you to your room immediately!", false, true);
            Item table = new Item("table", "You quietly take a seat at the table as you ponder how you're going to sneak out of your house. After brainstorming, you're ready for action. You stand up again.");
            Item stove = new Item("stove", "Ah ha! The stove's timer! You set the timer for 30 seconds.");
            Item couch = new Item("couch", "You take a seat on the empty sofa and get sucked into the TV. Your parents return from the kitchen and scold you for being out of bed.", false, true);
            Item jackOLantern = new Item("jackolantern", "a buck toothed pumpkin head emmiting light through the big old smile.", true);
            Item shape = new Item("shape", "As you approach the shape it bolts towards you. 'MEOW!' sounds the startled cat as it bolts between your legs. You step suddenly to avoid crushing its tail and twist your ankle. You hobble home without any candy.", false, true);


            //GIVE ROOMS THEIR ITEMS
            bedroom.Items.Add("jacket", jacket);
            bedroom.Items.Add("window", window);
            hallway.Items.Add("backdoor", backdoor);
            kitchen.Items.Add("fridge", fridge);
            kitchen.Items.Add("table", table);
            kitchen.Items.Add("stove", stove);
            livingroom.Items.Add("couch", couch);
            pumkinPatch.Items.Add("jackolantern", jackOLantern);
            pumkinPatch.Items.Add("shape", shape);

            //SETUP CURRENTROOM
            CurrentRoom = bedroom;
        }
        public void GetUserInput()
        {
            while (true)
            {
                string input = Console.ReadLine().ToLower();
                string pattern = @"\s";
                string[] action = Regex.Split(input, pattern);
                if (action[0] == "help")
                {
                    Help();
                }
                else if (action[0] == "inventory")
                {
                    Inventory();
                }
                else if (action[0] == "quit")
                {
                    playing = false;
                    quit = true; //this line will need to be modified
                    break;
                }
                else if (action.Length < 2)
                {
                    return;
                }
                if (action[0] == "use")
                {
                    UseItem(action[1]);
                    if (!playing)
                    {
                        return;
                    }
                }
                if (action[0] == "take")
                {
                    TakeItem(action[1]);
                }
                if (action[0] == "enter")
                {
                    Enter(action[1]);
                    if (!playing)
                    {
                        Console.Clear();
                        // CurrentRoom.ColoredDescription(this); //No need and slower to use a colored description non an altDescription
                        System.Console.WriteLine(CurrentRoom.AltDescription);
                    }
                    break;
                }
                System.Console.WriteLine("\n\nPress enter to continue");
                Console.ReadLine();
                break;
            }

        }

        public void Enter(string exit)
        {
            Console.Clear();
            if (!CurrentRoom.Exits.ContainsKey(exit))
            {
                System.Console.WriteLine("That's not a valid exit from this room");
                System.Console.WriteLine("\nPress enter to continue");
                Console.ReadLine();
                return;
            }
            if (CurrentRoom.Exits[exit].Losable)
            {
                playing = false;
            }
            CurrentRoom = CurrentRoom.Exits[exit];
        }
        public void TakeItem(string itemName)
        {
            Console.Clear();
            if (!CurrentRoom.Items.ContainsKey(itemName))
            {
                System.Console.WriteLine("That's not a valid item in this room.");
                return;
            }
            if (!CurrentRoom.Items[itemName].Takeable)
            {
                System.Console.WriteLine($"The {itemName} is not a takeable item.");
                return;
            }
            if (itemName == "jacket")
            {
                CurrentRoom.Exits["hallway"].Exits["livingroom"].Exits["outside"].Losable = false;
            }
            color.Magenta($"You pick up the {CurrentRoom.Items[itemName].Name}. It is {CurrentRoom.Items[itemName].Description}\n");
            color.White($"The {CurrentRoom.Items[itemName].Name} is now in your inventory.");
            CurrentPlayer.Inventory.Add(CurrentRoom.Items[itemName].Name, CurrentRoom.Items[itemName]);
            CurrentRoom.Items.Remove(CurrentRoom.Items[itemName].Name);
        }
        public void UseItem(string itemName)
        {
            Console.Clear();
            //Here's where I'll account for items in the player's inventory
            if (!CurrentRoom.Items.ContainsKey(itemName) && !CurrentPlayer.Inventory.ContainsKey(itemName))
            {
                System.Console.WriteLine("That's not a valid item in either this room nor your inventory.");

                return;
            }
            if (CurrentRoom.Items.ContainsKey(itemName))
            {
                if (CurrentRoom.Items[itemName].Takeable)
                {
                    System.Console.WriteLine($"The {itemName} is not a useable item.");
                    return;
                }
                color.Cyan(CurrentRoom.Items[itemName].Description);
                if (CurrentRoom.Items[itemName].Losable)
                {
                    playing = false;
                }
                if (itemName == "stove")
                {
                    timerSet = true;
                }
                return;
            }
            if (itemName == "jackolantern")
            {
                if (CurrentRoom.Name != "Corn Maze")
                {
                    System.Console.WriteLine("You take a look at your super cool jack-o-lantern, but it's not of much use to you at this location.");
                    return;
                }
                System.Console.WriteLine("Jack-o-lantern for the win! The small candle light shines through the pumpkin's buck-toothed smile and illuminates the path in front of you. You navigate the through the corn maze and see the hustle of the neighborhood's trick-or-treaters making rounds to all houses.");
                CurrentRoom = CurrentRoom.Exits["specialTopSecretKey"];
                return;
            }
            if (itemName == "jacket")
            {
                if (CurrentRoom.Name != "Outside" && CurrentRoom.Name != "Pumpkin Patch" && CurrentRoom.Name != "Corn Maze" && CurrentRoom.Name != "Street")
                {
                    System.Console.WriteLine("Good thinking to bring your jacket! It'll probably be cold outside.");
                    return;
                }
                System.Console.WriteLine("The cold October air can't pierce through your hoody. You're glad you wore your jacket otherwise you'd probably of had to of aborted the mission.");
                return;
            }
        }
        public void Inventory()
        {
            Console.Clear();
            if (CurrentPlayer.Inventory.Count >= 1)
            {
                color.White($"{CurrentPlayer.PlayerName}'s Inventory:\n");
                foreach (KeyValuePair<string, Item> item in CurrentPlayer.Inventory)
                {
                    color.Cyan("\n" + item.Key + "\n");
                    color.White(item.Value.Description + "\n");
                }
                return;
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
            color.Blue($@"Your mission: 
                Sneak out of your house and get to the full size candy bar neighborhood 
                early enough to get all the candy.");
            color.White("\n\nSubmit 'Help' at anytime to display these instructions.\n\n");
            color.Yellow("Submit 'Enter' + a valid destination to go to that location.\n\n");
            color.Cyan("Submit 'Inventory' at anytime to display any items in your inventory.\n\n");
            color.Cyan("Submit 'Use' + a valid item within the current room or a valid item within your inventory to use that item.\n\n");
            color.Magenta("Submit 'Take' + a valid item to add that item to your inventory.\n\n");
            color.Red("Submit 'Quit' at anytime to quit the current game.\n\n");
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
            CurrentPlayer = player;
            CurrentPlayer.Inventory = new Dictionary<string, Item>();
            timerSet = false;
            win = false;
            Setup();
            // if (counter < 1)
            // {
            Help();
            System.Console.WriteLine("Press enter to continue");
            Console.ReadLine();
            // }
            counter++;
            while (playing)
            {
                Console.Clear();
                // System.Console.WriteLine(CurrentRoom.Description);
                CurrentRoom.ColoredDescription(this);
                if (CurrentRoom.Name == "Neighborhood")
                {
                    win = true;
                    break;
                }
                System.Console.WriteLine("\n\nWhat do you do?");
                GetUserInput();
            }
            if (!playing && !quit)
            {
                Thread.Sleep(5000);
                color.Red("\nYou got busted! Game Over.\n");
                Thread.Sleep(1000);
            }
            if (win)
            {
                System.Console.WriteLine($"\n\nCongratulations {CurrentPlayer.PlayerName}! You Won!\n");
                Thread.Sleep(3000);
            }
            System.Console.WriteLine("Do you want to play again? (Y/N)");
            if (Console.ReadLine().ToLower().Contains("n"))
            {
                return;
            }
            playing = true;
            StartGame(CurrentPlayer);
        }
    }
}