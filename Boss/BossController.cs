using System.Collections;
using UnityEngine;

public class BossController : MonoBehaviour
{
    private Animator animator;
    private IBossState currentState;

    public Vector2 pivot = new Vector2(0, 0);
    public float radius = 4f;

    [SerializeField] float speedArround, speedAttack;
    [SerializeField] float radiusTarget;
    public bool isIdle;

    public bool isWalk;
    public bool isPatrolling;

    public bool isRun;
    public Vector2 currentPositionTarget;

    public GameObject currentPlayerTarget;
    float detectPlayerRange = 4f;

    public bool isHurt;
    public bool isAttack;
    public bool isDeath;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }


    private void Update()
    {
        if (currentState != null)
        {
            currentState.Update();

            var newState = currentState.HandleTransition();

            if (newState != null)
            {
                TransitionToState(newState);
            }
        }

        Target();
        CheckPlayerInRange();
        UpdateBosMovement();
    }

    private void TransitionToState(IBossState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState?.Enter();
    }


    private void UpdateBosMovement()
    {
        if (currentPlayerTarget != null)
            MoveToTarget(currentPlayerTarget.transform.position);
        else
        {
            if (Vector2.Distance(transform.position, currentPositionTarget) > 0.3f)
            {
                MoveToTarget(currentPositionTarget);
                isPatrolling = true;
            }
            else
            {
                if (isPatrolling)
                    StartCoroutine(WaitBossRest());
            }
        }
    }

    IEnumerator WaitBossRest()
    {
        isPatrolling = false;
        yield return new WaitForSeconds(8);
        float x = Random.Range(pivot.x - radius, pivot.x + radius);
        float y = Random.Range(pivot.y - radius, pivot.y + radius);
        currentPositionTarget = new Vector2(x, y);

        isPatrolling = true;
    }

    private void Target()
    {
        GameObject target = null;
        float minDistance = Mathf.Infinity;


        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radiusTarget);

        foreach (Collider2D hit in hits)
        {
            if (hit.transform == this.transform || !hit.CompareTag(TagManager.Player))
                continue;

            float distance = Vector2.Distance(transform.position, hit.gameObject.transform.position);

            if (distance < minDistance)
            {
                minDistance = distance;
                target = hit.gameObject;
            }
            currentPlayerTarget = target;
        }
    }


    public void CheckPlayerInRange()
    {
        if (currentPlayerTarget == null)
            return;

        float distance = Vector2.Distance(transform.position, currentPlayerTarget.transform.position);

        if (distance > detectPlayerRange)
        {
            currentPlayerTarget = null;
            isRun = false;
            isWalk = true;
        }
        else
        {
            isWalk = false;
            isRun = true;
        }
    }

    private void MoveToTarget(Vector2 target)
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speedAttack * Time.deltaTime);

        UpdateFacingDirection(target);
    }

    private void UpdateFacingDirection(Vector2 target)
    {
        Vector3 scale = transform.localScale;
        scale.x = (transform.position.x > target.x) ? -1 : 1;
        transform.localScale = scale;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(currentPositionTarget, 0.1f);
    }
}