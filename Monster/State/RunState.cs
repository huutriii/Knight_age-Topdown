using UnityEngine;

public class RunState : MonsterStateBase, IMonsterState
{
    public RunState(Animator animator) : base(animator) { }

    public override void Enter()
    {
        animator.SetBool(STATE.run, true);
    }

    public override void Exit()
    {
        animator.SetBool(STATE.run, false);
    }
}