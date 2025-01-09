using UnityEngine;

public abstract class MonsterStatBase : MonoBehaviour
{
    [SerializeField] protected Vector2 m_position;
    [SerializeField] protected float m_offsetX;
    [SerializeField] protected float m_offsetY;
    [SerializeField] protected float minAreaX;
    [SerializeField] protected float minAreaY;
    [SerializeField] protected float maxAreaX;
    [SerializeField] protected float maxAreaY;

    #region base stat

    protected float hp;
    protected float dame;
    protected float defense;
    protected float speed;
    protected int level;

    #endregion

    #region advance stat

    protected float atkRange;
    protected float criticalChance;
    protected float criticalDame;
    protected float dodgeChance;
    #endregion


    #region bonous stat

    protected float expDrop;
    protected float itemDrop;
    protected float spawnRate;

    #endregion

    private void OnEnable()
    {
        m_position = transform.position + new Vector3(m_offsetX, m_offsetY, 0);
    }

    public abstract void Attack();
    public abstract void MoveToward();
    public abstract void Escape();
    public abstract void Patrol();
    public void Patrol2()
    {
        if (Vector2.Distance(transform.position, (Vector3)m_position) <= 0.1f)
        {
            float x = Random.Range(minAreaX, maxAreaX);
            float y = Random.Range(minAreaY, maxAreaY);
            m_position = new Vector2(x, y);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, m_position, 0.4f * Time.deltaTime);
        }


        Vector3 scale = transform.localScale;
        if (transform.position.x > m_position.x)
        {
            scale.x = -1;
        }
        else
        {
            scale.x = 1;
        }
        transform.localScale = scale;

    }
}
