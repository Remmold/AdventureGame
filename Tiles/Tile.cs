public class Tile
{
    public string TileName {get;set;}
    public bool TileState {get;set;} 
    public string TileIcon {get; set;}
    public bool Solid {get;set;}

    public Tile(string tileName, string tileIcon)
    {
        TileName = tileName;
        TileIcon = tileIcon;
        TileState = false;
        Solid = false;
    }

    public Tile()
    {
        TileName = "Empty Tile";
        TileIcon = "   ";
        TileState = false;
        Solid = false;
    }

    public virtual void RunTile(List<Character> playerList)
    {

    }

    public virtual void RunSolidTile(List<Character> playerList)
    {

    }
}


interface IRoom
{
    void RunTile();
}