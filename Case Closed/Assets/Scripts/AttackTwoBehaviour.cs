using UnityEngine;

public class AttackTwoBehaviour : StateMachineBehaviour
{
    private float timer;
    public float minTime;
    public float maxTime;

    public float jumpSpeed;

    private Transform playerPos;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerPos = FindFirstObjectByType<Player>().GetComponent<Transform>();
        timer = Random.Range(minTime, maxTime);
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (timer <= 0)
        {
            int randomState = Random.Range(0, 2);
            if (randomState == 0)
            {
                animator.SetTrigger("idle");
            }
            else
            {
                animator.SetTrigger("attack_one");
            }
        }
        else
        {
            timer -= Time.deltaTime;
        }

        Vector2 target = new Vector2(playerPos.position.x, animator.transform.position.y);
        animator.transform.position = Vector2.MoveTowards(animator.transform.position, target, jumpSpeed * Time.deltaTime);

    }


    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

}
