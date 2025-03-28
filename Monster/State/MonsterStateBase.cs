using UnityEngine;
public class MonsterStateBase : MonoBehaviour
{
    protected Animator animator;
    protected MonsterController monster;

    public MonsterStateBase(MonsterController monster, Animator animator) => (this.animator, this.monster) = (animator, monster);

    protected void ResetAllAnimations()
    {
        animator.SetBool(StateConstant.idle, false);
        animator.SetBool(StateConstant.run, false);
        animator.SetBool(StateConstant.hurt, false);
        animator.SetBool(StateConstant.attack, false);
        animator.SetBool(StateConstant.died, false);

    }
}