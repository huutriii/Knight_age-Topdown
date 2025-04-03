using UnityEngine;

public abstract class BossBaseState : IBossState
{
    protected readonly Animator animator;
    protected readonly BossController boss;

    protected BossBaseState(BossController boss, Animator animator) =>
        (this.boss, this.animator) = (boss, animator);


    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void Update() { }
    public abstract IBossState HandleTransition();

}
