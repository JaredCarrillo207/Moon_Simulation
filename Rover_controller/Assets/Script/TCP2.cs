
using System;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

public class TCP2 : MonoBehaviour
{
    public string host = "127.0.0.1";
    public int port = 55555;
    public int bufferSize = 1024;

    private TcpClient client;
    private NetworkStream stream;
    private byte[] receiveBuffer;

    private void Start()
    {
        try
        {
            client = new TcpClient(host, port);
            stream = client.GetStream();
            receiveBuffer = new byte[bufferSize];
            stream.BeginRead(receiveBuffer, 0, bufferSize, ReceiveCallback, null);
        }
        catch (Exception e)
        {
            Debug.LogError($"Error connecting to the server: {e}");
        }
    }

    private void ReceiveCallback(IAsyncResult ar)
    {
        try
        {
            int bytesRead = stream.EndRead(ar);
            if (bytesRead > 0)
            {
                byte[] data = new byte[bytesRead];
                Array.Copy(receiveBuffer, data, bytesRead);
                string receivedMessage = System.Text.Encoding.UTF8.GetString(data);
                Debug.Log($"Received data from server: {receivedMessage}");

                // Call a method to handle the received data
                HandleReceivedData(receivedMessage);

                stream.BeginRead(receiveBuffer, 0, bufferSize, ReceiveCallback, null);
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Error receiving data from the server: {e}");
            CloseConnection();
        }
    }

    private void HandleReceivedData(string receivedData)
    {


        // Example parsing (assuming the format is "elapsed_time,x,y,z,rot0,rot1,rot2,rot3"):
        string[] values = receivedData.Split(',');
        if (values.Length == 8)
        {
            float elapsedTime = float.Parse(values[0]);
            float x = float.Parse(values[1]);
            float y = float.Parse(values[2]);
            float z = float.Parse(values[3]);
            float rot0 = float.Parse(values[4]);
            float rot1 = float.Parse(values[5]);
            float rot2 = float.Parse(values[6]);
            float rot3 = float.Parse(values[7]);

            // Use the parsed values as needed.
            // For example, you can log them to the Unity debug console:
            Debug.Log($"Elapsed Time: {elapsedTime}, Position: ({x}, {y}, {z}), Rotation: ({rot0}, {rot1}, {rot2}, {rot3})");
        }
        else
        {
            Debug.LogError("Received data is not in the expected format.");
        }
    }

    private void CloseConnection()
    {
        if (stream != null)
        {
            stream.Close();
            stream = null;
        }

        if (client != null)
        {
            client.Close();
            client = null;
        }
    }

    private void OnApplicationQuit()
    {
        CloseConnection();
    }
}
