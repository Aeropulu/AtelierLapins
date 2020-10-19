using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RabbitController : MonoBehaviour
{
    public GameObject target;
    private Vector3 velocity;
    // Start is called before the first frame update
    void Start()
    {
        velocity = transform.forward;
        if (target)
        {
            //GetComponent<NavMeshAgent>().destination = target.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<NavMeshAgent>().Move(velocity * Time.deltaTime);
    }
}
