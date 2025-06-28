using UnityEngine;

public class IntroBehaviour : StateMachineBehaviour
{
    private int randomState;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        randomState = Random.Range(0, 3);

        if (randomState == 0)
        {
            animator.SetTrigger("idle");
        }
        else if(randomState == 1)
        {
            animator.SetTrigger("attack_one");
        }
        else
        {
            animator.SetTrigger("attack_two");
        }
    }

  
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

   
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

}
