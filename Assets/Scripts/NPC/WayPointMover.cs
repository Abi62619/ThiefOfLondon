using UnityEngine; 
using System.Collections; 

public class WaypointMover : MonoBehaviour
{
    [Header("Waypoints Settings")]
    [SerializeField] private float moveSpeed; 
    [SerializeField] private float waitTime = 2f; 
    [SerializeField] private bool loopWaypoints = true;     
    [SerializeField] private Transform waypointParent; 

    private Transform[] _waypoints; 
    private int currentWaypointIndex; 
    private bool isWaiting; 

    void Start()
    {
        _waypoints = new Transform[waypointParent.childCount]; 

        for(int i = 0; i < waypointParent.childCount; i++)
        {
            waypoints[i] = waypointParent.GetChild(i); 
        }
    }

    void Update()
    {
        if(PauseMenu.IsGamePaused || isWaiting)
        {
            return; 
        }

        MoveToWaypoint();
    }

    void MoveToWaypoint()
    {
        Transform target = _waypoints[currentWaypointIndex]; 

        transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime); 

        if(Vector2.Distance(transform.position, target.position) < 1.0f)
        {
            WaitAtWaypoint(); 
        }
    }

    IEnumerator WaitAtWaypoint()
    {
        isWaiting = true; 
        yield return new WaitForSeconds(waitTime); 

        // if looping is enabled : increment currentWaypointIndex and wrap around if needed
        // if not looping: increment currentWaypointIndex but dont exceed last waypoint 
        currentWaypointIndex = loopWaypoints ? (currentWaypointIndex + 1) % waypoints.length : Mathf.Min(currentWaypointIndex + 1, _waypoints.Length - 1); 
    }
}