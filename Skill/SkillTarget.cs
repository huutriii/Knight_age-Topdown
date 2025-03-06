using System.Collections.Generic;
using UnityEngine;

public class SkillTarget : MonoBehaviour
{
    [SerializeField] Transform originalTarget;
    [SerializeField] GameObject _flameHit;
    Animator animator;
    float speed = 10f;
    List<GameObject> pools = new();
    Vector2 lastKnownPosition; // ✅ Lưu vị trí cuối cùng của target

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool(SkillContent.flame1, true);
    }

    private void OnEnable()
    {
        originalTarget = TargetClosit.Instance.GetTarget();

        // Nếu không có target ngay từ đầu, hủy skill
        if (originalTarget != null)
        {
            lastKnownPosition = originalTarget.position;
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    void Update()
    {
        // Luôn cập nhật target nếu còn tồn tại
        Transform newTarget = TargetClosit.Instance.GetTarget();
        if (newTarget != null)
        {
            originalTarget = newTarget;
            lastKnownPosition = originalTarget.position;
        }

        // Di chuyển đến vị trí cuối cùng
        Moving(lastKnownPosition);

        // Khi đến nơi, tự hủy
        if (Vector2.Distance(transform.position, lastKnownPosition) < 0.1f)
        {
            GameObject effect_hit = GetFlameHit();
            effect_hit.transform.position = transform.position;
            effect_hit.SetActive(true);

            this.gameObject.SetActive(false);
        }
    }

    void Moving(Vector2 targetPosition)
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        Vector3 pos = (targetPosition - (Vector2)transform.position).normalized;
        float angle = Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag(TagManager.Player))
        {
            GameObject effect_hit = GetFlameHit();
            effect_hit.transform.position = collision.transform.position;
            effect_hit.SetActive(true);

            this.gameObject.SetActive(false);
        }
    }

    GameObject GetFlameHit()
    {
        foreach (GameObject p in pools)
        {
            if (!p.activeSelf)
                return p;
        }

        GameObject flameHit = Instantiate(_flameHit, transform.position, Quaternion.identity);
        pools.Add(flameHit);
        flameHit.SetActive(false);

        return flameHit;
    }
}
