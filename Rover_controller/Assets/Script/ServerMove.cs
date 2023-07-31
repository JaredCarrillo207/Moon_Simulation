using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.IO;
using System;
using System.Text;

public class ServerMove : MonoBehaviour
{
    public string serverIP = "192.168.50.215"; // IP address of the TCP server
    public int serverPort = 5000; // Port number of the TCP server
    public float movementSpeed = 1.0f; // Speed at which the object moves

    private TcpClient client;
    private StreamReader reader;

    private List<Vector3> positions = new List<Vector3>();
    private List<Quaternion> rotations = new List<Quaternion>();

    private int currentPositionIndex = 0;
    private int currentRotationIndex = 0;

    private float startTime;
    private float journeyLength;

    private void Start()
    {
        ConnectToServer();
        ReadDataFromServer();

        if (positions.Count == 0 || rotations.Count == 0)
        {
            Debug.LogError("Error reading data from server.");
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

        transform.position = Vector3.Lerp(positions[currentPositionIndex], positions[currentPositionIndex + 1], fractionOfJourney);
        transform.rotation = Quaternion.Lerp(rotations[currentRotationIndex], rotations[currentRotationIndex + 1], fractionOfJourney);

        if (fractionOfJourney >= 1.0f)
        {
            currentPositionIndex++;
            currentRotationIndex++;
            startTime = Time.time;
            journeyLength = Vector3.Distance(positions[currentPositionIndex], positions[currentPositionIndex + 1]);
        }
    }

    private void ConnectToServer()
    {
        try
        {
            client = new TcpClient(serverIP, serverPort);
            reader = new StreamReader(client.GetStream(), Encoding.ASCII);
            Debug.Log("Connected to server: " + serverIP + ":" + serverPort);
        }
        catch (Exception e)
        {
            Debug.LogError("Error connecting to server: " + e.Message);
        }
    }

    private void ReadDataFromServer()
    {
        try
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] data = line.Split(',');

                float x = float.Parse(data[0]);
                float y = float.Parse(data[1]);
                float z = float.Parse(data[2]);
                float rotAngle = float.Parse(data[3]);
                float rotX = float.Parse(data[4]);
                float rotY = float.Parse(data[5]);
                float rotZ = float.Parse(data[6]);

                Vector3 position = new Vector3(x, y, z);
                Quaternion rotation = new Quaternion(rotX, rotY, rotZ, rotAngle);

                positions.Add(position);
                rotations.Add(rotation);
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error reading data from server: " + e.Message);
        }
    }

    private void OnApplicationQuit()
    {
        if (client != null && client.Connected)
        {
            client.Close();
            reader.Close();
        }
    }
}
