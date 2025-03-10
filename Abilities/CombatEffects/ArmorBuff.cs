public class ArmorBuff : CombatEffect
{
    public ArmorBuff(int duration,int magnitude,bool areaEffect) : base(duration,magnitude,eCombatEffect.ArmorBuff,areaEffect)
    {
        
    }

    public override void ApplyEffect(Character self,Character target)
    {
        base.ApplyEffect(self,target);
        List<Character> affectedCharacters = new();
        if(AreaEffect)
        {
            affectedCharacters = target.FriendList;
        }
        else affectedCharacters.Add(target);
        foreach(Character c in affectedCharacters)
        {
            c.TempArmor = Magnitude;
        }
    }
    //Message that is displayed when the effect affects a character! 
    public override void PrintApplication(Character character)
    {

            Utilities.ConsoleWriteColor(character.Name,character.NameColor);
            Utilities.ConsoleWriteColor("s Armor",ConsoleColor.DarkGray);
            Console.Write($" Has Increased ");
            Console.WriteLine($" for {Duration} rounds");
    }
        public override void AfterRound(Character character)
    {

        //reduces duration by 1round as long as its not the first round of application
        if(!FirstRound)
        {
            if(Duration > 0 )
            {
                Duration--;
            }  
        }
        else
        {
            FirstRound = false;
        }
    }
    public override CombatEffect CloneEffect()
    {
        return new ArmorBuff(Duration,Magnitude,AreaEffect);
    }
}