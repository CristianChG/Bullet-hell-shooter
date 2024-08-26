using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HeliceRotation : MonoBehaviour
{
    public float rotationSpeed = 1000f; // Velocidad de rotación en grados por segundo

    void Update()
    {
        // Rotar las hélices alrededor de su propio eje Z (ajusta según el eje de rotación correcto)
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }
}
