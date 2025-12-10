using UnityEngine;

public class PatrolState : StateMachineBehaviour
{

    private AIController ai;
    private Transform targetPoint;


    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ai = animator.GetComponent<AIController>();

        targetPoint= ai.GetNextPoint();
        ai.agent.isStopped = false;
        ai.agent.SetDestination(targetPoint.position);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(!ai.agent.pathPending && ai.agent.remainingDistance <= ai.agent.stoppingDistance)
        {
            animator.SetTrigger("Idle");
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ai.agent.isStopped = true;
    }




}
