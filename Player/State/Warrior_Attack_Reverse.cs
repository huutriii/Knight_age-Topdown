using UnityEngine;

public class Warrior_Attack_Reverse : WarriorStateBase
{
    public Warrior_Attack_Reverse(Animator animator) : base(animator)
    {
    }

    public override void Enter()
    {
        this._animator.SetBool(GAME.attack_reverse, true);
    }

    public override void Exit()
    {
        this._animator.SetBool(GAME.attack_reverse, false);
    }

    public override void HandleTransition()
    {
    }
}
