using UnityEngine;

public class DiedState : IMonsterState
{
    public Animator animator;
    public DiedState(Animator animator) => (this.animator) = (animator);
    public void Enter()
    {
        animator.SetBool("died", true);
    }

    public void Execute()
    {
        throw new System.NotImplementedException();
    }

    public void Exit()
    {
        animator.SetBool("died", false);
    }
}