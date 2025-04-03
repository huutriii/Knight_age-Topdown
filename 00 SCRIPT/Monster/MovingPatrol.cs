using System.Collections;
using UnityEngine;

public class MovingPatrol : MonoBehaviour
{
    // ... existing code ...

    [SerializeField] Vector2 pivot;
    [SerializeField] float radius;
    [SerializeField] GameObject player;
    [SerializeField] float restTime;

    [SerializeField] float moveSpeed = 2f;
    private bool isResting = false;
    [SerializeField] private Vector2 currentPatrolTarget;

    private void Start()
    {
        // Khởi tạo vị trí tuần tra đầu tiên
        SetNewPatrolTarget();
    }

    private void Update()
    {
        if (player != null || isResting) return;

        // Di chuyển đến điểm tuần tra
        Vector2 currentPos = transform.position;
        transform.position = Vector2.MoveTowards(currentPos, currentPatrolTarget, moveSpeed * Time.deltaTime);

        // Kiểm tra xem đã đến điểm tuần tra chưa
        if (Vector2.Distance(currentPos, currentPatrolTarget) < 0.1f)
        {
            StartCoroutine(RestAndSetNewTarget());
        }
    }

    private void SetNewPatrolTarget()
    {
        // Tạo một điểm ngẫu nhiên trong bán kính cho phép
        float randomAngle = Random.Range(0f, 360f);
        float randomRadius = Random.Range(0f, radius);
        float x = pivot.x + randomRadius * Mathf.Cos(randomAngle * Mathf.Deg2Rad);
        float y = pivot.y + randomRadius * Mathf.Sin(randomAngle * Mathf.Deg2Rad);
        currentPatrolTarget = new Vector2(x, y);
    }

    private IEnumerator RestAndSetNewTarget()
    {
        isResting = true;
        yield return new WaitForSeconds(restTime);
        SetNewPatrolTarget();
        isResting = false;
    }

    private void OnDrawGizmos()
    {
        // Vẽ một chấm đỏ tại vị trí tuần tra hiện tại
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(currentPatrolTarget, 0.2f);
    }

}