using UnityEngine;

public class Test10 : MonoBehaviour
{
    public GameObject g;
    // Start is called before the first frame update
    void Start()
    {
        //g = GetComponent<GameObject>();
        g.transform.position += new Vector3(0, 5f, 0) * Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
