using UnityEngine;

public abstract class MonsterStateBase : MonoBehaviour
{
    protected Animator _animator;
    protected string _stateName;

    public MonsterStateBase(Animator animator, string stateName) => (_animator, _stateName) = (animator, stateName);
}
