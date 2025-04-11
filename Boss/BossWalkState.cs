using UnityEngine;

public class BossWalkState : BossBaseState
{
    public BossWalkState(Animator animator) : base(animator)
    {
    }

    public override void Enter()
    {
        animator.SetBool(STATE.walk, true);
    }

    public override void Exit()
    {
        animator.SetBool(STATE.walk, false);
    }
}
