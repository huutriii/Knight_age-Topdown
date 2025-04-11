using UnityEngine;

public class BossIdleState : BossBaseState
{
    public BossIdleState(Animator animator) : base(animator)
    {
    }

    public override void Enter()
    {
        animator.SetBool(STATE.idle, true);
    }

    public override void Exit()
    {
        animator.SetBool(STATE.idle, false);
    }
}
