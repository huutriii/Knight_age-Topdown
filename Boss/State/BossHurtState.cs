using UnityEngine;

public class BossHurtState : BossBaseState
{
    public BossHurtState(Animator animator) : base(animator)
    {
    }

    public override void Enter()
    {
        this.animator.SetBool(STATE.hurt, true);
    }

    public override void Exit()
    {
        this.animator.SetBool(STATE.hurt, false);
    }
}
