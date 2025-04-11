using UnityEngine;

public class TowerControllerTest2 : MonoBehaviour
{
    public static TowerControllerTest2 Instance
    {
        get; private set;
    }

    [SerializeField] GameObject currentTarget;
    [SerializeField] float radius;
    public float Radius => radius;
    [SerializeField] LightningStrike lightning;
    [SerializeField] float magnitude;
    public float Magnitude => magnitude;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Update()
    {
        if (currentTarget != null)
        {
            lightning.gameObject.SetActive(true);
        }

        Target();
        CheckTargetInRange();
    }

    void Target()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radius);
        foreach (Collider2D hit in hits)
        {
            if (hit.transform == this.transform || !hit.gameObject.CompareTag(GAME.Player))
            {
                continue;
            }
            else
                currentTarget = hit.gameObject;
        }
    }

    void CheckTargetInRange()
    {
        if (currentTarget != null)
        {
            if (Vector2.Distance(transform.position, currentTarget.transform.position) > radius)
            {
                currentTarget = null;
            }
            else
            {
                magnitude = Vector2.Distance(transform.position, currentTarget.transform.position);
            }
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
