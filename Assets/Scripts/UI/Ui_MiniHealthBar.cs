using UnityEngine;

public class Ui_MiniHealthBar : MonoBehaviour
{

    private Entity entity;
    private void Awake()
    {
        entity = GetComponentInParent<Entity>();

    }
    private void OnEnable()
    {
        entity.OnFlipped += HandleFlip;
    }
    private void OnDisable()
    {
        entity.OnFlipped -= HandleFlip;    
    }

    // Update is called once per frame
    private void HandleFlip()=>transform.rotation = Quaternion.identity;
    
        
    
}
