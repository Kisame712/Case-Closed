using UnityEngine;

public class Popper : MonoBehaviour
{
    public GameObject controlledObject;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            controlledObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            controlledObject.SetActive(false);
        }
    }
}
