using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HelicopterController : MonoBehaviour
{
    public GameObject bulletHelicopterPrefab;
    public Transform bulletSpawnPoint;
    public float moveSpeed = 5f;
    public float bulletSpeed = 25f;
    private int currentMode = 1;
    public int maxHealth = 15; 
    private int currentHealth;
    public GameObject LevelCompleteText;
    public TextMeshProUGUI bossHealthText;

    void Start()
    {
        StartCoroutine(BossRoutine());
        currentHealth = maxHealth;      
        LevelCompleteText.SetActive(false);
        UpdateHealthUI(); 
    }
    public void TakeDamage(int damage)
    {
        UpdateHealthUI();
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
        ShowLevelComplete();
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
            transform.position += new Vector3(Mathf.Cos(Time.time) * moveSpeed * Time.deltaTime, 0, Mathf.Sin(Time.time) * moveSpeed * Time.deltaTime);
            
            bulletSpawnPoint.localPosition = new Vector3(Mathf.Cos(Time.time) * 1f, bulletSpawnPoint.localPosition.y, Mathf.Sin(Time.time) * 1f);
            
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
            // Movimiento en signo de infinito
            transform.position += new Vector3(Mathf.Sin(Time.time) * moveSpeed * Time.deltaTime, 0, Mathf.Sin(Time.time * 2f) * moveSpeed * Time.deltaTime);
            
            // Disparos en 8 direcciones con balas más lentas
            for (int i = 0; i < 8; i++)
            {
                float angle = i * 45f;
                Quaternion rotation = Quaternion.Euler(0, angle, 0);
                Shoot(rotation, bulletSpeed / 4);  // Balas más lentas (cuarta parte de la velocidad)
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

        transform.position += new Vector3(Mathf.Cos(Time.time) * moveSpeed * Time.deltaTime, 0, Mathf.Cos(Time.time * 2f) * moveSpeed * Time.deltaTime);


        bulletSpawnPoint.Rotate(0, 45f * Time.deltaTime, 0);  
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
        GameObject bullet = Instantiate(bulletHelicopterPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation * rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = bullet.transform.forward * (speed > 0 ? speed : bulletSpeed);
    }

    void ShowLevelComplete()
    {
        LevelCompleteText.SetActive(true);
    }

    void UpdateHealthUI()
    {
        if (bossHealthText != null)
        {
            bossHealthText.text = "Boss Health: " + currentHealth;
        }
    }
}
