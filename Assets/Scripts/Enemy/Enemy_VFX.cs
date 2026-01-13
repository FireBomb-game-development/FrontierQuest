using UnityEngine;

public class Enemy_VFX : Entity_VFX
{
    [Header(" Counter Attack Window")]
    [SerializeField] private GameObject attackAlert;
    [SerializeField] private bool showAlert = true;



    public void SetAttackAlert(bool enable)
    {
        if (showAlert) attackAlert.SetActive(enable);
    }
}
