using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] float speedMagnitude;
    bool isMove = false;
    List<GameObject> pools = new();
    [SerializeField] GameObject effect;
    void Start()
    {

    }

    void Update()
    {
        if (!isMove)
        {
            Vector3 posTarget = target.transform.position;

            Vector3 distance = (posTarget - transform.position).normalized;
            float angle = Mathf.Atan2(distance.y, distance.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0, 0, angle + 90);
            transform.position += distance * speedMagnitude * Time.deltaTime;

            if (Vector3.Distance(transform.position, target.transform.position) <= 0.2)
                isMove = true;
        }
    }

    private void OnEnable()
    {
        isMove = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isMove = true;
        StartCoroutine(WaitFordDisable());
        GameObject effect = GetEffect();
        effect.SetActive(true);
        effect.transform.position = target.transform.position;
    }

    GameObject GetEffect()
    {
        foreach (GameObject effectPool in pools)
        {
            if (!effectPool.activeSelf)
            {
                return effectPool;
            }
        }

        GameObject effect = Instantiate(this.effect, transform.position, Quaternion.identity);
        pools.Add(effect);
        effect.SetActive(false);

        return effect;
    }

    IEnumerator WaitFordDisable()
    {
        gameObject.SetActive(false);
        yield return new WaitForSeconds(1);

        Debug.Log("that");
    }
}
