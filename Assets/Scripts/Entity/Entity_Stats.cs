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


    public float getEvasion()
    {
        float baseEvasion = defence.evasion.GetValue();
        float bonusEvasion = major.agility.GetValue() * 0.5f;
        float evasionLimit = 30f;


        float totalEvasion = baseEvasion + bonusEvasion;
        return Mathf.Clamp(totalEvasion, 0, evasionLimit);
    }
}
