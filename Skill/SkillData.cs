
using UnityEngine;

[CreateAssetMenu(fileName = "Flame_", menuName = "System skill/ Skill")]
public class SkillData : ScriptableObject
{
    public string skillName;
    public GameObject skillPrefab;
    public float countDown;
    public float dame;
    public float mpUssage;
    public Sprite icon;
}
