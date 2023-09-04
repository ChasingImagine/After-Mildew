

using UnityEngine;
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using Newtonsoft.Json;
//using UnityEditor.Experimental.RestService;

public class MessageReceiver : MonoBehaviour
{

    


    private bool quit = false;
    private TcpClient client;
    private byte[] buffer = new byte[4096];

    public string serverAddress = "localhost";
    public int serverPort = 12345;
    public PlayersMeneger playersMeneger;


    string id = "";
    private Dictionary<string,Player> playersOld = new Dictionary<string,Player>();

    private void OnApplicationQuit()
    {
        quit = true;
    }



   // private float deltaTime = 0.0f;
    private void Update()
    {
        /*
        // Her kare güncellendiğinde geçen süreyi hesapla
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;

        // FPS değerini hesapla (1 saniyedeki kare sayısı)
        float fps = 1.0f / deltaTime;

        // FPS değerini konsola yazdır
        Debug.Log("FPS: " + Mathf.Round(fps));
        */
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

            Dictionary <string, string> receivedMessage = null;
            try
            {
                receivedMessage = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(receivedJson);
            }
            catch (System.Exception ex)
            {
                Debug.Log("Veri alırken hata oluştu: " + ex.Message);
                Debug.Log("Hata İzleme: " + ex.StackTrace);
                receivedMessage = null;
                continue;
            }
           
            if (receivedMessage != null)
            {
                if (receivedMessage["Player"] != null)
                {
                    
                    Player playerdata = Newtonsoft.Json.JsonConvert.DeserializeObject<Player>(Convert.ToString( receivedMessage["Player"]));
                 //   Debug.Log( "id : " + playerdata.id  + "  Received Position - x: " + playerdata.transforms.pozitions.x + " y: " + playerdata.transforms.pozitions.y + " z: " + playerdata.transforms.pozitions.z);
                    id = playerdata.id;


                }


                if (receivedMessage["Players"] != null)
                {
                    Dictionary<string,Player> players = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string,Player>>(receivedMessage["Players"]);

                    
                    // 1. sözlükte olup 2. sözlükte olmayan elemanları bulma
                    Dictionary<string, Player> differenceDict1 = new Dictionary<string, Player>();
                    foreach (var pair in playersOld)
                    {
                        if (!players.ContainsKey(pair.Key))
                        {
                            differenceDict1.Add(pair.Key, pair.Value);
                        }
                    }

                    foreach (var pair in differenceDict1)
                    {
                        playersMeneger.DeletePlayer(pair.Value);
                    }


                    foreach (string key in players.Keys)
                    {
                        
                        if(players[key] != null && players[key].id != id && players[key].id !=""  )
                        {
                           
                           playersMeneger.CreateObject ( players[key]);
                        }
                        else
                        {
                            Debug.Log("8888888888888888888888888888888");
                            if (players[key] != null)
                            {
                                Debug.Log("vay anasını beee");
                            }
                        }
                    }
                }


            }
            

            await Task.Delay(1000); // Wait for 1 second before sending data again
        }

        Debug.Log("Server connection closed.");
        client.Close();
    }

    private async Task SendDataToServerAsync()
    {
        
        Transforms PlayerTransform = new Transforms
        {
            pozitions = new Pozitions { x = transform.position.x, y = transform.position.y, z = transform.position.z },
            rotations = new Rotations { x = transform.localRotation.x, y = transform.localRotation.y, z = transform.localRotation.z }
        };

        Player PlayerToSend = new Player
        {
            id = id,
            transforms = PlayerTransform
            
        };

        Dictionary<string,Player> dataToSend = new Dictionary<string,Player>();
        dataToSend.Add("Palyer", PlayerToSend);

        string jsonData = JsonConvert.SerializeObject(dataToSend);
        byte[] dataBytes = Encoding.UTF8.GetBytes(jsonData);
        
        await client.GetStream().WriteAsync(dataBytes, 0, dataBytes.Length);
    }
}



