
using System;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_TreeNode : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{

    private UI ui;
    private RectTransform rect;

    [SerializeField] private Skill_DataSO skillData;
    [SerializeField] private String skillName;

    [SerializeField] private Image skillIcon;
    [SerializeField] private String lockedColorHex = ("#656565");
    [SerializeField] private Color lastColor;
    public bool isUnlocked;
    public bool isLocked;

    private void Awake()
    {
        ui = GetComponentInParent<UI>();
        rect = GetComponent<RectTransform>();
         UpdateIconColor(GetColorByHex(lockedColorHex));
    }


    private void Unlock()
    {
        isUnlocked = true;
        UpdateIconColor(Color.white);
        //findPlayer_Skill manager;
        // Unlock skill on skill manger;
    }

    private bool CanBeUnlocked()
    {
        if(isLocked || isUnlocked)return false;
        return true;
    }

    private void UpdateIconColor(Color color)
    {
        if(skillIcon == null)
        {
            Debug.Log("Error: skill icon is null");
            return;
        }
        lastColor = skillIcon.color;
        skillIcon.color = color;
    }



    public void OnPointerDown(PointerEventData eventData)
    {
        if(CanBeUnlocked())Unlock();
        else Debug.Log("Cannot be unlocked");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ui.skillToolTip.ShowToolTip(true,rect,skillData);
        if(!isUnlocked)UpdateIconColor(Color.white * 0.9f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ui.skillToolTip.ShowToolTip(false,rect);
        if(!isUnlocked)UpdateIconColor(lastColor);
    }

    private Color GetColorByHex(String hexNumber)
    {
        ColorUtility.TryParseHtmlString(hexNumber, out Color color);
        return color ;
    }

    private void OnValidate()
    {
        if(skillData == null)return;
        skillName = skillData.header;
        skillIcon.sprite = skillData.icon;
        gameObject.name = "UI_TreeNode - " + skillData.header;
    }
}
