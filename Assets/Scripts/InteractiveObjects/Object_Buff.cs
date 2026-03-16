using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Assertions.Comparers;

[System.Serializable]
public class Buff
{
    public StatType type;
    public float value;
}



public class Object_Buff : MonoBehaviour
{


private SpriteRenderer sr;
private EntityStats statsToModify;

[Header("Buff Details")]
[SerializeField] private Buff[] Buffs;
[SerializeField] private string buffName;
[SerializeField] private float buffValue =5;

[SerializeField] private float buffDuration =4;
[SerializeField] private bool canBeUsed = true;

[Header("Floaty Movement")]
[SerializeField] private float floatSpeed = 1f;
[SerializeField]private float floatRange = .1f;
private Vector3 startPos;


    public void Awake()
    {
        startPos = transform.position;
        sr = GetComponentInChildren<SpriteRenderer>();
    }


    private void Update()
    {
        float yOffeset = Mathf.Sin(Time.time *floatSpeed)*floatRange;
        transform.position = startPos + new Vector3 (0,yOffeset);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!canBeUsed)return;      
        statsToModify = collision.GetComponent<EntityStats>();
        StartCoroutine(BuffCo(buffDuration));
    }

    private IEnumerator BuffCo(float duration)
    {
        canBeUsed = false;
        sr.color = Color.clear;
        ApplyBuff(true);

        yield return new WaitForSeconds(duration);

        ApplyBuff(false);
        Destroy(gameObject);
    }

    private void ApplyBuff(bool apply)
    {
        foreach(var buff in Buffs)
        {
            Debug.Log("buff type" +buff.type);
            if(apply)statsToModify.GetStatByType(buff.type).AddModifier(buff.value, buffName);
            else statsToModify.GetStatByType(buff.type).RemoveModifier(buffName);
        }
    }
}
