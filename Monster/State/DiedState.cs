using UnityEngine;

public class DiedState : MonsterStateBase, IMonsterState
{
    public DiedState(Animator animator) : base(animator) { }

    public override void Enter()
    {
        animator.SetBool(STATE.died, true);
    }

    public override void Exit()
    {
        animator.SetBool(STATE.died, false);
    }
}