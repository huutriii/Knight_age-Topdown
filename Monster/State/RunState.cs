using UnityEngine;

public class RunState : IMonsterState
{
    public Animator animator;
    public RunState(Animator animator) => (this.animator) = (animator);
    public void Enter()
    {
        animator.SetBool("run", true);
    }

    public void Execute()
    {
        throw new System.NotImplementedException();
    }

    public void Exit()
    {
        animator.SetBool("run", false);
    }
}