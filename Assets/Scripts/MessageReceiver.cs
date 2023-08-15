

using UnityEngine;
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

public class MessageReceiver : MonoBehaviour
{
    private bool quit = false;
    private TcpClient client;
    private byte[] buffer = new byte[4096];

    public string serverAddress = "localhost";
    public int serverPort = 12345;

    private async void Start()
    {
        await ConnectToServerAsync();
        await CommunicationLoopAsync();
    }

    private async Task ConnectToServerAsync()
    {
        client = new TcpClient();
        await client.ConnectAsync(serverAddress, serverPort);
    }

    private async Task CommunicationLoopAsync()
    {
        while (!quit)
        {
            // Send data to server
            await SendDataToServerAsync();

            // Receive data from server
            int bytesRead = await client.GetStream().ReadAsync(buffer, 0, buffer.Length);
            if (bytesRead == 0)
            {
                break;
            }

            string receivedJson = Encoding.UTF8.GetString(buffer, 0, bytesRead);

            Transforms receivedMessage = JsonUtility.FromJson<Transforms>(receivedJson);

            if (receivedMessage.pozitions != null)
            {
                Debug.Log("Received Position - x: " + receivedMessage.pozitions.x + " y: " + receivedMessage.pozitions.y + " z: " + receivedMessage.pozitions.z);
            }

            await Task.Delay(1000); // Wait for 1 second before sending data again
        }

        Debug.Log("Server connection closed.");
        client.Close();
    }

    private async Task SendDataToServerAsync()
    {
        Transforms dataToSend = new Transforms
        {
            pozitions = new Pozitions { x = transform.position.x, y = transform.position.y, z = transform.position.z },
            rotations = new Rotations { x = transform.rotation.x, y = transform.rotation.y, z = transform.rotation.z }
        };

        string jsonData = JsonUtility.ToJson(dataToSend);
        byte[] dataBytes = Encoding.UTF8.GetBytes(jsonData);

        await client.GetStream().WriteAsync(dataBytes, 0, dataBytes.Length);
    }
}
