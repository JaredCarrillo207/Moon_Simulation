using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class TCPClient : MonoBehaviour
{
    public string host = "192.168.50.251"; // Replace with the server's IP address
    public int port = 55555; // Replace with the server's port number
    public int bufferSize = 32; // Adjust the buffer size to accommodate the data size

    private TcpClient client;
    private NetworkStream stream;
    private Thread receiveThread;

    private bool isReceiving = false;

    private void Start()
    {
        ConnectToServer();
    }

    private void OnDestroy()
    {
        DisconnectFromServer();
    }

    private void ConnectToServer()
    {
        try
        {
            client = new TcpClient();
            client.Connect(host, port);
            stream = client.GetStream();
            isReceiving = true;

            receiveThread = new Thread(ReceiveData);
            receiveThread.Start();
        }
        catch (Exception e)
        {
            Debug.LogError($"Error connecting to server: {e.Message}");
        }
    }

    private void DisconnectFromServer()
    {
        isReceiving = false;
        if (receiveThread != null && receiveThread.IsAlive)
        {
            receiveThread.Join();
        }

        if (stream != null)
        {
            stream.Close();
        }

        if (client != null)
        {
            client.Close();
        }
    }

    private void ReceiveData()
    {
        byte[] buffer = new byte[bufferSize];
        StringBuilder receivedDataBuilder = new StringBuilder();

        while (isReceiving)
        {
            try
            {
                int bytesRead = stream.Read(buffer, 0, bufferSize);
                if (bytesRead > 0)
                {
                    string receivedData = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    receivedDataBuilder.Append(receivedData);

                    // Check if the message is complete (ends with a newline character)
                    if (receivedData.EndsWith("\n"))
                    {
                        receivedData = receivedDataBuilder.ToString().Trim();
                        receivedDataBuilder.Clear();

                        Debug.Log("Received from server: " + receivedData);

                        // Parse the comma-separated string
                        string[] dataParts = receivedData.Split(',');
                        if (dataParts.Length == 8)
                        {
                            if (float.TryParse(dataParts[0], out float elapsed_time) &&
                                float.TryParse(dataParts[1], out float position_x) &&
                                float.TryParse(dataParts[2], out float position_y) &&
                                float.TryParse(dataParts[3], out float position_z) &&
                                float.TryParse(dataParts[4], out float rotation_r) &&
                                float.TryParse(dataParts[5], out float rotation_x) &&
                                float.TryParse(dataParts[6], out float rotation_y) &&
                                float.TryParse(dataParts[7], out float rotation_z))
                            {
                                Debug.Log("Elapsed Time: " + elapsed_time);
                                Debug.Log("Position: " + position_x + ", " + position_y + ", " + position_z);
                                Debug.Log("Rotation: " + rotation_r + ", " + rotation_x + ", " + rotation_y + ", " + rotation_z);
                                // Process the received data as needed.
                            }
                            else
                            {
                                Debug.LogError("Invalid data format received.");
                            }
                        }
                        else
                        {
                            Debug.LogError("Invalid data format received.");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Error receiving data: {e.Message}");
                DisconnectFromServer();
                break;
            }
        }
    }
}