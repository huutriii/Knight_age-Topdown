using UnityEngine;
public class MonsterStateBase : MonoBehaviour
{
    protected Animator animator;
    protected MonsterController monster;

    public MonsterStateBase(MonsterController monster, Animator animator) => (this.animator, this.monster) = (animator, monster);

    protected void ResetAllAnimations()
    {
        animator.SetBool(StateContaint.idle, false);
        animator.SetBool(StateContaint.run, false);
        animator.SetBool(StateContaint.hurt, false);
        animator.SetBool(StateContaint.attack, false);
        animator.SetBool(StateContaint.died, false);

    }
}