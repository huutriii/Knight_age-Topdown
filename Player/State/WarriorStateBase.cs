using UnityEngine;
[System.Serializable]
public abstract class WarriorStateBase : IWarriorState
{
    protected Animator _animator;

    public WarriorStateBase(Animator animator) => (_animator) = (animator);

    public abstract void Enter();
    public abstract void Exit();
    public abstract void HandleTransition();
}
