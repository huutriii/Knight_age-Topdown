using UnityEngine;

public class Warrior_Attack : WarriorStateBase
{
    public Warrior_Attack(Animator animator) : base(animator)
    {
    }

    public override void Enter()
    {
        Debug.Log("Attack entered !");
        this._animator.SetBool("Attack", true);
    }

    public override void Exit()
    {
        this._animator.SetBool("Attack", false);
    }

    public override void HandleTransition()
    {
    }
}
