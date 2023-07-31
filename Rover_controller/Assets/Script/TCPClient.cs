using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

public class TCPClient : MonoBehaviour
{
    public string serverIP = "192.168.50.223"; // IP address of the TCP server
    public int serverPort = 55555; // Port number of the TCP server

    private TcpClient client;
    private NetworkStream stream;
    private byte[] receiveBuffer = new byte[1024];
    private bool isReceiving = false;

    private async void Start()
    {
        await ConnectToServer();
    }

    private async Task ConnectToServer()
    {
        try
        {
            client = new TcpClient();
            await client.ConnectAsync(serverIP, serverPort);
            stream = client.GetStream();

            // Start listening to messages from the server.
            isReceiving = true;
            ReceiveMessages();
        }
        catch (Exception e)
        {
            Debug.LogError($"Error connecting to the server: {e.Message}");
        }
    }

    private async void ReceiveMessages()
    {
        while (isReceiving)
        {
            try
            {
                int bytesRead = await stream.ReadAsync(receiveBuffer, 0, receiveBuffer.Length);
                if (bytesRead > 0)
                {
                    string message = Encoding.ASCII.GetString(receiveBuffer, 0, bytesRead);
                    Debug.Log(message);
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Error receiving message: {e.Message}");
                break;
            }
        }

        // Close the client and stream when the thread exits.
        if (stream != null)
        {
            stream.Close();
        }

        if (client != null)
        {
            client.Close();
        }
    }

    private void OnApplicationQuit()
    {
        // disconnect from the server and stop the receiving thread.
        isReceiving = false;
    }
}