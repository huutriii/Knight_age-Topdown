using UnityEngine;

public class BossDeathState : BossBaseState
{
    public BossDeathState(Animator animator) : base(animator)
    {
    }

    public override void Enter()
    {
        this.animator.SetBool(STATE.died, true);
        Debug.Log("death !");
    }

    public override void Exit()
    {
        this.animator.SetBool(STATE.died, false);
    }
}
