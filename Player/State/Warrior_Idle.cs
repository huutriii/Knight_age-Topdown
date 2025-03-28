using UnityEngine;

public class Warrior_Idle : WarriorStateBase
{
    public Warrior_Idle(Animator animator) : base(animator)
    {
    }

    public override void Enter()
    {
        this._animator.SetBool("Idle", true);
    }

    public override void Exit()
    {
        this._animator.SetBool("Idle", false);
    }

    public override void HandleTransition()
    {
    }
}
