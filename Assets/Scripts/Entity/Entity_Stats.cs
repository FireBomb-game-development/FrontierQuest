using UnityEngine;

public class EntityStats : MonoBehaviour
{
    public Stat_MajorGroup major;
    public Stat_DefenceGroup defence;
    public Stat_OffenceGroup offence;
    public Stat maxHealth;





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
}
