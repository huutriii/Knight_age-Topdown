using UnityEngine;

public class Warrior_Idle : WarriorStateBase
{
    public Warrior_Idle(Animator animator) : base(animator)
    {
    }

    public override void Enter()
    {
        this._animator.SetBool(STATE.idle, true);
    }

    public override void Exit()
    {
        this._animator.SetBool(STATE.idle, false);
    }

    public override void HandleTransition()
    {
    }
}
