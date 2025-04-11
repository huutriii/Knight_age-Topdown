using UnityEngine;

public abstract class BossBaseState : IBossState
{
    protected readonly Animator animator;

    protected BossBaseState(Animator animator) =>
        (this.animator) = (animator);


    public abstract void Enter();
    public abstract void Exit();

}
