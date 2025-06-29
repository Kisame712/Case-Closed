using UnityEngine;

public class Destroyer : MonoBehaviour
{
    public float destroyAfter;
    void Start()
    {
        Destroy(gameObject, destroyAfter);  
    }

}
