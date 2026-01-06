using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Player_Combat : Entity_Combat
{

    [Header("counter attack details")]
    [SerializeField] private float counterRecovery = .1f;

    public bool CounterAttackPerformed()
    {
        bool hasPerformedCounter = false;
        foreach (var target in GetDetectedColiders())
        {
            ICounterable counterable = target.GetComponent<ICounterable>();


            if (counterable == null) continue;
            if (counterable.CanBeCountered)
            {
                counterable.handleCounter();
                hasPerformedCounter = true;
            }

        }
        return hasPerformedCounter;
    }

    public float GetCounterDuration() => counterRecovery;
}
