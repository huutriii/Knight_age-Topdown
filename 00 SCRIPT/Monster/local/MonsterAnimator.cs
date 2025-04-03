using UnityEngine;

/// <summary>
/// Controls the animation states of a monster character.
public class MonsterAnimator : MonoBehaviour
{
    private Animator animator;
    private IMonsterState currentState;
    private MonsterController monsterController;

    /// <summary>
    /// Initializes the animator component and sets the initial state.
    /// </summary>
    public void Initialize(MonsterController controller)
    {
        monsterController = controller;
        if (monsterController == null)
        {
            Debug.LogError($"[{gameObject.name}] MonsterController reference is required!");
            return;
        }

        animator = GetComponentInChildren<Animator>();
        if (animator == null)
        {
            Debug.LogError($"[{gameObject.name}] Failed to find Animator component in children!");
            return;
        }

        TransitionToState(new IdleState(monsterController, animator));
    }

    /// <summary>
    /// Updates the current state and handles state transitions.
    /// </summary>
    public void UpdateState()
    {
        if (currentState == null) return;

        currentState.Update();

        var newState = currentState.HandleTransition();
        if (newState != null && newState.GetType() != currentState.GetType())
        {
            TransitionToState(newState);
        }
    }

    /// <summary>
    /// Transitions to a new state if it's valid.
    /// </summary>
    /// <param name="newState">The new state to transition to.</param>
    protected virtual void TransitionToState(IMonsterState newState)
    {
        if (newState == null)
        {
            Debug.LogWarning($"[{gameObject.name}] Attempted to transition to a null state!");
            return;
        }

        if (currentState != null)
        {
            currentState.Exit();
        }

        currentState = newState;
        currentState.Enter();
    }

    /// <summary>
    /// Gets the current animator component.
    /// </summary>
    /// <returns>The animator component or null if not initialized.</returns>
    public Animator GetAnimator() => animator;

    /// <summary>
    /// Gets the current state of the monster.
    /// </summary>
    /// <returns>The current state or null if not initialized.</returns>
    public IMonsterState GetCurrentState() => currentState;
}