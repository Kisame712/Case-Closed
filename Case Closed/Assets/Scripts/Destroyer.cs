using UnityEngine;

public class Destroyer : MonoBehaviour
{
    public float bulletRange;
    void Start()
    {
        Destroy(gameObject, bulletRange);  
    }

}
