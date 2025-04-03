using UnityEngine;

public class BossWalkState : BossBaseState, IBossState
{
    public BossWalkState(BossController boss, Animator animator) : base(boss, animator) { }

    public override void Enter()
    {
        animator.SetBool(Constant.walk, true);
        boss.isWalk = true;
    }

    public override void Exit()
    {
        animator.SetBool(Constant.walk, false);
        boss.isWalk = false;
    }

    public override IBossState HandleTransition()
    {
        if (boss.isDeath)
            return new BossDeathState(boss, animator);
        if (boss.isAttack)
            return new BossAttackState(boss, animator);
        if (boss.isHurt)
            return new BossHurtState(boss, animator);
        if (boss.currentPlayerTarget != null && boss.isWalk)
            return new BossWalkState(boss, animator);
        if (boss.currentPlayerTarget == null && boss.isRun)
            return new BossRunState(boss, animator);

        return null;
    }
}
