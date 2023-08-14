using UnityEngine;
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;




public class MessageReceiver : MonoBehaviour
{

    public bool patates = false;


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
        await CommunicationLoopAsync();
    }

    private async Task ConnectToServerAsync()
    {
        client = new TcpClient();
        await client.ConnectAsync(serverAddress, serverPort);
    }

    private async Task CommunicationLoopAsync()
    {
        while (client != null && client.Connected)
        {
            // Mesaj gönderme
            Message sendMessage = new Message
            {
                text = "Unity'den mesaj",
                sayi = 42
            };

            string sendJson = JsonUtility.ToJson(sendMessage);
            byte[] sendData = Encoding.ASCII.GetBytes(sendJson);

            await client.GetStream().WriteAsync(sendData, 0, sendData.Length);

            // Mesaj alma
            int bytesRead = await client.GetStream().ReadAsync(buffer, 0, buffer.Length);
            if (bytesRead == 0)
            {
                break;
            }

            string receivedJson = Encoding.ASCII.GetString(buffer, 0, bytesRead);

            Message receivedMessage = JsonUtility.FromJson<Message>(receivedJson);

            if (!string.IsNullOrEmpty(receivedMessage.text))
            {
                Debug.Log("Sunucudan gelen mesaj: " + receivedMessage.text + " " + receivedMessage.sayi);
            }

            await Task.Delay(1000); // 1 saniye bekle
            if (patates)
            {
                break;
            }
        }

        Debug.Log("Sunucu bağlantısı kapatıldı.");
        client.Close();
    }





}
