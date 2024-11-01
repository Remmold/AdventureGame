
public class PlayerCombatHandler : ICombatHandler
{
    public Ability SelectAbility(List<Ability> abilityList)
    {
        //int chosenIndex = Utilities.PickIndexFromList(Utilities.ToStringList(abilityList),"What ability do you want to use?");
        //return abilityList[chosenIndex];
        int markedIndex = 0;
        bool stillChoosing = true;
        Ability returnAbility = null;
        
        
        while(stillChoosing)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("What ability do you want to use?");
            Console.ResetColor();
            for(int i = 0; i< abilityList.Count; i++)
            {
                if(i == markedIndex)
                {
                    Utilities.ConsoleWriteColor("*",ConsoleColor.Blue);
                    if(abilityList[i].CurrentCooldown < abilityList[i].CoolDownTimer)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write(abilityList[i].Name);
                        Console.Write($" ({abilityList[i].CoolDownTimer-abilityList[i].CurrentCooldown})");
                    }
                    else
                    {
                    Console.Write(abilityList[i].Name);
                    }
                    Utilities.ConsoleWriteLineColor("*",ConsoleColor.Blue);

                }
                else
                {
                    if(abilityList[i].CurrentCooldown < abilityList[i].CoolDownTimer)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write(abilityList[i].Name);
                        Console.WriteLine($" ({abilityList[i].CoolDownTimer-abilityList[i].CurrentCooldown})");
                    }
                    else
                    {
                        Console.WriteLine(abilityList[i].Name);
                    }
                    Console.ResetColor();
                }
            }
            ConsoleKeyInfo pressedKey = Console.ReadKey(true);
            switch(pressedKey.Key)
            {
                case ConsoleKey.W:
                case ConsoleKey.UpArrow:
                    if(markedIndex > 0 )
                    {
                        markedIndex--;
                        
                    }
                    break;
                case ConsoleKey.S:
                case ConsoleKey.DownArrow:
                    if(markedIndex < abilityList.Count -1 )
                    {
                        markedIndex++;  
                    }
                    break;
                case ConsoleKey.Enter:
                    if(abilityList[markedIndex].CurrentCooldown == abilityList[markedIndex].CoolDownTimer)
                    {
                        returnAbility = abilityList[markedIndex];
                        stillChoosing = false;
                    }
                    break;
            }
            Console.Clear();
        }
        return returnAbility;
    }
public Character ChooseEnemyTarget(List<Character> potentialTargets)
{
    int markedIndex = 0;
    bool stillChoosing = true;

    while (stillChoosing)
    {
        Console.Clear();
        Console.WriteLine("Who do you want to attack?");
        
        for (int i = 0; i < potentialTargets.Count; i++)
        {
            
            Character target = potentialTargets[i];
            if (i == markedIndex)
            {
                Utilities.ConsoleWriteColor("*", ConsoleColor.Blue);
                Utilities.ConsoleWriteColor(target.Name, ConsoleColor.DarkRed);
                Utilities.ConsoleWriteColor($"   [{target.CurrentHealth}/{target.MaxHealth}]",ConsoleColor.Red);
                CombatHandler.PrintAllEffectIcons(target);
                Utilities.ConsoleWriteLineColor("*", ConsoleColor.Blue);
            }
            else
            {
                Utilities.ConsoleWriteColor(target.Name, ConsoleColor.DarkRed);
                Utilities.ConsoleWriteColor($"   [{target.CurrentHealth}/{target.MaxHealth}]",ConsoleColor.Red);
                CombatHandler.PrintAllEffectIcons(target);
                Console.WriteLine();
            }
        }

        ConsoleKeyInfo pressedKey = Console.ReadKey(true);
        switch (pressedKey.Key)
        {
            case ConsoleKey.W:
            case ConsoleKey.UpArrow:
                if (markedIndex > 0) markedIndex--;
                break;
            case ConsoleKey.S:
            case ConsoleKey.DownArrow:
                if (markedIndex < potentialTargets.Count - 1) markedIndex++;
                break;
            case ConsoleKey.Enter:
                stillChoosing = false;
                break;
        }
    }
    
    return potentialTargets[markedIndex];
}

    public void UpdateCombatState()
    {
        //Dont implement for player
        throw new NotImplementedException();
    }

    public Ability ChooseOffensiveAbility()
    {
        //Dont implement for player
        throw new NotImplementedException();
    }

    public Ability ChooseDefensiveAbility()
    {
        //Dont implement for player
        throw new NotImplementedException();
    }

    public Ability ChooseSupportiveAbility()
    {
        //Dont implement for player
        throw new NotImplementedException();
    }

    public Character ChooseFriendlyTarget(List<Character> potentialTargets)
    {
        throw new NotImplementedException();
    }
}