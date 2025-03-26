using UnityEngine;
public class Warrior_Run : WarriorStateBase
{
    public Warrior_Run(Animator animator) : base(animator)
    {
    }

    public override void Enter()
    {
        this._animator.SetBool("Run", true);
    }

    public override void Exit()
    {
        this._animator.SetBool("Run", false);
    }

    public override void HandleTransition()
    {
        throw new System.NotImplementedException();
    }
}
