using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine;

public class SliderScript : MonoBehaviour
{
    public PinchSlider sourceScriptReference;

    public float valueFromSource;

    public float rotationAngle = 360f; // The total rotation angle in degrees (0 to 360).
    private float startingRotationY;

    void Start()
    {

        sourceScriptReference.SliderValue = 1;
        // Store the initial Y-axis rotation of the game object.
        startingRotationY = transform.localEulerAngles.y;

    }

    private void Update()
    {
        valueFromSource = sourceScriptReference.SliderValue;
        // Calculate the target Y-axis rotation based on the "valuefromsource" variable.
        float targetRotationY = startingRotationY + valueFromSource * rotationAngle;

        // Smoothly rotate the game object to the target rotation.
        transform.localEulerAngles = new Vector3(
            transform.localEulerAngles.x,
            Mathf.LerpAngle(transform.localEulerAngles.y, targetRotationY, Time.deltaTime * 5f),
            transform.localEulerAngles.z
        );
    }
}