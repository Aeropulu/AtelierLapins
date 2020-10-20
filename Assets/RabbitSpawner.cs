using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitSpawner : MonoBehaviour
{
    public float radius = 2.0f;
    public int number = 30;
    public GameObject prefab;

    public GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        for (int i=0; i < number; i++)
        {
            Vector2 v2 = Random.insideUnitCircle * radius;
            Vector3 pos = new Vector3(transform.position.x + v2.x, transform.position.y, transform.position.z + v2.y);
            GameObject go = Instantiate(prefab, pos, Quaternion.Euler(0, Random.Range(-180, 180), 0));
            go.GetComponent<RabbitController>().manager = GetComponent<BoidManager>();
            if (target && (Random.value < 0.3f))
            {
                go.GetComponent<RabbitController>().target = target.transform;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
