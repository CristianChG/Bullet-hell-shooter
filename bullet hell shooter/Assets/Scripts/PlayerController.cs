using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{
    public float speed = 40;
    public float turnSpeed = 90;
    public float horizontalInput;
    public float forwardInput;
    public string inputId;

    public GameObject bulletPrefab;
    public Transform spawnPoint;
    public float bulletSpeed = 50f;

    public float limitX = 50f;  
    public float limitZ = 100f;
    void Start()
    {
        
    }

    void shoot()
    {
        // Instancia la bala en el spawn point y la dispara hacia adelante
        GameObject bullet = Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        bulletRb.velocity = spawnPoint.forward * bulletSpeed;
    }
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal" + inputId);
        forwardInput = Input.GetAxis("Vertical" + inputId);

        transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);
        transform.Rotate(Vector3.up, Time.deltaTime * turnSpeed * horizontalInput);

        if (Input.GetKeyDown(KeyCode.Return))
        {
            shoot();
        }

        Vector3 pos = transform.position;

        pos.x = Mathf.Clamp(pos.x, -limitX, limitX);

        pos.z = Mathf.Clamp(pos.z, -limitZ, limitZ);

        transform.position = pos;
    }
}
