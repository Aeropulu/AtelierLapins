using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RabbitController : MonoBehaviour
{
    public float maxSpeed = 2.0f;

    public float attractionRange = 1.5f;
    public float attractionStrength = 1.0f;
    public float alignmentRange = 1.0f;
    public float alignementStrength = 1.0f;
    public float repulsionRange = 0.5f;
    public float repulsionStrength = 1.0f;
    
    public BoidManager manager;

    public Transform target;
    public float followStrength = 1.0f;
    private Vector3 velocity;
    private NavMeshAgent agent;
    private NavMeshPath path;

    private NavMeshHit leftHit;
    private NavMeshHit rightHit;
    private float raycastAngle = 10.0f;



    // Start is called before the first frame update
    void Start()
    {
        
        velocity = transform.forward;
        manager.boids.Add(transform);
        agent = GetComponent<NavMeshAgent>();
        path = new NavMeshPath();
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 sumForces = Vector3.zero;
        int numForces = 0;

        foreach (Transform t in manager.boids)
        {
            float distance = Vector3.Distance(transform.position, t.position);
            if (distance <= repulsionRange && t!= transform)
            {
                float factor = -(repulsionRange - distance) / repulsionRange;
                sumForces += (t.position - transform.position).normalized * factor * repulsionStrength;
                numForces++;
            }
            else if (distance <= alignmentRange)
            {
                sumForces += t.forward * alignementStrength;
                numForces++;
            }
            else if (distance <= attractionRange)
            {
                float factor = (distance - alignmentRange) / (attractionRange - alignmentRange);
                sumForces += (t.position - transform.position).normalized * factor * attractionStrength;
                numForces++;
            }
        }
        
        
        if (agent.Raycast(transform.position + Vector3.RotateTowards(velocity, transform.right, raycastAngle * Mathf.Deg2Rad, 0.0f), out rightHit))
        {

        }
        if (agent.Raycast(transform.position + Vector3.RotateTowards(velocity, -transform.right, raycastAngle * Mathf.Deg2Rad, 0.0f), out leftHit))
        {

        }
        
        
        if (numForces > 0)
        {
            sumForces /= (float)numForces;
            velocity += sumForces * Time.deltaTime;
        }
        

        transform.forward = velocity.normalized;
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
        
        agent.Move(velocity * Time.deltaTime);
    }

    
    private void OnDrawGizmos()
    {
        return;
        if (target)
        {
            for (int i = 0; i < path.corners.Length; i++)
            {
                if (i == 0)
                    Gizmos.DrawLine(transform.position, path.corners[i]);
                else
                    Gizmos.DrawLine(agent.path.corners[i - 1], path.corners[i]);
            }
        }
    }
}
