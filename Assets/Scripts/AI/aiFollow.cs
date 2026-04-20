using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class aiFollow : MonoBehaviour
{

    public Transform target;
    public float stoppingDistance = 1f; 
    
    private NavMeshAgent agent; 
        
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (agent != null)
        {
            agent.SetDestination(target.position);

            if(Vector3.Distance(transform.position, target.position) <= stoppingDistance)
            {
                agent.isStopped = true;
            }else
            {
                agent.isStopped = false;
            }
        }
    }
}
