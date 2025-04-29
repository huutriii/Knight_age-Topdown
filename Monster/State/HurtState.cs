using UnityEngine;

public class HurtState : MonsterStateBase, IMonsterState
{
    public HurtState(Animator animator) : base(animator) { }

    public override void Enter()
    {
        animator.SetBool(STATE.hurt, true);
    }

    public override void Exit()
    {
        animator.SetBool(STATE.hurt, false);
    }
}