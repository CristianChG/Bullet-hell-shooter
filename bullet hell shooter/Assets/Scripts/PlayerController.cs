using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{
    public float speed = 40;
    public float turnSpeed = 90;

    public float slowSpeed = 20;
    public float horizontalInput;
    public float forwardInput;
    public string inputId;
    public bool isSlowMode = false;

    public GameObject bulletPrefab;
    public Transform spawnPoint;
    public float bulletSpeed = 50f;
    public float fireRate = 0.1f; 
    private float nextFire = 0.0f;

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
        // Cambiar entre modos Normal y Lento
        if (Input.GetKeyDown(KeyCode.C))
        {
            isSlowMode = !isSlowMode;
        }

        float currentSpeed = isSlowMode ? slowSpeed : speed;

        horizontalInput = Input.GetAxis("Horizontal" + inputId);
        forwardInput = Input.GetAxis("Vertical" + inputId);

        transform.Translate(Vector3.forward * Time.deltaTime * currentSpeed * forwardInput);
        transform.Rotate(Vector3.up, Time.deltaTime * turnSpeed * horizontalInput);

        if (Input.GetKey(KeyCode.Return) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            shoot();
        }

        Vector3 pos = transform.position;

        pos.x = Mathf.Clamp(pos.x, -limitX, limitX);
        pos.z = Mathf.Clamp(pos.z, -limitZ, limitZ);

        transform.position = pos;

        if (isSlowMode)
        {
            Time.timeScale = 0.5f; 
        }
        else
        {
            Time.timeScale = 1f; 
        }
    }
}
