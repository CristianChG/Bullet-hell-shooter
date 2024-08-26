using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBoundary : MonoBehaviour
{
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Bullet") || other.CompareTag("Player") || other.CompareTag("Boss") || other.CompareTag("BulletBoss"))
        {
            Destroy(other.gameObject);
        }
        {
            if (other.CompareTag("Bullet"))
            {
                Destroy(other.gameObject);
            }
            else
            {
                // Restringir al jugador y jefe para que no salgan del mapa
                Vector3 newPosition = other.transform.position;
                newPosition.x = Mathf.Clamp(newPosition.x, -limitX, limitX);
                newPosition.z = Mathf.Clamp(newPosition.z, -limitZ, limitZ);
                other.transform.position = newPosition;
            }
        }
    }
    public float limitX = 50f;
    public float limitZ = 100f;
}
