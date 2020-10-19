using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RabbitController : MonoBehaviour
{
    public GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        if (target)
        {
            GetComponent<NavMeshAgent>().destination = target.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
