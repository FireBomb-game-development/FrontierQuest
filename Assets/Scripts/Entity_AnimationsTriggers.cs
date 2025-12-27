using UnityEngine;

public class Entity_AnimationsTriggers : MonoBehaviour
{

    private Entity entity;

    private void Awake()
    {
        entity = GetComponentInParent<Entity>();
    }
    private void CurrentStateTrigger()
    {
        entity.CurrentStateAnimationTrigger();
    }
}
