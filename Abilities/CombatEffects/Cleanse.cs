public class Cleanse : CombatEffect
{
    public List<eCombatEffect> TypesToCleanse {get;set;}
    public Cleanse(List<eCombatEffect> typesToCleanse,bool areaEffect) : base(1,1,eCombatEffect.Cleanse,areaEffect)
    {
        TypesToCleanse = typesToCleanse;
    }

    //Clears all the abilities in "TypesToCleanse" on the target
    public override void ApplyEffect(Character caster,Character target)
    {   
        List<Character> affectedCharacters = new();
        if(AreaEffect)
        {
            affectedCharacters = target.FriendList;
        }
        else affectedCharacters.Add(target);
        foreach(Character c in affectedCharacters)
        {
            bool foundEffect = false;
            for(int i = 0 ; i< c.CurrentStatusEffects.Count;i++)
            {
                
                foreach(eCombatEffect effect in TypesToCleanse)
                {
                    if(c.CurrentStatusEffects[i].Type == effect)
                    {
                        c.ClearEffect(c.CurrentStatusEffects[i]);
                        foundEffect = true;
                    }
                }
                
            }
            if(!foundEffect) Console.WriteLine("But had no effect");
        }
    }
    public override CombatEffect CloneEffect()
    {
        return new Cleanse(TypesToCleanse,AreaEffect);
    }
}