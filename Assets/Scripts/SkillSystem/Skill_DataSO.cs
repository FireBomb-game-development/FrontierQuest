using UnityEngine;

[CreateAssetMenu(menuName ="RPG Setup/Skill Data" , fileName ="Skill data")]
public class Skill_DataSO :ScriptableObject
{


public int cost;
[Header("Skill details")]
public string header;
[TextArea]
public string description;
public Sprite icon;


}
