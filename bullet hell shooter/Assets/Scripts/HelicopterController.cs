using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterController : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform spawnPoint;
    public float moveSpeed = 5f;
    public float bulletSpeed = 25f;
    private int currentMode = 1;
    void Start()
    {
        StartCoroutine(BossRoutine());        
    }
    IEnumerator BossRoutine()
    {
        while (true)
        {
            if (currentMode == 1)
            {
                yield return StartCoroutine(MoveInCircleAndShoot());
            }
            else if (currentMode == 2)
            {
                yield return StartCoroutine(MoveInInfinityAndShoot());
            }
            else if (currentMode == 3)
            {
                yield return StartCoroutine(MoveInStarAndShoot());
            }

            currentMode = (currentMode % 3) + 1;
        }
    }
    IEnumerator MoveInCircleAndShoot()
    {
        float time = 0;
        while (time < 10f)
        {
            // Movimiento en círculos
            transform.position += new Vector3(Mathf.Cos(Time.time) * moveSpeed * Time.deltaTime, 0, Mathf.Sin(Time.time) * moveSpeed * Time.deltaTime);
            
            // Disparos en 5 direcciones
            for (int i = 0; i < 5; i++)
            {
                float angle = i * 72f;
                Quaternion rotation = Quaternion.Euler(0, angle, 0);
                Shoot(rotation);
            }

            time += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator MoveInInfinityAndShoot()
    {
        float time = 0;
        while (time < 10f)
        {
            // Movimiento en forma de infinito
            transform.position += new Vector3(Mathf.Sin(Time.time) * moveSpeed * Time.deltaTime, 0, Mathf.Sin(Time.time * 2f) * moveSpeed * Time.deltaTime);
            
            // Disparos en 8 direcciones
            for (int i = 0; i < 8; i++)
            {
                float angle = i * 45f;
                Quaternion rotation = Quaternion.Euler(0, angle, 0);
                Shoot(rotation, bulletSpeed / 2); // Velocidad reducida
            }

            time += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator MoveInStarAndShoot()
    {
        float time = 0;
        while (time < 10f)
        {
            // Movimiento en forma de estrella (patrón manual)
            // Puedes diseñar este movimiento a tu gusto
            transform.position += new Vector3(Mathf.Cos(Time.time) * moveSpeed * Time.deltaTime, 0, Mathf.Cos(Time.time * 2f) * moveSpeed * Time.deltaTime);

            // Disparos formando una flor
            for (int i = 0; i < 8; i++)
            {
                float angle = i * 45f;
                Quaternion rotation = Quaternion.Euler(0, angle, 0);
                Shoot(rotation);
            }

            time += Time.deltaTime;
            yield return null;
        }
    }
    void Shoot(Quaternion rotation, float speed = -1)
    {
        GameObject bullet = Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation * rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = bullet.transform.forward * (speed > 0 ? speed : bulletSpeed);
    }
}
