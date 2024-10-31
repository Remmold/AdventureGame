public class StarterTile : Tile
{
    public StarterTile() : base("Start", " S ")
    {
    }

    public override void RunRoom(Character player)
    {
        if (RoomState == false)
        {
            Console.WriteLine("Welcome, adventurer! This is where your journey begins. Take a deep breath and step boldly into the unknown. Good luck!");
            RoomState = true;
        }
        else
        {
            Console.WriteLine("You've already been here. The journey awaits beyond, no need to linger.");
        }
    }

}