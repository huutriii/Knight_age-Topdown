using UnityEngine;

public class WalkState : IMonsterState
{
    public Animator animator;
    public WalkState(Animator animator) => (this.animator) = (animator);
    public void Enter()
    {
        animator.SetBool("walk", true);
    }

    public void Execute()
    {
        throw new System.NotImplementedException();
    }

    public void Exit()
    {
        animator.SetBool("walk", false);
    }
}
