<<<<<<< HEAD
using UnityEngine;

public class LightningStrike : MonoBehaviour
{
    private static LightningStrike instance;
    public static LightningStrike Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<LightningStrike>();
                if (instance == null)
                {
                    Debug.LogError("No LightningStrike found in scene!");
                    return null;
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    [SerializeField] GameObject currentTarget;
    [SerializeField] float magnitude;
    float radius = 7f;
    [SerializeField] float angle;

    private void Update()
    {
        Target();
        CheckTargetInRange();

        Change();
    }

    void Change()
    {
        if (currentTarget != null)
        {
            Vector3 direction = currentTarget.transform.position - transform.position;
            direction = direction.normalized;

            angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0, 0, angle + 90);
            float scaleX = 3 * magnitude;
            Vector3 scale = new Vector3(1.5f * magnitude, magnitude, 1);
            transform.localScale = scale;
        }
        if (currentTarget != null)
            Debug.Log(Vector2.Distance(transform.position, currentTarget.transform.position));
    }

    void Target()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radius);
        foreach (Collider2D hit in hits)
        {
            if (hit.transform == this.transform || !hit.gameObject.CompareTag(TagManager.Player))
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
                currentTarget = null;
            else
                magnitude = Vector2.Distance(transform.position, currentTarget.transform.position);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
=======
﻿using System.Collections;
using UnityEngine;

public class LightningStrike : MonoBehaviour
{
    Animator animator;
    GameObject currentTarget;
    [SerializeField] float radius, magnitude;
    [SerializeField] float timeDuration;
    private void Start()
    {
        animator = GetComponent<Animator>();
        radius = TowerControllerTest.Instance.Radius;
        magnitude = TowerControllerTest.Instance.Magnitude;
    }


    private void OnEnable()
    {
        StartCoroutine(WaitLightning());
    }

    private void Update()
    {
        Target();
        CheckTargetInRange();
        TargetAttack();
    }

    public void TargetAttack()
    {
        if (currentTarget != null)
        {
            Vector3 direction = currentTarget.transform.position - transform.position;
            direction = direction.normalized;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle + 90);

            float scaleX = 3 * magnitude;
            Vector3 scale = new Vector3(1.5f * magnitude, magnitude, 1);
            transform.localScale = scale;
        }
    }
    void Target()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radius);
        foreach (Collider2D hit in hits)
        {
            if (hit.transform == this.transform || !hit.gameObject.CompareTag(TagManager.Player))
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
    IEnumerator WaitLightning()
    {
        yield return new WaitForSeconds(timeDuration);
        gameObject.SetActive(false);
    }
}
>>>>>>> 71c3880 (tower)
