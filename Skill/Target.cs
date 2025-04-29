using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private static Target _instance;
    public static Target Instance => _instance;

    public List<Transform> targetsInRange = new();
    public GameObject currentTarget;
    public int targetIndex = 0;
    public bool isTargetSkill => currentTarget != null;
    private void Awake()
    {
        if (Instance == null)
        {
            _instance = this;
            return;
        }
        if (Instance.GetInstanceID() != GetInstanceID())
        {
            Destroy(gameObject);
        }
    }
    void Update()
    {
        UpdateTargets();
        SelectTarget();
    }

    void UpdateTargets()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 5f, ~LayerMask.GetMask(GAME.Player));
        List<Transform> tempTargets = new();

        foreach (var hit in hits)
        {
            if (hit.gameObject == gameObject) continue;
            tempTargets.Add(hit.transform);
        }

        tempTargets.Sort((a, b) =>
        {
            float disA = Vector2.Distance(transform.position, a.position);
            float disB = Vector2.Distance(transform.position, b.position);

            return disA.CompareTo(disB);
        });

        targetsInRange = tempTargets;

        if (targetsInRange.Count == 0)
        {
            currentTarget = null;
            targetIndex = 0;
        }

        if (currentTarget != null)
        {
            Debug.DrawLine(transform.position, currentTarget.transform.position, Color.yellow);
        }
    }

    void SelectTarget()
    {
        if (targetsInRange.Count == 0) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentTarget = targetsInRange[targetIndex].gameObject;
            targetIndex = (targetIndex + 1) % targetsInRange.Count;
        }
    }

    public Vector3 GetCurrentTarget() => currentTarget.transform.position;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 5f);
    }
}
