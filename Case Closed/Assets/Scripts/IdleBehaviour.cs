using UnityEngine;

public class IdleBehaviour : StateMachineBehaviour
{
    private float timer;
    public float minTime;
    public float maxTime;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = Random.Range(minTime, maxTime);
    }

    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(timer <= 0)
        {
            int randomState = Random.Range(0, 2);
            if(randomState == 0)
            {
                animator.SetTrigger("attack_one");
            }
            else
            {
                animator.SetTrigger("attack_two");
            }
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }

    
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

}
