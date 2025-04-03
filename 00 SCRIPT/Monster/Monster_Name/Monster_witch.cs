using System.Collections.Generic;
using UnityEngine;

public class Monster_witch : MonsterStatBase
{
    [SerializeField] int countMonster;
    [SerializeField] List<GameObject> monstersByArea = new();
    [SerializeField] GameObject monster;
    public override void Attack()
    {
        throw new System.NotImplementedException();
    }

    public override void Escape()
    {
        throw new System.NotImplementedException();
    }

    public override void MoveToward()
    {
        throw new System.NotImplementedException();
    }

    public override void Patrol()
    {
        throw new System.NotImplementedException();
    }

    private void Update()
    {
        Patrol2();
        //Generate();
        foreach (GameObject monster in monstersByArea)
        {
            monster.SetActive(true);
            monster.transform.position = transform.position;
        }
    }

    void Generate()
    {
        if (monstersByArea.Count < countMonster)
        {
            GameObject monster = Instantiate(this.monster, transform.position, Quaternion.identity);
            monster.SetActive(true);
        }
        else
        {

        }
    }
}
