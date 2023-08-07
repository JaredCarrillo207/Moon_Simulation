using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleFollow : MonoBehaviour
{
    public Transform roverOne, roverTwo, roverThree, roverViper; // Reference to the game object you want to copy

    private Transform objectFocus;

    private Vector3 lastLocalPosition; // Local position of the target object in the previous frame

    public Material dissolveMaterial, rockDissolve, wheelDissolve, chassisDissolve, moonDissolve; // Assign the material using the "Custom/CenterOnlyShader" shader in the Inspector

    public GameObject Rover1Card, Rover2Card, Rover3Card, Rover4Card, worldScale, baseScale, moonButton, objectToRotate;
    public float dissolveInterpolation = 0.02f;
    public float dissolveInterpolation2 = 1.0f;
    public float interpolationDuration = 1.0f;
    public float interpolationDuration2 = 5.0f;
    public float targetInterpolation;
    public float targetInterpolation2;


    public Vector3 zoomInScale = new Vector3(1f, 1f, 1f);
    public Vector3 zoomOutScale = new Vector3(0.1f, 0.1f, 0.1f);// Set the target scale you want to reach.
    public Vector3 zoomOutOutScale = new Vector3(0.02f, 0.02f, 0.02f);// Set the target scale you want to reach.

    public Vector3 zoomBaseInScale = new Vector3(1.04f, 0.03f, 1.04f);
    public Vector3 zoomBaseOutScale = new Vector3(3.0f, 0.03f, 3.0f);

    private float duration = 3f;


    public float scalingSpeed = 1f;

    private Vector3 initialScale;
    private Vector3 initialScale2;
    private Vector3 initialScale3;
    private Vector3 initialScale4;
    private Vector3 initialScale5;
    private Coroutine scalingCoroutine;


    public float followSpeed = 0.1f; // Adjust this to control the speed of interpolation

    public GameObject openingSound;




    void Start()
    {
        moonDissolve.SetFloat("_Interpolation", 15f);

        //worldScale.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);

        dissolveInterpolation = 0.02f;

        targetInterpolation = dissolveInterpolation;
        targetInterpolation2 = dissolveInterpolation2;

        objectFocus = roverOne;

        Rover2Card.SetActive(false);
        Rover3Card.SetActive(false);
        Rover4Card.SetActive(false);
        // Check if the target object is assigned
        if (objectFocus != null)
        {
            // Initialize lastLocalPosition with the initial local position of the target object
            lastLocalPosition = objectFocus.localPosition;

        }
        else
        {
            Debug.LogWarning("ObjectToCopy is not assigned. Please assign it in the inspector.");
        }
    }

    void Update()
    {
        // Check if the target object is assigned
        if (objectFocus != null)
        {
            // Calculate the local position change since the last frame
            Vector3 localPositionChange = objectFocus.localPosition - lastLocalPosition;

            // Apply the inverse local position change to the attached object
            transform.localPosition -= localPositionChange;

            // Update lastLocalPosition to the current local position of the target object
            lastLocalPosition = objectFocus.localPosition;


        }

        // Check if the dissolve material and target object are assigned
        if (dissolveMaterial != null && objectFocus != null)
        {
            // Get the position of the target object in world space
            Vector3 targetPosition = objectFocus.position;

            // Update the "_Center" property of the dissolve material to match the target object's position
            dissolveMaterial.SetVector("_Center", new Vector4(targetPosition.x, targetPosition.y, targetPosition.z, 0));
            dissolveMaterial.SetFloat("_Interpolation", dissolveInterpolation);
        }

        if (rockDissolve != null && objectFocus != null)
        {
            // Get the position of the target object in world space
            Vector3 targetPosition = objectFocus.position;

            // Update the "_Center" property of the dissolve material to match the target object's position
            rockDissolve.SetVector("_Center", new Vector4(targetPosition.x, targetPosition.y, targetPosition.z, 0));
            rockDissolve.SetFloat("_Interpolation", dissolveInterpolation);
        }

        if (wheelDissolve != null && objectFocus != null)
        {
            // Get the position of the target object in world space
            Vector3 targetPosition = objectFocus.position;

            // Update the "_Center" property of the dissolve material to match the target object's position
            wheelDissolve.SetVector("_Center", new Vector4(targetPosition.x, targetPosition.y, targetPosition.z, 0));
            wheelDissolve.SetFloat("_Interpolation", dissolveInterpolation);
        }

        if (chassisDissolve != null && objectFocus != null)
        {
            // Get the position of the target object in world space
            Vector3 targetPosition = objectFocus.position;

            // Update the "_Center" property of the dissolve material to match the target object's position
            chassisDissolve.SetVector("_Center", new Vector4(targetPosition.x, targetPosition.y, targetPosition.z, 0));
            chassisDissolve.SetFloat("_Interpolation", dissolveInterpolation);
        }


    }

    private IEnumerator ScaleObjectGradually()
    {
        
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * scalingSpeed;
            worldScale.transform.localScale = Vector3.Lerp(initialScale, zoomInScale, t);
            yield return null;
        }

        worldScale.transform.localScale = zoomInScale;// To ensure it reaches the exact target scale.
    }

    private IEnumerator ScaleObjectGradually2()
    {

        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * scalingSpeed;
            worldScale.transform.localScale = Vector3.Lerp(initialScale2, zoomOutScale, t);
            yield return null;
        }

        worldScale.transform.localScale = zoomOutScale; // To ensure it reaches the exact target scale.
    }




    private IEnumerator ScaleObjectGradually3()
    {

        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * scalingSpeed;
            baseScale.transform.localScale = Vector3.Lerp(initialScale3, zoomBaseInScale, t);
            yield return null;
        }

        baseScale.transform.localScale = zoomBaseInScale;// To ensure it reaches the exact target scale.
    }

    private IEnumerator ScaleObjectGradually4()
    {

        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * scalingSpeed;
            baseScale.transform.localScale = Vector3.Lerp(initialScale4, zoomBaseOutScale, t);
            yield return null;
        }

        baseScale.transform.localScale = zoomBaseOutScale; // To ensure it reaches the exact target scale.
    }

    private IEnumerator ScaleObjectGradually5()
    {

        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * scalingSpeed;
            worldScale.transform.localScale = Vector3.Lerp(initialScale5, zoomOutOutScale, t);
            yield return null;
        }

        worldScale.transform.localScale = zoomOutOutScale; // To ensure it reaches the exact target scale.
    }



    private IEnumerator InterpolateDissolve(float targetInterpolation)
    {
        float startTime = Time.time;
        float startInterpolation = dissolveInterpolation;

        while (Time.time < startTime + interpolationDuration)
        {
            float t = (Time.time - startTime) / interpolationDuration;
            dissolveInterpolation = Mathf.Lerp(startInterpolation, targetInterpolation, t);

            // Update the "_Center" property of the dissolve material to match the target object's position
            if (dissolveMaterial != null && objectFocus != null)
            {
                Vector3 targetPosition = objectFocus.position;
                dissolveMaterial.SetVector("_Center", new Vector4(targetPosition.x, targetPosition.y, targetPosition.z, 0));
            }

            // Update the "_Interpolation" property of the dissolve material
            dissolveMaterial.SetFloat("_Interpolation", dissolveInterpolation);

            yield return null;
        }

        // Ensure the final value is set exactly to the target value
        dissolveInterpolation = targetInterpolation;
        dissolveMaterial.SetFloat("_Interpolation", dissolveInterpolation);
    }

    private IEnumerator ChangeTargetGradually(Transform newTarget)
    {
        // Gradually move towards the new target position
        Vector3 initialPosition = transform.position;
        Vector3 targetPosition = newTarget.position;

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * followSpeed;
            transform.position = Vector3.Lerp(targetPosition, initialPosition, t); // Inverse Lerp from target to initial position
            yield return null;
        }

        // Set the new target after the transition is complete
        objectFocus = newTarget;
    }





    private IEnumerator InterpolateDissolveMoon()
    {
        float startTime = Time.time;
        float elapsedTime = 0f;

        // Get the starting value of _Interpolation
        float startInterpolation = moonDissolve.GetFloat("_Interpolation");

        while (elapsedTime < duration)
        {
            // Calculate the progress of interpolation from 0 to 1
            float t = elapsedTime / duration;

            // Use Mathf.Lerp to smoothly interpolate the value from startInterpolation to 0
            float currentInterpolation = Mathf.Lerp(startInterpolation, 0f, t);

            // Set the new _Interpolation value to the shader
            moonDissolve.SetFloat("_Interpolation", currentInterpolation);

            // Wait for the next frame
            yield return null;

            // Update elapsed time
            elapsedTime = Time.time - startTime;
        }

        // Ensure that the _Interpolation value is set to 0 exactly
        moonDissolve.SetFloat("_Interpolation", 0f);
    }






    public void followRover1()
    {
        worldScale.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        StartCoroutine(ChangeTargetGradually(roverOne));
        Rover1Card.SetActive(true);
        Rover2Card.SetActive(false);
        Rover3Card.SetActive(false);
        Rover4Card.SetActive(false);

    }

    public void followRover2()
    {
        worldScale.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        StartCoroutine(ChangeTargetGradually(roverTwo));
        Rover1Card.SetActive(false);
        Rover2Card.SetActive(true);
        Rover3Card.SetActive(false);
        Rover4Card.SetActive(false);
    }

    public void followRover3()
    {
        worldScale.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        StartCoroutine(ChangeTargetGradually(roverThree));
        Rover1Card.SetActive(false);
        Rover2Card.SetActive(false);
        Rover3Card.SetActive(true);
        Rover4Card.SetActive(false);
    }

    public void followViper()
    {
        worldScale.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        StartCoroutine(ChangeTargetGradually(roverViper));
        Rover1Card.SetActive(false);
        Rover2Card.SetActive(false);
        Rover3Card.SetActive(false);
        Rover4Card.SetActive(true);
    }


    public void scale10()
    {
        StartCoroutine(InterpolateDissolve(targetInterpolation));
        initialScale3 = baseScale.transform.localScale;
        scalingCoroutine = StartCoroutine(ScaleObjectGradually3());
    }

    public void scale80()
    {
        StartCoroutine(InterpolateDissolve(targetInterpolation2));
        initialScale4 = baseScale.transform.localScale;
        scalingCoroutine = StartCoroutine(ScaleObjectGradually4());
    }

    public void zoomIn()
    {
        if(objectFocus != roverViper)
        {
            initialScale = worldScale.transform.localScale;
            scalingCoroutine = StartCoroutine(ScaleObjectGradually());
        }
        else
        {
            initialScale2 = worldScale.transform.localScale;
            scalingCoroutine = StartCoroutine(ScaleObjectGradually2());
        }
    }

    public void zoomOut()
    {
        if (objectFocus != roverViper)
        {
            initialScale2 = worldScale.transform.localScale;
            scalingCoroutine = StartCoroutine(ScaleObjectGradually2());
        }
        else
        {
            initialScale5 = worldScale.transform.localScale;
            scalingCoroutine = StartCoroutine(ScaleObjectGradually5());
        }
        //worldScale.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
    }

    public void scaleButton()
    {
        if (dissolveInterpolation != 1.0f)
        {
            scale80();

        }
        else
        {
            scale10();

        }
    }

    public void zoomButton()
    {
        if (worldScale.transform.localScale == zoomInScale)
        {
            zoomOut();

        }
        else
        {
            zoomIn();

        }
    }


    public void roverButton()
    {
        if (objectFocus == roverOne)
        {
            followRover2();
        }
        if (objectFocus == roverTwo)
        {
            followRover3();
        }
        if (objectFocus == roverThree)
        {
            followViper();
        }
        if (objectFocus == roverViper)
        {
            followRover1();
        }
    }



    public void moonExpand()
    {
        openingSound.SetActive(true);
        StartCoroutine(InterpolateDissolveMoon());
        moonButton.SetActive(false);
    }



    /**public void rotate90()
    {
        if (objectToRotate == null)
        {
            Debug.LogError("Please assign a GameObject to objectToRotate variable in the Inspector.");
            return;
        }

        // Get the current local rotation of the game object
        Quaternion currentRotation = objectToRotate.transform.localRotation;

        // Calculate the new local rotation by adding a 90-degree rotation around the y-axis
        Quaternion newRotation = Quaternion.Euler(0f, 90f, 0f) * currentRotation;

        // Apply the new local rotation to the game object
        objectToRotate.transform.localRotation = newRotation;
    }**/



}