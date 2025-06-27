using UnityEditor;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    public float activeRange;
    public bool magnetActive;

    Player player;
    Animator magnetAnim;

    void Start()
    {
        player = FindFirstObjectByType<Player>();
        magnetAnim = GetComponent<Animator>();
    }

    
    void Update()
    {
        if(Vector2.Distance(transform.position, player.gameObject.transform.position) < activeRange)
        {
            magnetAnim.SetBool("isActive", true);
            magnetActive = true;
        }
        else
        {
            magnetAnim.SetBool("isActive", false);
            magnetActive = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.green;
        Handles.DrawWireDisc(transform.position, Vector3.forward, activeRange);
    }
}
