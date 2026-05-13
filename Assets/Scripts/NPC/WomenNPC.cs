using UnityEngine;
using System.Collections.Generic; 
using System.Collections; 

public class WomenNPC : MonoBehaviour
{
    [Header("Waypoints Settings")]
    public List<Transform> waypoints; 
    UnityEngine.AI.NavMeshAgent navMeshAgent; 
    public int currentWaypointIndex; 

    [Header("Animation Settings")]
    [SerializeField] private Animator womenAnim; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>(); 
    }

    // Update is called once per frame
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

        womenAnim.SetBool("isWomenWalking", true); 
    }
}
