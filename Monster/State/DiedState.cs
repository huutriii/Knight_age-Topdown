using UnityEngine;

public class DiedState : MonsterStateBase, IMonsterState
{
    private float deathTimer;
    private const float DEATH_DURATION = 2f;

    public DiedState(MonsterController monster, Animator animator) : base(monster, animator)
    {
        deathTimer = 0f;
    }

    public void Enter()
    {
        ResetAllAnimations();
        animator.SetBool(StateContaint.died, true);
        monster.SetMovementLocked(true);
    }

    public void Exit()
    {
        // Không cần exit vì đây là state cuối cùng
    }

    public void Update()
    {
        deathTimer += Time.deltaTime;

        if (deathTimer >= DEATH_DURATION)
        {
            // Disable gameObject và respawn
            monster.gameObject.SetActive(false);
            SouthestController.Instance.ReSpawnMonster(monster.gameObject, monster.Pivot);
        }
    }

    public IMonsterState HandleTransition()
    {
        // Không có transition nào từ death state
        return null;
    }
}