using UnityEngine;
public class Warrior_Run : WarriorStateBase
{
    public Warrior_Run(Animator animator) : base(animator) { }

    public override void Enter()
    {
        this._animator.SetBool(STATE.run, true);
    }

    public override void Exit()
    {
        this._animator.SetBool(STATE.run, false);
    }

    public override void HandleTransition()
    {
    }
}
