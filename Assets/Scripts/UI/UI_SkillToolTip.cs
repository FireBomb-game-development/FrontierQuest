using UnityEngine;
using TMPro;
using System;
using System.Text;

public class UI_SkillToolTip : Ui_ToolTip
{   private UI_SkillTree skillTree;

    [SerializeField] private TextMeshProUGUI skillName;
    [SerializeField] private TextMeshProUGUI skillDescription;
    [SerializeField] private TextMeshProUGUI skillRequirements;

    [Space]

    [SerializeField] private string metConditionHex;
    [SerializeField] private string notMetConditionHex;
    [SerializeField] private string importentInfoHex;
    [SerializeField] private Color exampleColor;
    [SerializeField] private string lockedSkillText = "Different skill path chosen, skill is now locked.";




    private protected override void Awake()
    {
        base.Awake();
        skillTree = GetComponentInParent<UI_SkillTree>();
    }

    public override void ShowToolTip(bool show, RectTransform targetRect)
    {
        base.ShowToolTip(show, targetRect);
    }
    public void ShowToolTip(bool show, RectTransform targetRect,UI_TreeNode node)
    {
        base.ShowToolTip(show,targetRect);
        if(!show)return;
        
        skillName.text =node.skillData.header;
        skillDescription.text = node.skillData.description;
        
        string skillLockedText = $"<color={importentInfoHex}>- {lockedSkillText}</color>";
        string requierments = node.isLocked ? skillLockedText: GetRequirements(node.skillData.cost,node.necessaryNodes, node.conflictNodes);

        skillRequirements.text = requierments;

    }




    private string GetRequirements( int skillCost, UI_TreeNode[] necessaryNodes, UI_TreeNode[] conflictNodes)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("Requirements");
        string costColor = skillTree.EnoughSkillPoints(skillCost)?metConditionHex:notMetConditionHex;
        sb.AppendLine($"<color={costColor}>- {skillCost} skill point(s) </color>");

        foreach(UI_TreeNode necessarySkill in necessaryNodes){
            string nodeColor = necessarySkill.isUnlocked? metConditionHex:notMetConditionHex; 
            sb.AppendLine($"<color={nodeColor}>- {necessarySkill.skillData.header}</color>");
        }
        
        if(conflictNodes.Length <=0)return sb.ToString();

        sb.AppendLine();
        sb.AppendLine($"<color={importentInfoHex}>locks: </color>");
        foreach(UI_TreeNode conflictSkill in conflictNodes)
        {
            sb.AppendLine($"<color={importentInfoHex}>- {conflictSkill.skillData.header}</color>");
        }
        return(sb.ToString());
        
    }
}
