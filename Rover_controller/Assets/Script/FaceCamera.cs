using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    public Camera mainCamera;
    public float rotationSpeed = 5f; // Adjust this value to control the rotation speed

    void Start()
    {
        // Make sure to assign the main camera to this script
        mainCamera = Camera.main;

        if (mainCamera == null)
        {
            Debug.LogError("Main camera not found! Make sure you have a camera in the scene tagged as 'MainCamera'.");
        }
    }

    void Update()
    {
        if (mainCamera != null)
        {
            // Get the direction from the quad to the camera
            Vector3 directionToCamera = mainCamera.transform.position - transform.position;

            // Calculate the new rotation to face the camera with an offset of 180 degrees
            Quaternion targetRotation = Quaternion.LookRotation(directionToCamera, Vector3.up) * Quaternion.Euler(0f, 180f, 0f);

            // Smoothly rotate the quad towards the camera over time
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
