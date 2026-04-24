using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class NPCPolice : MonoBehaviour
{
    [Header("NPC movement settings")]
    [SerializeField] private Transform player;
    [SerializeField] private float viewDistance = 10f;
    [SerializeField] private float sprintSpeed = 7f; 
    private NavMeshAgent agent;
    private Rigidbody playerRb; 

    [Header("Police Alerted Settings")]
    [SerializeField] private bool isPoliceAlerted; 
    public PickPocketManager pickpocketManager; 

    [Header("Animation Settings")]
    [SerializeField] private Animator policeAnim; 
    [SerializeField] private float idleTimeBeforeStop = 1.5f;
    private float idleTimer; 

    [Header("Lose Sight Settings")]
    [SerializeField] private float loseSightTime = 120f; // 2 minutes
    private float loseSightTimer = 0f;
    private bool canSeePlayer = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        policeAnim = GetComponent<Animator>();

        Debug.Log("NPCPolice START");

        if (player != null)
        {
            playerRb = player.GetComponent<Rigidbody>();
            Debug.Log("Player assigned: " + player.name);
        }
        else
        {
            //Debug.LogError("NPCPolice: Player Transform is NOT assigned!", this);
        }
    }

    void Update()
    {
        //Debug.Log("Is Police Alerted: " + isPoliceAlerted);

        if (isPoliceAlerted)
        {
            //Debug.Log("Calling Movement()");
            Movement();
        }
    }

    public void Movement()
    {
        if (playerRb == null)
        {
            //Debug.LogWarning("Player Rigidbody is NULL");
            return;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        Debug.Log("Distance to player: " + distanceToPlayer);

        if (distanceToPlayer > viewDistance)
        {
            Debug.Log("Player OUTSIDE view distance");
            PlayerNotVisible();
            StopMoving();
            return;
        }

        RaycastHit hit;
        Vector3 directionToPlayer = (player.position - transform.position).normalized;

        Debug.DrawRay(transform.position, directionToPlayer * viewDistance, Color.red);

        if (Physics.Raycast(transform.position, directionToPlayer, out hit, viewDistance))
        {
            Debug.Log("Raycast HIT: " + hit.transform.name);

            if (hit.transform == player)
            {
                //Debug.Log("Player VISIBLE - CHASING");

                canSeePlayer = true;
                loseSightTimer = 0f;

                agent.speed = sprintSpeed;
                policeAnim.SetBool("isPoliceSprinting", true);
                agent.SetDestination(player.position);

                idleTimer = 0f;
            }
            else
            {
                Debug.Log("Raycast hit something ELSE");
                PlayerNotVisible();
                StopMoving();
            }
        }
        else
        {
            Debug.Log("Raycast hit NOTHING");
        }
    }

    void StopMoving()
    {
        //Debug.Log("Stopping movement");

        agent.ResetPath(); 
        agent.speed = 0f;

        idleTimer += Time.deltaTime;

        if (idleTimer >= idleTimeBeforeStop)
        {
            //Debug.Log("Switching to idle animation");
            policeAnim.SetBool("isPoliceSprinting", false);
        }
    }

    void PlayerNotVisible()
    {
        canSeePlayer = false;
        loseSightTimer += Time.deltaTime;

        Debug.Log("Player NOT visible. Timer: " + loseSightTimer);

        if (loseSightTimer >= loseSightTime)
        {
            //Debug.Log("Police is NO LONGER alerted");
            isPoliceAlerted = false;

            agent.ResetPath();
            //policeAnim.SetBool("isPoliceSprinting", false);
        }
    }

    public void Alerted()
    {
        Debug.Log("POLICE HAS BEEN ALERTED!");
        isPoliceAlerted = true; 
    }

    public void Arrested()
    {
        /*if (gameObject.CompareTag("Player"))
        {
            Time.timeScale = 0f;
        }*/
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player has been arrested");
            Time.timeScale = 0f;
            SceneManager.LoadScene("Arrested"); 
        }
    }
}