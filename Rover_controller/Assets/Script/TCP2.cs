using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Collections;
using UnityEngine;
using System.IO;

public class TCP2 : MonoBehaviour
{
    private const string HOST = "127.0.0.1";
    private const int PORT = 55555;
    private const int BUFFER_SIZE = 1024;

    private bool end = false;

    private void Start()
    {
        Thread serverThread = new Thread(StartServer);
        serverThread.Start();

        // In Unity, you generally don't want to use an empty while loop like this.
        // Consider implementing the necessary logic in Update() method instead.
        while (!end)
        {
            // Implement your custom logic here or use the Update() method.
        }
        serverThread.Join();
    }

    private void StartServer()
    {
        TcpListener serverSocket = null;
        List<TcpClient> clientSockets = new List<TcpClient>();

        try
        {
            IPAddress ipAddress = IPAddress.Parse(HOST);
            serverSocket = new TcpListener(ipAddress, PORT);
            serverSocket.Start();
            serverSocket.Server.ReceiveTimeout = 10000; // 10 seconds timeout

            Debug.Log($"Server listening on {HOST}:{PORT}");

            while (!end)
            {
                try
                {
                    TcpClient clientSocket = serverSocket.AcceptTcpClient();
                    clientSocket.ReceiveTimeout = 10000; // 10 seconds timeout
                    clientSockets.Add(clientSocket);

                    Debug.Log($"New connection from {clientSocket.Client.RemoteEndPoint}");
                }
                catch (SocketException)
                {
                    // Handle exceptions or other logic as needed.
                }

                List<TcpClient> tempSockets = new List<TcpClient>(clientSockets);

                foreach (TcpClient clientSocket in tempSockets)
                {
                    try
                    {
                        NetworkStream networkStream = clientSocket.GetStream();
                        byte[] buffer = new byte[BUFFER_SIZE];
                        int bytesRead = networkStream.Read(buffer, 0, buffer.Length);

                        if (bytesRead > 0)
                        {
                            float[] receivedData = new float[7]; // Assuming 7 floats in the received data
                            Buffer.BlockCopy(buffer, 0, receivedData, 0, bytesRead);

                            Debug.Log($"Received data from {clientSocket.Client.RemoteEndPoint}: {string.Join(", ", receivedData)}");

                            string response = "done deal in dundee";
                            byte[] responseBytes = Encoding.UTF8.GetBytes(response);
                            networkStream.Write(responseBytes, 0, responseBytes.Length);
                        }
                        else
                        {
                            Debug.Log($"Client {clientSocket.Client.RemoteEndPoint} disconnected.");
                            clientSocket.Close();
                            clientSockets.Remove(clientSocket);
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.Log($"Error receiving data from {clientSocket.Client.RemoteEndPoint}: {ex}");
                        clientSocket.Close();
                        clientSockets.Remove(clientSocket);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Debug.Log($"Exception in StartServer: {ex}");
        }
        finally
        {
            if (serverSocket != null)
            {
                serverSocket.Stop();
            }

            foreach (TcpClient clientSocket in clientSockets)
            {
                clientSocket.Close();
            }
            clientSockets.Clear();
        }
    }
}
