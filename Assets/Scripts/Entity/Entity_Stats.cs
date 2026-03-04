
using Unity.Mathematics;
using UnityEngine;

public class EntityStats : MonoBehaviour
{
    public Stat_MajorGroup major;
    public Stat_DefenceGroup defence;
    public Stat_OffenceGroup offence;
    public Stat maxHealth;




    public float GetElementalDamage(out ElementalType element)
    {
        float fireDamage = offence.fireDamage.GetValue();
        float iceDamage = offence.iceDamage.GetValue();
        float lightningDamage = offence.lightningDamage.GetValue();
        
        float bonusElemntalDamage = major.intelegence.GetValue();// bonus 1 elemental damage for each intelegence point

        float highestDamage = fireDamage;

        element = ElementalType.Fire;

        if(iceDamage>highestDamage){
            highestDamage = iceDamage;
            element = ElementalType.Ice;
        }

        if(lightningDamage > highestDamage){
            highestDamage = lightningDamage;
            element = ElementalType.Lightning;
        }
        
        if(highestDamage <=0){ 
            element = ElementalType.None;
            return 0;
        }
        
        float bonusFire =(fireDamage ==highestDamage)? 0: fireDamage *= 0.5f;
        float bonusIce =(iceDamage ==highestDamage)? 0: iceDamage *= 0.5f;
        float bonusLightning =(lightningDamage ==highestDamage)? 0: lightningDamage *= 0.5f;

        float weakerElementDamage = bonusFire + bonusIce + bonusLightning;
        float finalDamage = highestDamage + weakerElementDamage + bonusElemntalDamage;

        return finalDamage;
    }

    public float GetElementalResistence(ElementalType element)
    {
       float baseElementalResistence = 0;
       float bonusElementalResistemce = major.intelegence.GetValue() * .5f; // 0.5 bonus resistence point for each intelegnce point

        switch (element)
        {
            case ElementalType.Fire:
                baseElementalResistence = defence.fireRes.GetValue();
                break;
            case ElementalType.Ice:
                baseElementalResistence = defence.iceRes.GetValue();
                break;
            case ElementalType.Lightning:
                baseElementalResistence = defence.lightningRes.GetValue();
                break;

        }
        float resistence = baseElementalResistence + bonusElementalResistemce;
        float resistenceCap = 75f; // max resistence for element is 75%;
        float finalResistence = math.clamp(resistence,0,resistenceCap)/100;

        return finalResistence;
    }
    public float GetMaxHealth()
    {

        float baseHp = maxHealth.GetValue();
        float bonusHp = major.vitality.GetValue();
        return baseHp + bonusHp * 5;
    }



    public float GetArmorMitigation(float aramorReduction)
    {
        float baseArmor = defence.armor.GetValue();
        float bonusArmor = major.strength.GetValue();
        float totaArmor = baseArmor + bonusArmor;
        
        float reductionMultiplier = Mathf.Clamp(1 - aramorReduction,0,1);
        float effectiveArmor = totaArmor * reductionMultiplier;

        float mitigation = effectiveArmor / (effectiveArmor +100);
        float mitigationCap = 0.85f; // Max mitigation is 85%\
        float finalMitigation = Mathf.Clamp(mitigation,0,mitigationCap); // lock mitigation between min and max values
        return finalMitigation;


    }

    public float GetArmorReduction()
    {
        float finalReduction =  offence.aramorReduction.GetValue()/100;
        return finalReduction;

    }
    public float getEvasion()
    {
        float baseEvasion = defence.evasion.GetValue();
        float bonusEvasion = major.agility.GetValue() * 0.5f;
        float evasionLimit = 30f;


        float totalEvasion = baseEvasion + bonusEvasion;
        return Mathf.Clamp(totalEvasion, 0, evasionLimit);
    }

    public float GetPhyisicalDamage(out bool isCrit, float scaleFactor = 1)
    {
        float baseDamage = offence.damage.GetValue();
        float bonusDamage = major.strength.GetValue(); // Bonus damage from Strength: +1 per STR
        float totalBaseDamage = baseDamage + bonusDamage;

        float baseCritChance = offence.critChance.GetValue();
        float bonusCritChance = major.agility.GetValue() * .3f; //  Bonus crit chance from Agility: +0.3% per AGI 
        float critChance = baseCritChance + bonusCritChance;

        float baseCritPower = offence.critPower.GetValue();
        float bonusCritPower = major.strength.GetValue() * .5f; // Bonus crit chance from Strength: +0.5% per STR 
        float critPower = (baseCritPower + bonusCritPower) / 100; // Total crit power as multiplier ( e.g 150 / 100 = 1.5f - multiplier)

        isCrit = UnityEngine.Random.Range(0, 100) < critChance;
        float finalDamage = isCrit ? totalBaseDamage * critPower : totalBaseDamage;

        return finalDamage * scaleFactor;
    }

}
