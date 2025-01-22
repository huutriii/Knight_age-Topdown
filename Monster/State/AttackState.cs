using UnityEngine;

public class AttackState : IMonsterState
{
    public Animator animator;
    public AttackState(Animator animator) => (this.animator) = (animator);
    public void Enter()
    {
        animator.SetBool("attack", true);
    }

    public void Execute()
    {
        throw new System.NotImplementedException();
    }

    public void Exit()
    {
        animator.SetBool("attack", false);
    }
}
