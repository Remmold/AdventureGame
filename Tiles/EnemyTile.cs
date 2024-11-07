class EnemyTile : RewardTile
{
    public int NrOfEnemies {get;set;}
    public DifficultyLevel Difficulty {get; set;}
    public string Race {get; set;}
    
    public EnemyTile(string tileName, int reward, int nrOfEnemies) : base(tileName, " ☠ ", reward)
    {
        NrOfEnemies = nrOfEnemies;
        Solid = true;
    }

    public override void RunSolidTile(List<Character> playerList)
    {
        
        if (IsVisited == false) // Never entered the tile before!
        {
            string encounterMessage = $"As you step into the enemy lair, {NrOfEnemies} fierce foes appear, blocking your path!";
            Success = CombatHandler.RunCombatScenario(NPCFactory.GenerateNPCs(ConsoleColor.DarkRed,50,eEnemyFamily.Goblin,NrOfEnemies),playerList,encounterMessage);
            IsVisited = true;

        }
        else // Entered the tile before
        {
            if (!Success)
            {
                string encounterMessage = $"\"Ah, it's you again! You weak and pitiful human, fleeing from us last time. You should have stayed away, coward!\"";
                Success = CombatHandler.RunCombatScenario(NPCFactory.GenerateNPCs(ConsoleColor.DarkRed,50,eEnemyFamily.Goblin,NrOfEnemies),playerList,encounterMessage);
            }
            else
            {
                Utilities.CharByCharLine("The room is quiet now... The scent of past victory lingers in the air. You have been here before.", 8, ConsoleColor.DarkBlue);
            }                
        }
    }

    public List<Character> CreateEnemies()
    {
        Random random = new Random();
        List<Character> returnList = new();
        for(int i = 0; i< NrOfEnemies ; i++)
        {
            string name="";
            switch(Race.ToUpper())
            {
                case "GOBLIN":
                    
                    switch(random.Next(1,5))
                    {
                        case 1:
                        name = "Goblin fighter";
                            break;
                        case 2:
                        name = "Goblin warlord";
                            break;
                        case 3: 
                        name = "Goblin shaman";
                            break;
                        case 4:
                        name = "Goblin scout";
                            break;
                        case 5:
                        name = "Goblin tamer";
                            break;
                    }
                    ICombatSelection combatHandler = new NPCSupportAI();
                    Character e = new Character(name,15*(int)Difficulty,10+random.Next(1,4),5*random.Next(1,4),combatHandler,ConsoleColor.DarkGray,50);
                    returnList.Add(e);
                    break;
            }
        }
        return returnList;
    }

}

public enum DifficultyLevel
{
    Easy = 1,
    Medium = 2,
    Hard = 3
}