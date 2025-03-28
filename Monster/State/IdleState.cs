using UnityEngine;

public class IdleState : MonsterStateBase, IMonsterState
{
    public IdleState(MonsterController monster, Animator animator) : base(monster, animator) { }
    public void Enter()
    {
        ResetAllAnimations();
        animator.SetBool(StateConstant.idle, true);
    }

    public void Exit()
    {
        animator.SetBool(StateConstant.idle, false);
    }

    public IMonsterState HandleTransition()
    {
        if (monster.IsDead)
            return new DiedState(monster, animator);

        if (monster.IsHurt)
            return new HurtState(monster, animator);

        if (monster.IsAttacking)
            return new AttackState(monster, animator);

        if (monster.ShouldRun)
            return new RunState(monster, animator);

        return null;
    }

    public void Update()
    {
    }
}