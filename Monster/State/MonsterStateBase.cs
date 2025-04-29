using UnityEngine;
public abstract class MonsterStateBase : IMonsterState
{
    protected Animator animator;

    public MonsterStateBase(Animator animator) => (this.animator) = (animator);

    public abstract void Enter();

    public abstract void Exit();

}