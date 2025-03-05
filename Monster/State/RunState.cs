using UnityEngine;

public class RunState : MonsterStateBase, IMonsterState
{
    public RunState(MonsterController monster, Animator animator) : base(monster, animator) { }

    public void Enter()
    {
        ResetAllAnimations();
        animator.SetBool(StateContaint.run, true);
    }

    public void Exit()
    {
        animator.SetBool(StateContaint.run, false);
    }

    public void Update()
    {
        // Không cần gọi MoveToTarget ở đây nữa vì MonsterController đã xử lý trong UpdateMovement
        // Chỉ cần đảm bảo animation run được set đúng
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