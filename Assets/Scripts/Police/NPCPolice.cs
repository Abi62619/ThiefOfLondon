using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCPolice : MonoBehaviour
{
    [Header("NPC movement settings")]
    public Transform player;
    public float viewDistance = 10f;
    private NavMeshAgent agent;

    [Header("Police Alerted Settings")]
    [SerializeField] private bool isPoliceAlerted; 
    public PickPocketManager pickpocketManager; 

    [Header("Animation Settings")]
    [SerializeField] private Animator policeAnim; 
    [SerializeField] private float idleTimeBeforeStop = 0f;
    private float idleTimer; 

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        policeAnim = GetComponent<Animator>(); 
    }

    void Update()
    {
        if (isPoliceAlerted)
        {
            Movement();
        }
    }

    public void Alerted()
    {
        isPoliceAlerted = true; 
    }

    public void Movement()
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
                policeAnim.SetBool("isPoliceWalking", true);
                idleTimer = 0f; //reset when moving
            }
            else
            {
                // DO NOT SEE PLAYER: Stop
                agent.SetDestination(transform.position);

                idleTimer += Time.deltaTime;

                if (idleTimer >= idleTimeBeforeStop)
                {
                    policeAnim.SetBool("isPoliceWalking", false);
                }
            }
        }
    }
}