public class TileKey : Tile
{
    public TileKey() : base("Key tile", " ⚿ ")
    {
    }
    public override void RunTile(List<Character> playerList)
    {
        Character player = playerList[0];
        if (!IsVisited)
        {
            Console.WriteLine("Congratulations, adventurer! You get a key to your inventory!");
            player.Inventory.Items.Add(new Item("DoorKey", "A mysterious key... What can it be for..?"));
            IsVisited = true;
            RemoveTile = true;
        }
        else
        {
            Console.WriteLine("You've already gotten the key. But you can always revisit for nostalgia.");
        }
    }
}