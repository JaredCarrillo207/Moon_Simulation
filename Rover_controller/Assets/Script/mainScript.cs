using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainScript : MonoBehaviour
{
    public GameObject mainCamera, modeSelect, terrainSelect, rockSelect, roverSelect, mapBumpy, viperRover, cadreRover;


    void Start()
    {
        modeSelect.SetActive(true);
        terrainSelect.SetActive(false);
        rockSelect.SetActive(false);
        roverSelect.SetActive(false);
        mapBumpy.SetActive(false);

    }

    void Update()
    {


    }


    public void gameModeDemoMode()
    {
        modeSelect.SetActive(false);
        terrainSelect.SetActive(true);
    }

    public void terrainSelectBumpy()
    {
        terrainSelect.SetActive(false);
        mapBumpy.SetActive(true);
        rockSelect.SetActive(true);
    }

    public void rockSelectSave()
    {
        rockSelect.SetActive(false);
        roverSelect.SetActive(true);
    }


    public void placeCadreRover()
    {
        if (mainCamera != null)
        {
            Vector3 spawnPosition = mainCamera.transform.position + mainCamera.transform.forward * 2.0f; // Adjust the distance as needed
            Instantiate(cadreRover, spawnPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogError("Main camera GameObject not assigned.");
        }
    }

    public void placeViperRover()
    {
        if (mainCamera != null)
        {
            Vector3 spawnPosition = mainCamera.transform.position + mainCamera.transform.forward * 2.0f; // Adjust the distance as needed
            Instantiate(viperRover, spawnPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogError("Main camera GameObject not assigned.");
        }
    }

}