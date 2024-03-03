using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;

public class ObjectPlacerScript : MonoBehaviour
{
    public GameObject smallRockPrefab, mainCamera;
    public GameObject bigRockPrefab;
    public Transform terrain;

    public int numberOfSmallRocks = 20;
    public int numberOfBigRocks = 10;

    public PinchSlider sourceScriptReference;

    public float valueFromSource;




    void Start()
    {
        // You can call PlaceObjects here initially or trigger it with a button press.
        sourceScriptReference.SliderValue = 1;
    }

    void Update()
    {
        valueFromSource = sourceScriptReference.SliderValue;
    }


    public void PlaceObjectsOnClick()
    {
        PlaceObjects(smallRockPrefab, numberOfSmallRocks);
        PlaceObjects(bigRockPrefab, numberOfBigRocks);
    }

    void PlaceObjects(GameObject prefab, int count)
    {
        MeshCollider terrainCollider = terrain.GetComponent<MeshCollider>();

        if (terrainCollider == null)
        {
            Debug.LogError("Terrain does not have a MeshCollider component.");
            return;
        }

        List<Vector3> objectPositions = new List<Vector3>();

        for (int i = 0; i < count; i++)
        {
            Vector3 randomPoint = GetRandomPointOnTerrain(terrainCollider);
            if (!IsTooCloseToOtherObjects(randomPoint, objectPositions))
            {
                objectPositions.Add(randomPoint);
                Instantiate(prefab, randomPoint, Quaternion.identity);
            }
        }
    }

    Vector3 GetRandomPointOnTerrain(MeshCollider terrainCollider)
    {
        // Implementation for getting a random point on the terrain...
        Vector3 terrainSize = terrainCollider.bounds.size;
        Vector3 randomPoint = new Vector3(
            Random.Range(terrain.transform.position.x - terrainSize.x / 2, terrain.transform.position.x + terrainSize.x / 2),
            terrain.transform.position.y,
            Random.Range(terrain.transform.position.z - terrainSize.z / 2, terrain.transform.position.z + terrainSize.z / 2)
        );

        Ray ray = new Ray(randomPoint + Vector3.up * terrainSize.y, Vector3.down);
        RaycastHit hit;
        if (terrainCollider.Raycast(ray, out hit, terrainSize.y * 2))
        {
            randomPoint = hit.point;
        }

        return randomPoint;
    }

    bool IsTooCloseToOtherObjects(Vector3 position, List<Vector3> existingPositions)
    {
        // Implementation for checking if the point is too close to other objects...
        foreach (Vector3 existingPosition in existingPositions)
        {
            if (Vector3.Distance(position, existingPosition) < valueFromSource) // Adjust this distance as needed
            {
                return true;
            }
        }
        return false;
    }


    public void placeBigRock()
    {
        if (mainCamera != null)
        {
            Vector3 spawnPosition = mainCamera.transform.position + mainCamera.transform.forward * 2.0f; // Adjust the distance as needed
            Instantiate(bigRockPrefab, spawnPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogError("Main camera GameObject not assigned.");
        }
    }

    public void placeSmallRock()
    {
        if (mainCamera != null)
        {
            Vector3 spawnPosition = mainCamera.transform.position + mainCamera.transform.forward * 2.0f; // Adjust the distance as needed
            Instantiate(smallRockPrefab, spawnPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogError("Main camera GameObject not assigned.");
        }
    }

}