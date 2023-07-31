using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveCenterController : MonoBehaviour
{
    public Material dissolveMaterial; // Assign the material using the "Custom/CenterOnlyShader" shader in the Inspector
    public Transform targetObject; // Assign the game object you want to track in the Inspector

    void Update()
    {
        // Check if the dissolve material and target object are assigned
        if (dissolveMaterial != null && targetObject != null)
        {
            // Get the position of the target object in world space
            Vector3 targetPosition = targetObject.position;

            // Update the "_Center" property of the dissolve material to match the target object's position
            dissolveMaterial.SetVector("_Center", new Vector4(targetPosition.x, targetPosition.y, targetPosition.z, 0));
        }
    }
}
