using UnityEngine;
using TMPro;

public class UI_SkillToolTip : Ui_ToolTip
{

[SerializeField] private TextMeshProUGUI skillName;
[SerializeField] private TextMeshProUGUI skillDescription;
[SerializeField] private TextMeshProUGUI skillRequirements;



    public override void ShowToolTip(bool show, RectTransform targetRect)
    {
        base.ShowToolTip(show, targetRect);
    }

    public void ShowToolTip(bool show, RectTransform targetRect,Skill_DataSO skillData)
    {
        base.ShowToolTip(show,targetRect);
        if(!show)return;
        skillName.text =skillData.header;
        skillDescription.text = skillData.description;
        skillRequirements.text = "Requierments: \n "
        +" -"+ skillData.cost + " skill point";
    }



}
