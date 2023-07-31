using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float rotationSpeed = 50f; // You can adjust this value in the Inspector to control the rotation speed.

    private Vector3 previousPosition;
    private bool shouldRotate = false;

    // Start is called before the first frame update
    void Start()
    {
        // Record the initial position of the parent object
        previousPosition = transform.parent.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentPosition = transform.parent.position;

        if (currentPosition != previousPosition)
        {
            shouldRotate = true;
        }
        else
        {
            shouldRotate = false;
        }

        if (shouldRotate)
        {
            // Rotate the object around its local Z-axis continuously based on the rotationSpeed variable.
            transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime, Space.Self);
        }

        // Update the previous position for the next frame
        previousPosition = currentPosition;
    }
}
