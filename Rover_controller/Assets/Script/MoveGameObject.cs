using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MoveGameObject : MonoBehaviour
{
    public string csvFilePath; // Path to the CSV file
    public float movementSpeed = 1.0f; // Speed at which the object moves
    public float positionScaleFactor = 0.01f; // Factor to divide x, y, z positions by

    private List<Vector3> positions = new List<Vector3>();
    private List<Quaternion> rotations = new List<Quaternion>();
    private float[] timestamps;

    private int currentPositionIndex = 0;
    private int currentRotationIndex = 0;

    private float startTime;
    private float journeyLength;

    private void Start()
    {
        ReadCSVData();
        if (positions.Count == 0 || rotations.Count == 0 || timestamps.Length == 0)
        {
            Debug.LogError("Error reading CSV data.");
            return;
        }

        startTime = Time.time;
        journeyLength = Vector3.Distance(positions[0], positions[1]);
    }

    private void Update()
    {
        if (currentPositionIndex >= positions.Count - 1)
            return;

        float distanceCovered = (Time.time - startTime) * movementSpeed;
        float fractionOfJourney = distanceCovered / journeyLength;

        transform.localPosition = Vector3.Lerp(positions[currentPositionIndex], positions[currentPositionIndex + 1], fractionOfJourney);
        transform.localRotation = Quaternion.Lerp(rotations[currentRotationIndex], rotations[currentRotationIndex + 1], fractionOfJourney);

        if (fractionOfJourney >= 1.0f)
        {
            currentPositionIndex++;
            currentRotationIndex++;
            startTime = Time.time;
            journeyLength = Vector3.Distance(positions[currentPositionIndex], positions[currentPositionIndex + 1]);
        }
    }

    private void ReadCSVData()
    {
        if (!File.Exists(csvFilePath))
        {
            Debug.LogError("CSV file not found at path: " + csvFilePath);
            return;
        }

        string[] lines = File.ReadAllLines(csvFilePath);

        for (int i = 1; i < lines.Length; i++) // Skip the header row
        {
            string[] data = lines[i].Split(',');

            float time = float.Parse(data[0]);
            float x = float.Parse(data[1]) * positionScaleFactor;
            float y = float.Parse(data[3]) * positionScaleFactor;
            float z = float.Parse(data[2]) * positionScaleFactor;
            float rotAngle = float.Parse(data[4]);
            float rotX = float.Parse(data[5]);
            float rotY = float.Parse(data[7]);
            float rotZ = float.Parse(data[6]);

            Vector3 position = new Vector3(x, y, z);
            Quaternion rotation = new Quaternion(rotX, rotY, rotZ, rotAngle);

            positions.Add(position);
            rotations.Add(rotation);
        }

        timestamps = new float[positions.Count];
        for (int i = 0; i < positions.Count; i++)
        {
            timestamps[i] = float.Parse(lines[i + 1].Split(',')[0]);
        }
    }
}
