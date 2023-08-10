
using UnityEngine;
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

public class MessageReceiver : MonoBehaviour
{
    public string serverAddress = "localhost";
    public int serverPort = 12345;

    private TcpClient client;
    private byte[] buffer = new byte[1024];
    private StringBuilder receivedMessage = new StringBuilder();

    [Serializable]
    public class Message
    {
        public string text;
        public int sayi;
    }

    private async void Start()
    {
        await ConnectToServerAsync();
        await ReceiveMessagesAsync();
    }

    private async Task ConnectToServerAsync()
    {
        client = new TcpClient();
        await client.ConnectAsync(serverAddress, serverPort);
    }

    private async Task ReceiveMessagesAsync()
    {
        while (client != null && client.Connected)
        {
            int bytesRead = await client.GetStream().ReadAsync(buffer, 0, buffer.Length);
            if (bytesRead == 0)
            {

                break;
            }

            string jsonData = Encoding.ASCII.GetString(buffer, 0, bytesRead);

            Message message = JsonUtility.FromJson<Message>(jsonData);

            if (!string.IsNullOrEmpty(message.text))
            {
                Debug.Log("Sunucudan gelen mesaj: " + message.text + " " + message.sayi);

                // Sunucuya cevap g�nderme
                Message responseMessage = new Message
                {
                    text = "Unity'den gelen cevap",
                    sayi = 42
                };

                string responseJson = JsonUtility.ToJson(responseMessage);
                byte[] responseData = Encoding.ASCII.GetBytes(responseJson);

                await client.GetStream().WriteAsync(responseData, 0, responseData.Length);
            }
        }

        //Debug.Log("Sunucu ba�lant�s� kapand�.");
        //client.Close();
    }
}