using UnityEngine;

public class IdleState : IMonsterState
{
    public Animator animator;

    public IdleState(Animator animator) => (this.animator) = (animator);
    public void Enter()
    {
        animator.SetBool("idle", true);
    }

    public void Execute()
    {
        throw new System.NotImplementedException();
    }

    public void Exit()
    {
        animator.SetBool("idle", false);
    }
}
