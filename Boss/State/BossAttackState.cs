using UnityEngine;

public class BossAttackState : BossBaseState
{
    public BossAttackState(Animator animator) : base(animator)
    {
    }

    public override void Enter()
    {
        this.animator.SetBool(STATE.attack, true);
    }

    public override void Exit()
    {
        this.animator.SetBool(STATE.attack, false);
    }
}
