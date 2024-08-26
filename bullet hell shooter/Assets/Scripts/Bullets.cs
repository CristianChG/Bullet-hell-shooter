using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    public float limitX = 50f;
    public float limitZ = 100f;

    void Update()
    {
        if (transform.position.x < -limitX || transform.position.x > limitX ||
            transform.position.z < -limitZ || transform.position.z > limitZ)
        {
            Destroy(gameObject);
        }
    }
}
