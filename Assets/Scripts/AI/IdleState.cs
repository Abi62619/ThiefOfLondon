using UnityEngine;

public class IdleState : StateMachineBehaviour
{
    private float timer;
    private float waitTime = 2;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer += Time.deltaTime;
        if (timer > waitTime)
        {
            animator.SetTrigger("Patrol");
        }

    }


}
