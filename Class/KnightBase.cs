using UnityEngine;

public abstract class KnightBase : MonoBehaviour
{
    protected float maxHP;
    protected float maxMP;
    protected float exp;
    protected float speed;
    protected float baseEXP;
    protected int _level;
    protected int level
    {
        get => _level;
        set
        {
            if (exp >= baseEXP * level)
            {
                _level++;
            }
        }
    }

    protected float dame;
    protected float distanceSkill;

    protected float defense;
    protected float dexterity;

}
