using UnityEngine;

public class BossIdleState : BossBaseState, IBossState
{
    public BossIdleState(BossController boss, Animator animator) : base(boss, animator) { }

    public override void Enter()
    {
        animator.SetBool(TagManager.ilde, true);
        boss.isIdle = true;
    }

    public override void Exit()
    {
        animator.SetBool(TagManager.ilde, false);
        boss.isIdle = false;
    }

    public override IBossState HandleTransition()
    {
        if (boss.isDeath)
            return new BossDeathState(boss, animator);
        if (boss.isAttack)
            return new BossAttackState(boss, animator);
        if (boss.isHurt)
            return new BossHurtState(boss, animator);
        if (boss.isRun)
            return new BossRunState(boss, animator);
        if (boss.isWalk)
            return new BossWalkState(boss, animator);
        if (boss.isIdle)
            return null;


        return null;
    }

}
