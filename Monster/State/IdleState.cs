using UnityEngine;

public class IdleState : MonsterStateBase, IMonsterState
{
    public IdleState(Animator animator) : base(animator) { }
    public override void Enter()
    {
        animator.SetBool(STATE.idle, true);
    }

    public override void Exit()
    {
        animator.SetBool(STATE.idle, false);
    }
}