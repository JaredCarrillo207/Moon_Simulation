using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockDistributor : MonoBehaviour
{
    public GameObject moonLand; // Assign your moon land GameObject here
    public GameObject[] rockPrefabs; // Assign your rock prefabs here

    public float valueFromSource = 1.0f; // Distance between rocks

    private float minX, maxX, minZ, maxZ; // Bounds of the moon land

    void Start()
    {
        if (moonLand == null)
        {
            Debug.LogError("MoonLand is not assigned!");
            return;
        }

        if (rockPrefabs == null || rockPrefabs.Length == 0)
        {
            Debug.LogError("RockPrefabs are not assigned!");
            return;
        }

        // Calculate the bounds of the moon land GameObject
        Renderer renderer = moonLand.GetComponent<Renderer>();
        if (renderer == null)
        {
            Debug.LogError("MoonLand does not have a Renderer component!");
            return;
        }

        minX = moonLand.transform.position.x - renderer.bounds.size.x / 2;
        maxX = moonLand.transform.position.x + renderer.bounds.size.x / 2;
        minZ = moonLand.transform.position.z - renderer.bounds.size.z / 2;
        maxZ = moonLand.transform.position.z + renderer.bounds.size.z / 2;

        DistributeRocks();
    }

    public void DistributeRocks()
    {
        Debug.Log($"Starting to distribute rocks. Total rocks: {rockPrefabs.Length}");

        for (int i = 0; i < rockPrefabs.Length; i++)
        {
            Vector3 rockPosition = GetRandomPosition();

            // Check if the position is too close to other rocks
            int safetyNet = 0;
            while (IsTooCloseToOthers(rockPosition) && safetyNet < 100)
            {
                rockPosition = GetRandomPosition();
                safetyNet++;
            }

            if (safetyNet == 100)
            {
                Debug.LogWarning("Couldn't find a suitable position for rock after 100 attempts.");
                continue;
            }

            // Adjust the y-position slightly above the moon land surface to prevent z-fighting
            rockPosition.y += 0.1f;

            // Instantiate the rock prefab at the random position
            GameObject rockInstance = Instantiate(rockPrefabs[i], rockPosition, Quaternion.identity, moonLand.transform);
            Debug.Log($"Rock instantiated at position: {rockInstance.transform.position}, Prefab: {rockPrefabs[i].name}");

            // Log scale and active state for troubleshooting
            Debug.Log($"Rock scale: {rockInstance.transform.localScale}");
            Debug.Log($"Rock active state: {rockInstance.activeSelf}");
        }

        Debug.Log("Finished distributing rocks.");
    }



    Vector3 GetRandomPosition()
    {
        // Generate a random position within the bounds
        float x = Random.Range(minX, maxX);
        float z = Random.Range(minZ, maxZ);
        return new Vector3(x, moonLand.transform.position.y, z);
    }

    bool IsTooCloseToOthers(Vector3 position)
    {
        foreach (Transform rock in moonLand.transform)
        {
            if (Vector3.Distance(position, rock.position) < valueFromSource)
            {
                return true;
            }
        }
        return false;
    }
}
