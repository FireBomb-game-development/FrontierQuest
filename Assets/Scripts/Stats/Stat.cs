using System;
using UnityEngine;

[Serializable]
public class Stat
{
    [SerializeField] public float baseValue;

    public float GetValue()
    {
        return baseValue;
    }

}
