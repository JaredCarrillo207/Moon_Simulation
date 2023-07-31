using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusObject : MonoBehaviour
{
    public Transform targetObject;

    private Vector3 initialOffset;

    private void Start()
    {
        if (targetObject == null)
        {
            Debug.LogWarning("Target object is not assigned!");
            enabled = false; // Disable the script if the target object is not assigned
            return;
        }

        // Calculate the initial offset between this object's center and the target object's position
        initialOffset = transform.position - targetObject.position;
    }

    private void Update()
    {
        if (targetObject != null)
        {
            // Calculate the difference in positions between the current and initial offset
            Vector3 difference = transform.position - (targetObject.position + initialOffset);

            // Match the position of this object to the targetObject's center while keeping the offset
            transform.position = targetObject.position + initialOffset;
        }
    }
}