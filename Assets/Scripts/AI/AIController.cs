using UnityEditor.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    

    public Transform[] patrolPoints;
    public int currentPoint;

    [HideInInspector] public NavMeshAgent agent;
    [HideInInspector] public Animator animator;

    public bool isThrowAvailiable;
    public bool isSlashAvailiable;
    public bool isSwingAvailiable;

    public bool isSpecialAvailiable; 


    public void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    public Transform GetNextPoint()
    { 
        Transform point = patrolPoints[currentPoint];
        currentPoint = (currentPoint + 1 ) % patrolPoints.Length;
        return point;     
    
    
    }



}
