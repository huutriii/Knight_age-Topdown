using UnityEngine;

public class LOcaltest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(1, 1, 0) * Time.deltaTime * 1;
    }
}
