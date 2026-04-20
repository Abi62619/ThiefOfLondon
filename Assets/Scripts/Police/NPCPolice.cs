using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCPolice : MonoBehaviour
{
    public Transform player;
    public float viewDistance = 10f;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        RaycastHit hit;
        Vector3 directionToPlayer = (player.position - transform.position).normalized;

        // Visualize ray in scene view
        Debug.DrawRay(transform.position, directionToPlayer * viewDistance, Color.red);

        // Raycast check: sees player if nothing obstructs the view
        if (Physics.Raycast(transform.position, directionToPlayer, out hit, viewDistance))
        {
            if (hit.transform == player)
            {
                // SEE PLAYER: Move towards
                agent.SetDestination(player.position);
            }
            else
            {
                // DO NOT SEE PLAYER: Stop
                agent.SetDestination(transform.position);
            }
        }
    }

    void PoliceAlerted()
    {
        
    }
}