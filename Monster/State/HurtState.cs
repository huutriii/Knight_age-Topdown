using UnityEngine;

public class HurtState : IMonsterState
{
    public Animator animator;
    public HurtState(Animator animator) => (this.animator) = (animator);
    public void Enter()
    {
        animator.SetBool("hurt", true);
    }

    public void Execute()
    {
        throw new System.NotImplementedException();
    }

    public void Exit()
    {
        animator.SetBool("hurt", false);
    }
}
