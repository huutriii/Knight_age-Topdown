using UnityEngine;
public class MonsterStateBase : MonoBehaviour
{
    protected Animator animator;
    protected MonsterController monster;

    public MonsterStateBase(MonsterController monster, Animator animator) => (this.animator, this.monster) = (animator, monster);

    protected void ResetAllAnimations()
    {
        animator.SetBool(STATE.idle, false);
        animator.SetBool(STATE.run, false);
        animator.SetBool(STATE.hurt, false);
        animator.SetBool(STATE.attack, false);
        animator.SetBool(STATE.died, false);

    }
}