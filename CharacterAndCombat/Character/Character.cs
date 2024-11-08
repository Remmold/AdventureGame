using System.Dynamic;
using System.Security.Cryptography.X509Certificates;

public class Character
{
    public string Name {get;set;}
    public int CurrentHealth {get;set;}
    public int MaxHealth {get;set;}
    public int Power {get;set;}
    public int Armor {get;set;}
    public int TempArmor {get;set;}
    public int Shield{get;set;}
    public int XPos {get;set;}
    public int YPos {get;set;}
    public ConsoleColor NameColor {get;set;}
    public bool IsImmune {get;set;}
    public List<Ability>  Abilities {get;set;}
    public List<CombatEffect> CurrentStatusEffects {get;set;}
    public Inventory Inventory {get;set;}
    public bool AbleToAct {get;set;}
    public ICombatSelection ICombatHandler {get;set;}
    public int StartingHealth { get;set; }
    public int Intelligence {get;set;}
    public int V1 { get; }
    public int V2 { get; }
    public int V3 { get; }
    public PlayerCombatSelector PlayerCombatSelector { get; }
    public ConsoleColor Cyan { get; }

    public Character(string name,int startingHealth,int power,int armor,ICombatSelection icombatHandler,int intelligence,ConsoleColor selfColor)
    {
        AbleToAct = true;
        CurrentHealth = startingHealth;
        Power = power;
        Name = name;
        MaxHealth = CurrentHealth;
        Armor = armor;
        //En spelares användningsredo abilities. 4 stycken
        Abilities = new(); 
        CurrentStatusEffects = new List<CombatEffect>();
        IsImmune = false;
        Inventory = new();
        ICombatHandler = icombatHandler;
        Shield = 0;
        NameColor = selfColor;
        TempArmor = 0;
        Intelligence = intelligence;
        

    }
    // Constructor for players only
    public Character(string name,int startingHealth,int power,int armor)
    {
        AbleToAct = true;
        CurrentHealth = startingHealth;
        Power = power;
        Name = name;
        MaxHealth = CurrentHealth;
        Armor = armor;
        //En spelares användningsredo abilities. 4 stycken
        Abilities = new(); 
        CurrentStatusEffects = new List<CombatEffect>();
        IsImmune = false;
        Inventory = new();
        ICombatHandler = new PlayerCombatSelector();
        Shield = 0;
        NameColor = ConsoleColor.Cyan;
        TempArmor = 0;
        Intelligence = 1;
        

    }


    #region Taking damage
    public void TakeTrueDamage(int damage)
    {
        CurrentHealth = CurrentHealth - damage >  0 ? CurrentHealth -damage: 0;
    }
    public void TakeDamage(int damage)
    {
        if(IsImmune == true)
        {
            Console.WriteLine($"{Name} is Immune to damage this round");
        }
        else if(damage >0 )
        {
            int trueDamage = CalculateDamageTaken(damage);
            int absorbed = damage - trueDamage;
            DisplayDamageTaken(trueDamage,absorbed);
            CurrentHealth -= trueDamage;

            if(CurrentHealth < 0) 
            {
                CurrentHealth = 0;//make sure no negative health
                Utilities.ConsoleWriteLineColor($"{Name} has died",ConsoleColor.DarkRed);
            }
        }
    }
    public virtual void DisplayDamageTaken(int damage,int absorbed)
    {
        Utilities.ConsoleWriteColor(Name,NameColor);
        Console.Write(" Takes ");
        string stringOfDamage = damage.ToString();
        string stringOfAbsorbed = absorbed.ToString();
        Utilities.ConsoleWriteColor(stringOfDamage,ConsoleColor.Red);
        Console.Write(", ");
        Utilities.ConsoleWriteColor(stringOfAbsorbed,ConsoleColor.DarkYellow);
        Console.WriteLine(" absorbed by armor");
    }
    public int ReduceDamageByArmor(int unmitigatedDamage)
    {
        int totalArmor = Armor+TempArmor;
        double percentageReduction =  (double)totalArmor/(totalArmor+50);
        int trueDamage = (int)(unmitigatedDamage*(1-percentageReduction));
        return trueDamage;
    }
    public int CalculateDamageTaken(int damage)
    {
        damage = ReduceDamageByArmor(damage);
        return damage < 0 ? 0 : damage; // make sure dmage isnt negative
    }
#endregion
#region Statuseffects

