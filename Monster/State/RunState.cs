using UnityEngine;

public class RunState : MonsterStateBase, IMonsterState
{
    public RunState(MonsterController monster, Animator animator) : base(monster, animator) { }

    public void Enter()
    {
        ResetAllAnimations();
        animator.SetBool(STATE.run, true);
    }

    public void Exit()
    {
        animator.SetBool(STATE.run, false);
    }

    public IMonsterState HandleTransition()
    {
        if (monster.IsDead)
            return new DiedState(monster, animator);

        if (monster.IsHurt)
            return new HurtState(monster, animator);

        if (monster.IsAttacking)
            return new AttackState(monster, animator);

        if (!monster.ShouldRun)
            return new IdleState(monster, animator);

        return null;
    }
}