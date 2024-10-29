public class Poison : CombatEffect
{
    public Poison(int duration,int magnitude) : base(duration,magnitude,eCombatEffect.Poison)
    {

    }

    public override void ApplyEffect(Character character)
    {
        base.ApplyEffect(character);

    }
    public override void PrintApplication(Character character)
    {
            Console.Write($"{character.Name} has been ");
            Utilities.ConsoleWriteColor("Poisoned",ConsoleColor.DarkGreen);
            Console.WriteLine($" for {Duration} rounds");
    }
        public override void ResolveEffect(Character character)
    {
        Console.Write($"{character.Name} suffers {Magnitude} damage due to  ");
        Utilities.ConsoleWriteColor("Poison ",ConsoleColor.DarkGreen);
        //player takes poison damage
        character.CurrentHealth = character.CurrentHealth <= 0 ? 0 : character.CurrentHealth-Magnitude;
        
        Console.WriteLine($" {Duration} rounds remaining");
        //reduces duration by 1round
        base.ResolveEffect(character);
    }
    public override CombatEffect CloneEffect()
    {
        return new Poison(Duration,Magnitude);
    }
}