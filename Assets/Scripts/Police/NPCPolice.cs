using UnityEngine;
using UnityEngine.AI; 

public class NPCPolice : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;

    public float arrestDistance = 2f;
    private bool isChasing = false;

    private PickPocketManager pickpocketManager; 

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        pickpocketManager = GetComponent<PickPocketManager>(); 
        Debug.Log("NPCPolice started, isChasing = " + isChasing);
    }

    void Update()
    {
        if (isChasing)
        {
            agent.SetDestination(player.position);

            float distance = Vector3.Distance(transform.position, player.position);

            Debug.Log("Chasing player...");

            if (distance <= arrestDistance)
            {
                ArrestPlayer();
            }
        }
    }

    public void Alert(Transform playerTransform)
    {
        Debug.Log("NPC ALERTED");
        player = playerTransform;
        isChasing = true;
    }

    private void ArrestPlayer()
    {
        Debug.Log("Player Arrested!");

        // Stop movement
        agent.isStopped = true;
        isChasing = false;

        // test punishment
        Time.timeScale = 0f; // pause game
        // or reduce money, send to jail
    }
}
