using UnityEngine;

public class Enemy_AnimationTriggers : Entity_AnimationsTriggers
{

    private Enemy enemy;
    private Enemy_VFX enemy_Vfx;
    protected override void Awake()
    {
        base.Awake();
        enemy = GetComponentInParent<Enemy>();
        enemy_Vfx = GetComponentInParent<Enemy_VFX>();
        
    }
 
    private void EnableCounterWindow()
    {
        enemy_Vfx.SetAttackAlert(true);
        enemy.SetCounterWindow(true);
    }
    private void disableAttackWindow()
    {
        enemy_Vfx.SetAttackAlert(false);
        enemy.SetCounterWindow(false);
    }

}
