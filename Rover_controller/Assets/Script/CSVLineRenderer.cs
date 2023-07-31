using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CSVLineRenderer : MonoBehaviour
{
    // Public variable to store the file path of the CSV file.
    public string csvFilePath;

    // Prefab for the sphere to be instantiated.
    public GameObject spherePrefab;

    // Public variable to control the multiplication factor for x, y, and z data.
    public float positionMultiplyFactor = 0.003f;

    // Public variable to store the starting position GameObject.
    public GameObject startingPosition;

    void Start()
    {
        // Check if the CSV file path is not empty.
        if (!string.IsNullOrEmpty(csvFilePath))
        {
            // Read the contents of the CSV file.
            List<string> lines = new List<string>(File.ReadAllLines(csvFilePath));

            // Loop through each line of the CSV file (skipping the first line with headers).
            for (int i = 1; i < lines.Count; i++)
            {
                // Split the data using the comma as the separator.
                string[] data = lines[i].Split(',');

                // Parse the data values as needed (assuming the format is "x, y, z, rotation angle, rotation x, rotation y, rotation z").
                float x = float.Parse(data[1]) * positionMultiplyFactor;
                float y = float.Parse(data[2]) * positionMultiplyFactor;
                float z = float.Parse(data[3]) * positionMultiplyFactor;
                float rotationAngle = float.Parse(data[4]);
                float rotationX = float.Parse(data[5]);
                float rotationY = float.Parse(data[6]);
                float rotationZ = float.Parse(data[7]);

                // Create a new Vector3 for the local position relative to the starting position GameObject.
                Vector3 localPosition = new Vector3(x, y, z);

                // Instantiate the sphere prefab at the local position relative to the starting position.
                GameObject sphere = Instantiate(spherePrefab, startingPosition.transform.position + localPosition, Quaternion.identity);

                // Set the parent of the instantiated sphere to the starting position GameObject.
                sphere.transform.SetParent(startingPosition.transform);

                // Set the local rotation of the sphere relative to the starting position.
                sphere.transform.localRotation = Quaternion.Euler(rotationX, rotationY, rotationZ);


            }
        }
        else
        {
            Debug.LogError("CSV file path is empty!");
        }
    }
}