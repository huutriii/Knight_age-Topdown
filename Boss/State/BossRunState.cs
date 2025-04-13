using UnityEngine;

public class BossRunState : BossBaseState
{
    public BossRunState(Animator animator) : base(animator)
    {
    }

    public override void Enter()
    {
        animator.SetBool(STATE.run, true);
    }

    public override void Exit()
    {
        animator.SetBool(STATE.run, false);
    }
}
