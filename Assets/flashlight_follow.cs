using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flashlight_follow : MonoBehaviour
{
    public Transform player;      // Reference to the player's transform
    public Transform headTransform; // Reference to the head (or another point) on the player
    public float rotationSpeed = 30f; // Speed of rotation

    void Update()
    {
        if (player != null && headTransform != null)
        {
            // Set the light's position above the player's head
            transform.position = headTransform.position + Vector3.up * 2;

            // Rotate the light gradually
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }
}