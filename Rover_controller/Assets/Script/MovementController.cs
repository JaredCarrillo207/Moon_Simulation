using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public float speed = 5f; // Adjust this value to set the tank's speed
    public float turnSpeed = 50f; // Adjust this value to control how fast the tank turns

    void Update()
    {
        // Get input from keyboard or game controller
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate the movement amount based on the vertical input
        Vector3 movement = transform.forward * verticalInput * speed * Time.deltaTime;
        transform.position += movement;

        // Calculate the turning amount based on horizontal input
        float turnAmount = horizontalInput * turnSpeed * Time.deltaTime;

        // Rotate the tank around the Y-axis
        transform.Rotate(0f, turnAmount, 0f);
    }
}