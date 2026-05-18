using UnityEngine; 
using System.Collections; 
using System.Collections.Generic; 
using UnityEngine.AI; 

public class AgentAI : MonoBehaviour
{
    [Header("Waypoints Settings")]
    public List<Transform> waypoints; 
    NavMeshAgent navMeshAgent; 
    public int currentWaypointIndex; 

    [Header("Animation Settings")]
    [SerializeField] private Animator prayAnim;  

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>(); 
    }
    
    void Update()
    {
        Walking(); 
    }

    void Walking()
    {
        if(waypoints.Count == 0)
        {
            return; 
        }

        float distanceToWayPoint = Vector3.Distance(waypoints[currentWaypointIndex].position, transform.position); 

        if(distanceToWayPoint <= 3)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Count; 
        }

        navMeshAgent.SetDestination(waypoints[currentWaypointIndex].position); 

        prayAnim.SetBool("isPrayWalking", true); 
    }
}