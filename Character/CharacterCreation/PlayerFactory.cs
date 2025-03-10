public static class PlayerFactory
{



    public static List<Character> GenerateParty()
    {
        List<string> fullPartyOptions = ["yes","no"];
        List<Character> returnList = new();
        int index = Utilities.PickIndexFromList(fullPartyOptions,"Do you want to create a Custom party?");
        if(index == 0)
        {
            List<string> characterOption = ["Pyromancer","Priest","Rogue","Warrior"];
            for(int i = 0; i < 4;i++)
            {
                Utilities.ConsoleWriteLineColor("Create your party",ConsoleColor.DarkGreen);
                index = Utilities.PickIndexFromList(characterOption,$"Pick character nr {i+1}",ConsoleColor.DarkGreen);
                switch(index)
                {
                    case 0:
                        returnList.Add(GenerateFireMage("Pyromancer"));
                        break;
                    case 1:
                        returnList.Add(GeneratePriest("Priest"));
                        break;
                    case 2:
                        returnList.Add(GenerateRogue("Rogue"));
                        break;
                    case 3:
                        returnList.Add(GenerateWarrior("Warrior"));
                        break;
                }
            }
           
        }
        else 
        {
            returnList.Add(GenerateFireMage("Pyromancer"));
            returnList.Add(GeneratePriest("Priest"));
            returnList.Add(GenerateRogue("Rogue"));
            returnList.Add(GenerateWarrior("Warrior"));
            
        }
        return returnList;
    }
    //Power == % increase in magnitude on abilities and effects
    //10% reduction: 5.56 armor
    //20% reduction: 12.5 armor
    //30% reduction: 21.43 armor
    //40% reduction: 33.33 armor
    //50% reduction: 50.0 armor
    //60% reduction: 75.0 armor
    //70% reduction: 116.67 armor
    //80% reduction: 200.0 armor
    //90% reduction: 450.0 armor
    public static Player GenerateFireMage(string name)
    { 
        //Offensive
        Ability fireBolt = new("Firebolt",eTargetType.Enemy,0,eAbilityType.Offensive);
        fireBolt.Description = "Basic firespell dealing low damage to a target";
        fireBolt.AddDamageEffect(10,false);
        Ability ignite = new("Ignite",eTargetType.Enemy,0,eAbilityType.Offensive);
        ignite.Description = "Ignites one target for moderate damage over 5 rounds";
        ignite.AddBurnEffect(5,4,false);
        //OffensiveStrong
        Ability fireBall = new("FireBall",eTargetType.Enemy,3,eAbilityType.OffensiveStrong);
        fireBall.Description = "Hurl a ball of fire towards an enemy dealing moderate damage";
        fireBall.AddDamageEffect(20,false);
        Ability massIgnite = new("Mass Ignite",eTargetType.AnyEnemy,5,eAbilityType.OffensiveStrong);
        massIgnite.Description = "Ignites all enemies triggering a powerful burn for 2 rounds";
        massIgnite.AddBurnEffect(2,5,true);
        //Defensive
        Ability moltenArmor = new("Molten Armor",eTargetType.Self,5,eAbilityType.DefensiveSelf);
        moltenArmor.Description = "Surrounds the caster with molten lava drasticly increasing armor for 2 rounds";
        moltenArmor.AddArmorBuffEffect(30,2,false);
        //Supportive
        Ability supressFlames = new("Supress Flames",eTargetType.AnyFriend,2,eAbilityType.CleanseOther);
        supressFlames.Description = "Removes all burn effects from friendly targets";
        supressFlames.AddCleanseEffect(eCombatEffect.Burn,true);
        List<Ability> abilityList = new()
        {
            fireBolt,
            ignite,
            fireBall,
            moltenArmor,
            supressFlames,
            massIgnite
        };
        Player returnCharacter = new(name,100,20,10);
        returnCharacter.Abilities = abilityList;

        return returnCharacter;
    }
    public static Player GeneratePriest(string name)
    { 
        // Basic Offensive Ability
        Ability smite = new("Smite", eTargetType.Enemy, 0, eAbilityType.Offensive);
        smite.Description = "Smite the Enemy target with holy damage";
        smite.AddDamageEffect(8,false); // Light damage, 0 cooldown

        // Supportive Abilities
        Ability heal = new("Heal", eTargetType.Friendly, 1, eAbilityType.HealingAny);
        heal.Description = "Heal any friendly target moderatly";
        heal.AddHealingEffect(15,false); // Basic healing ability

        Ability renew = new("Renew", eTargetType.Friendly, 0, eAbilityType.HealingAny);
        renew.Description = $"Adds HoT to Any friendly target for 3 rounds";
        renew.AddHealingOverTimeEffect(5, 3,false); // Healing over time for 3 turns
        
        Ability purify = new("Purify", eTargetType.Friendly, 2, eAbilityType.CleanseOther);
        purify.Description = "Removes all posion from a friend";
        purify.AddCleanseEffect(eCombatEffect.Poison,false); // Cleanse poison debuff

        // Defensive Ability
        Ability holyBarrier = new("Holy Barrier", eTargetType.Self, 4, eAbilityType.DefensiveSelf);
        holyBarrier.Description = "A strong buff that increases casters Armor temporarily";
        holyBarrier.AddArmorBuffEffect(20, 2,false); // Temporary armor buff for self

        // Ability List
        List<Ability> abilityList = new()
        {
            smite,
            heal,
            renew,
            purify,
            holyBarrier
        };

        // Creating the Priest Character
        Player returnCharacter = new(name, 80, 15, 12); // Adjusted health, power, and armor for a priest character
        returnCharacter.Abilities = abilityList;

        return returnCharacter;
    }
    public static Player GenerateRogue(string name)
    { 
        // Offensive Abilities
        Ability quickStrike = new("Quick Strike", eTargetType.Enemy, 0, eAbilityType.Offensive);
        quickStrike.Description = "A swift attack to deal light damage to a single target";
        quickStrike.AddDamageEffect(8,false); // Low damage, 0 cooldown

        Ability poisonDagger = new("Poison Dagger", eTargetType.Enemy, 2, eAbilityType.Offensive);
        poisonDagger.Description = "Poisons the enemy target, dealing damage over time";
        poisonDagger.AddPoisonEffect(3, 5,false); // Applies poison for 3 turns, dealing 5 damage each round
        //Offensive strong
        Ability backstab = new("Backstab", eTargetType.Enemy, 3, eAbilityType.OffensiveStrong);
        backstab.Description = "Deals a powerful blow to a single target from behind";
        backstab.AddDamageEffect(20,false); // High damage ability with a cooldown

        // Defensive Abilities
        Ability refillFlasks = new("Refill flasks", eTargetType.Self, 8, eAbilityType.DefensiveSelf);
        refillFlasks.Description = "Regenerates health over 5 rounds";
        refillFlasks.AddHealingOverTimeEffect(3,5,false);

        // Supportive Abilities
        Ability shadowstep = new("Shadowstep", eTargetType.Self, 5, eAbilityType.DefensiveSelf);
        shadowstep.Description = "Allows the Rogue to become immune to damage for 1 round";
        shadowstep.AddImmuneEffect(1,false); // Makes Rogue immune for 1 round

        List<Ability> abilityList = new()
        {
            quickStrike,
            poisonDagger,
            refillFlasks,
            backstab,
            shadowstep
        };

        Player returnCharacter = new(name, 75, 18, 8); // Adjusted health, power, and armor for a Rogue
        returnCharacter.Abilities = abilityList;

        return returnCharacter;
    }

    public static Player GenerateWarrior(string name)
    { 
        // Offensive Abilities
        Ability strike = new("Strike", eTargetType.Enemy, 0, eAbilityType.Offensive);
        strike.Description = "A basic melee attack that deals solid damage to the target";
        strike.AddDamageEffect(10,false); // Basic attack with no cooldown

        Ability shieldBash = new("Shield Bash", eTargetType.Enemy, 2, eAbilityType.Offensive);
        shieldBash.Description = "Bashes the target with a shield, causing moderate damage";
        shieldBash.AddDamageEffect(12,false); // Deals moderate damage and stuns

        // Defensive Abilities
        Ability fortify = new("Fortify", eTargetType.Self, 4, eAbilityType.DefensiveSelf);
        fortify.Description = "Temporarily raises armor to reduce incoming damage";
        fortify.AddArmorBuffEffect(25, 2,false); // Increases armor significantly for 2 turns

        Ability commandingShout = new("Commanding Shout", eTargetType.AnyEnemy, 3, eAbilityType.OffensiveAOE);
        commandingShout.Description = "Screams at ALL nearby enemies causing them to likely target the caster";
        commandingShout.AddDamageEffect(10,true); // Reflects a portion of damage back to the attacker
        commandingShout.AddThreatEffect(300,true);

        // Supportive Abilities
        Ability rally = new("Rally", eTargetType.Self, 5, eAbilityType.HealingSelf);
        rally.Description = "Gradually restores health over 3 rounds to the Warrior";
        rally.AddHealingOverTimeEffect(5, 3,false); // Heals the Warrior over time

        List<Ability> abilityList = new()
        {
            strike,
            shieldBash,
            fortify,
            commandingShout,
            rally
        };

        Player returnCharacter = new(name, 120, 15, 20); // Adjusted stats for a tanky Warrior
        returnCharacter.Abilities = abilityList;

        return returnCharacter;
    }
        public static Player GenerateSummoner(string name)
    { 
        // Offensive Abilities
        Ability summonGoblin = new("Summon Goblin", eTargetType.Self, 0, eAbilityType.DefensiveSelf);
        summonGoblin.Description = "A basic melee attack that deals solid damage to the target";
        summonGoblin.AddSummonEffect(eEnemyFamily.Goblin,eEnemyType.Minion,0,3); // Basic attack with no cooldown


        List<Ability> abilityList = new()
        {
            summonGoblin
        };

        Player returnCharacter = new(name, 120, 15, 20); // Adjusted stats for a tanky Warrior
        returnCharacter.Abilities = abilityList;

        return returnCharacter;
    }






}