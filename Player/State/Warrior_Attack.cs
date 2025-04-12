using UnityEngine;

public class Warrior_Attack : WarriorStateBase
{
    public Warrior_Attack(Animator animator) : base(animator)
    {
    }

    public override void Enter()
    {
        this._animator.SetBool(STATE.attack, true);
    }

    public override void Exit()
    {
        this._animator.SetBool(STATE.attack, false);
    }

    public override void HandleTransition()
    {
    }
}
