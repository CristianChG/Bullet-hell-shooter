using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class BulletCounter : MonoBehaviour
{
    public TextMeshProUGUI bulletCountText;

    // Update is called once per frame
    void Update()
    {
        // Cuenta cuántos objetos con el tag "Bullet" hay en la escena
        int bulletCount = GameObject.FindGameObjectsWithTag("Bullet").Length;

        // Actualiza el texto del UI con el número de balas
        bulletCountText.text = "Bullets: " + bulletCount.ToString();
    }
}
