
using System;
using UnityEditor.Experimental.GraphView;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_TreeNode : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{

    
    private UI ui;
    private RectTransform rect;
    public UI_SkillTree skillTree;


    [Header("Unlock details")]

    public UI_TreeNode [] necessaryNodes;
    public UI_TreeNode [] conflictNodes;
    public bool isLocked;
    public bool isUnlocked;


    [Header("Skill details")]


    public Skill_DataSO skillData;
    [SerializeField] private String skillName;
    [SerializeField] private Image skillIcon;
    [SerializeField] private int skillCost;
    [SerializeField] private String lockedColorHex = ("#656565");
    private Color lastColor;


    private void Awake()
    {
        ui = GetComponentInParent<UI>();
        rect = GetComponent<RectTransform>();
        UpdateIconColor(GetColorByHex(lockedColorHex));
        skillTree = GetComponentInParent<UI_SkillTree>();
    }


    private void Unlock()
    {
        isUnlocked = true;
        UpdateIconColor(Color.white);
        skillTree.RemoveSkillPoints(skillData.cost);
        LockConflictsNodes();
        //findPlayer_Skill manager;
        // Unlock skill on skill manger;
    }

    private bool CanBeUnlocked()
    {
        if(isLocked || isUnlocked)return false;
        if(!skillTree.EnoughSkillPoints(skillData.cost))return false;
        foreach(var node in necessaryNodes) if(!node.isUnlocked)return false;
        foreach(var node in conflictNodes) if(node.isUnlocked) return false;
        return true;
    }

    private void LockConflictsNodes() {foreach(UI_TreeNode conflictNode in conflictNodes) conflictNode.isLocked = true;}


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
        ui.skillToolTip.ShowToolTip(true,rect,this);
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
        skillCost = skillData.cost;
        gameObject.name = "UI_TreeNode - " + skillData.header;
    }
}