    public bool CharacterHasEffect(eCombatEffect eCombatEffect)
    {
        foreach(CombatEffect ce in CurrentStatusEffects)
        {
            if(eCombatEffect == ce.Type)
            {
                return true;
            }
        }
        return false;
    }

    public virtual void ClearEffect(CombatEffect effect)
    {
        switch(effect.Type)
        {
            case eCombatEffect.ArmorBuff:
                TempArmor = 0;
                Utilities.ConsoleWriteColor(Name,NameColor);
                Utilities.ConsoleWriteColor("s Armor",ConsoleColor.DarkYellow);
                Console.WriteLine($" is no longer enhanced");
                break;
            case eCombatEffect.Freeze:
                AbleToAct = true;
                Utilities.ConsoleWriteColor(Name,NameColor);
                Console.Write($" is no longer ");
                Utilities.ConsoleWriteLineColor("Frozen",ConsoleColor.Blue);
                break;
            case eCombatEffect.Poison:
                for(int i = 0; i< CurrentStatusEffects.Count; i++)
                {
                    if(CurrentStatusEffects[i] == effect)
                    {
                        CurrentStatusEffects.Remove(CurrentStatusEffects[i]);
                    }
                }
                Utilities.ConsoleWriteColor(Name,NameColor);
                Console.Write($" is no longer ");
                Utilities.ConsoleWriteLineColor("Poisoned",ConsoleColor.DarkGreen);
                break;
            case eCombatEffect.Burn: // Add this case for Burn effect
                for (int i = 0; i < CurrentStatusEffects.Count; i++)
                {
                    if (CurrentStatusEffects[i] == effect)
                    {
                        CurrentStatusEffects.Remove(CurrentStatusEffects[i]);
                        break;
                    }
                }
                Utilities.ConsoleWriteColor(Name, NameColor);
                Console.Write(" is no longer ");
                Utilities.ConsoleWriteLineColor("Burning", ConsoleColor.Red);
                break;
            case eCombatEffect.Immune:
                Utilities.ConsoleWriteColor(Name,NameColor);
                Console.Write($" is no longer ");
                Utilities.ConsoleWriteLineColor("Immune",ConsoleColor.White);
                IsImmune = false;
                break;
            case eCombatEffect.Shield:
                Shield = 0;
                break;
            case eCombatEffect.HealingOverTime:
                for (int i = 0; i < CurrentStatusEffects.Count; i++)
                {
                    if (CurrentStatusEffects[i] == effect)
                    {
                        CurrentStatusEffects.Remove(CurrentStatusEffects[i]);
                        break;
                    }
                }
                Utilities.ConsoleWriteColor(Name, NameColor);
                Console.Write(" is no longer affected by ");
                Utilities.ConsoleWriteLineColor("Healing Over Time", ConsoleColor.Green);
                break;
        }
        //code for removing the effect
    }
    public virtual void ClearAllEffects()
    {
        for(int i = 0; i< CurrentStatusEffects.Count ; i++)
        {
            ClearEffect(CurrentStatusEffects[i]);
            CurrentStatusEffects.Remove(CurrentStatusEffects[i]);
        }
    }

#endregion
#region Abilityrelated methods
    //Allows the used to pick an ability and replace it with another from AllKnownAbilities

    public int SelectAbilityIndex(string message)
    {
        return  Utilities.PickIndexFromList(Utilities.ToStringList(Abilities),message);
    }
#endregion




}
