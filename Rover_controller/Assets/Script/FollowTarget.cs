using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public Transform objectToCopy; // Reference to the game object you want to copy

    private Vector3 lastLocalPosition; // Local position of the target object in the previous frame

    void Start()
    {
        // Check if the target object is assigned
        if (objectToCopy != null)
        {
            // Initialize lastLocalPosition with the initial local position of the target object
            lastLocalPosition = objectToCopy.localPosition;
        }
        else
        {
            Debug.LogWarning("ObjectToCopy is not assigned. Please assign it in the inspector.");
        }
    }

    void Update()
    {
        // Check if the target object is assigned
        if (objectToCopy != null)
        {
            // Calculate the local position change since the last frame
            Vector3 localPositionChange = objectToCopy.localPosition - lastLocalPosition;

            // Apply the inverse local position change to the attached object
            transform.localPosition -= localPositionChange;

            // Update lastLocalPosition to the current local position of the target object
            lastLocalPosition = objectToCopy.localPosition;









        }
    }
}