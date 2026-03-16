
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class EntityStats : MonoBehaviour
{
    public Stat_SetupSO setupStatSetup;

    public Stat_ResourceGroup resources;
    public Stat_DefenseGroup defense;
    public Stat_OffenceGroup offense;

    public Stat_MajorGroup major;



    public float GetElementalDamage(out ElementalType element, float scaleFactor =1)
    {
        float fireDamage = offense.fireDamage.GetValue();
        float iceDamage = offense.iceDamage.GetValue();
        float lightningDamage = offense.lightningDamage.GetValue();
        
        float bonusElemntalDamage = major.intelligence.GetValue();// bonus 1 elemental damage for each intelegence point

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
        
        float bonusFire =(element == ElementalType.Fire)? 0: fireDamage *= 0.5f;
        float bonusIce =(element == ElementalType.Ice)? 0: iceDamage *= 0.5f;
        float bonusLightning =(element == ElementalType.Lightning)? 0: lightningDamage *= 0.5f;

        float weakerElementDamage = bonusFire + bonusIce + bonusLightning;
        float finalDamage = highestDamage + weakerElementDamage + bonusElemntalDamage;

        return finalDamage * scaleFactor;
    }

    public float GetElementalResistence(ElementalType element)
    {
       float baseElementalResistence = 0;
       float bonusElementalResistemce = major.intelligence.GetValue() * .5f; // 0.5 bonus resistence point for each intelegnce point

        switch (element)
        {
            case ElementalType.Fire:
                baseElementalResistence = defense.fireRes.GetValue();
                break;
            case ElementalType.Ice:
                baseElementalResistence = defense.iceRes.GetValue();
                break;
            case ElementalType.Lightning:
                baseElementalResistence = defense.lightningRes.GetValue();
                break;

        }
        float resistence = baseElementalResistence + bonusElementalResistemce;
        float resistenceCap = 75f; // max resistence for element is 75%;
        float finalResistence = math.clamp(resistence,0,resistenceCap)/100;

        return finalResistence;
    }
    public float GetMaxHealth()
    {

        float baseHp = resources.maxHealth.GetValue();
        float bonusHp = major.vitality.GetValue();
        return baseHp + bonusHp * 5;
    }



    public float GetArmorMitigation(float aramorReduction)
    {
        float baseArmor = defense.armor.GetValue();
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
        float finalReduction =  offense.armorReduction.GetValue()/100;
        return finalReduction;

    }
    public float getEvasion()
    {
        float baseEvasion = defense.evasion.GetValue();
        float bonusEvasion = major.agility.GetValue() * 0.5f;
        float evasionLimit = 30f;


        float totalEvasion = baseEvasion + bonusEvasion;
        return Mathf.Clamp(totalEvasion, 0, evasionLimit);
    }

    public float GetPhyisicalDamage(out bool isCrit, float scaleFactor = 1)
    {
        float baseDamage = offense.damage.GetValue();
        float bonusDamage = major.strength.GetValue(); // Bonus damage from Strength: +1 per STR
        float totalBaseDamage = baseDamage + bonusDamage;

        float baseCritChance = offense.critChance.GetValue();
        float bonusCritChance = major.agility.GetValue() * .3f; //  Bonus crit chance from Agility: +0.3% per AGI 
        float critChance = baseCritChance + bonusCritChance;

        float baseCritPower = offense.critPower.GetValue();
        float bonusCritPower = major.strength.GetValue() * .5f; // Bonus crit chance from Strength: +0.5% per STR 
        float critPower = (baseCritPower + bonusCritPower) / 100; // Total crit power as multiplier ( e.g 150 / 100 = 1.5f - multiplier)

        isCrit = UnityEngine.Random.Range(0, 100) < critChance;
        float finalDamage = isCrit ? totalBaseDamage * critPower : totalBaseDamage;

        return finalDamage * scaleFactor;
    }

    public Stat GetStatByType(StatType type)
    {
        switch (type)
        {
            case StatType.MaxHealth: return resources.maxHealth;
            case StatType.HealthRegen: return resources.healthRegen;
            case StatType.Strength: return major.strength;
            case StatType.Agility: return major.agility;
            case StatType.Intelligence: return major.intelligence;
            case StatType.Vitality: return major.vitality;
            case StatType.AttackSpeed: return offense.attackSpeed;
            case StatType.Damage: return offense.damage;
            case StatType.CritChance: return offense.critChance;
            case StatType.CritPower: return offense.critPower;
            case StatType.ArmorReduction: return offense.armorReduction;
            case StatType.FireDamage: return offense.fireDamage;
            case StatType.IceDamage: return offense.iceDamage;
            case StatType.LightningDamage: return offense.lightningDamage;
            case StatType.Armor: return defense.armor;
            case StatType.Evasion: return defense.evasion;
            case StatType.IceResistance: return defense.iceRes;
            case StatType.FireResistance: return defense.fireRes;
            case StatType.LightningResistance: return defense.lightningRes;
            default:
                Debug.LogWarning($"statType {type} not implemented yet."); return null;
        }
    }


    [ContextMenu("Update Default Stat Setup")]
    public void ApplyDefaultStatSetup()
    {
        if(setupStatSetup == null)
        {
            Debug.Log("no defualt stat setup assigned");
            return;
        }
        resources. maxHealth.SetBaseValue(setupStatSetup.maxHealth);
        resources. healthRegen. SetBaseValue(setupStatSetup.healthRegen);
        major.strength.SetBaseValue(setupStatSetup.strength);
        major. agility.SetBaseValue(setupStatSetup.agility);
        major.intelligence. SetBaseValue(setupStatSetup.intelligence);
        major.vitality.SetBaseValue(setupStatSetup.vitality);
        offense.attackSpeed.SetBaseValue(setupStatSetup.attackSpeed);
        offense.damage.SetBaseValue(setupStatSetup.damage);
        offense.critChance.SetBaseValue(setupStatSetup.critChance);
        offense.critPower. SetBaseValue(setupStatSetup.critPower);
        offense.armorReduction.SetBaseValue(setupStatSetup.armorReduction);
        offense.iceDamage.SetBaseValue(setupStatSetup.iceDamage);
        offense.fireDamage.SetBaseValue(setupStatSetup.fireDamage);
        offense.lightningDamage.SetBaseValue(setupStatSetup.lightningDamage);
        defense.armor.SetBaseValue(setupStatSetup.armor);
        defense.evasion.SetBaseValue(setupStatSetup.evasion);
        defense.iceRes.SetBaseValue(setupStatSetup.iceResistance);
        defense.fireRes.SetBaseValue(setupStatSetup.fireResistance);
        defense.lightningRes.SetBaseValue(setupStatSetup.LightningResistance);
    }
}
