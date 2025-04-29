using UnityEngine;

public class AttackState : MonsterStateBase
{
    public AttackState(Animator animator) : base(animator) { }

    public override void Enter()
    {
        animator.SetBool(STATE.attack, true);
    }

    public override void Exit()
    {
        animator.SetBool(STATE.attack, false);
    }
}