

using UnityEngine;
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Collections.Specialized;
using Unity.VisualScripting;
//using UnityEditor.Experimental.RestService;

public class MessageReceiver : MonoBehaviour
{




    [SerializeField] int playertype = 2;
    private bool quit = false;
    private TcpClient client;
    private byte[] buffer = new byte[4096];

    public string serverAddress = "localhost";
    public int serverPort = 12345;
    public PlayersMeneger playersMeneger;


    public static string id = "";
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



                   Dictionary<string,Player> difference = new Dictionary<string,Player>();


                    foreach (var kvp in playersOld)
                    {
                        if (!players.ContainsKey(kvp.Key))
                        {
                            difference.Add(kvp.Key, kvp.Value);
                        }
                    }

                    Console.WriteLine("İlk sözlükte olup ikinci sözlükte olmayanlar:");
                    foreach (var kvp in difference)
                    {
                        
                        Console.WriteLine($"{kvp.Key}: {kvp.Value.id}");
                        playersMeneger.DeletePlayer(kvp.Value.id);
                    }

                    playersOld = players;



                    foreach (string key in players.Keys)
                    {
                        
                        if(players[key] != null && players[key].id != id && players[key].id !=""  )
                        {
                           
                           playersMeneger.CreateObject ( players[key]);
                        }
                       
                    }
                }


            }
            

            await Task.Delay(16); // Wait for 16 micro second before sending data again
        }

        Debug.Log("Server connection closed.");
        client.Close();
    }

    private async Task SendDataToServerAsync()
    {

        float p_x = float.Parse(transform.position.x.ToString("F3"));
        float p_y = float.Parse(transform.position.y.ToString("F3"));
        float p_z = float.Parse(transform.position.z.ToString("F3"));
        /*
        float r_x = float.Parse(transform.localRotation.x.ToString("F3"));
        float r_y = float.Parse(transform.localRotation.y.ToString("F3"));
        float r_z = float.Parse(transform.localRotation.z .ToString("F3"));
        */
       
       
        Transforms PlayerTransform = new Transforms
        {
       

        pozitions = new Pozitions { x = p_x, y = p_y, z =  p_z},
        rotations = new Rotations { x = this.transform.rotation.eulerAngles.x, y = this.transform.rotation.eulerAngles.y, z = this.transform.rotation.eulerAngles.z}
        };

        Player PlayerToSend = new Player
        {
            id = id,
            transforms = PlayerTransform,
            playerTypes = playertype,
            


        };
        if (PlayerToSend.playerTypes == 0)
        {
            return;
        }
        
        
    

        Dictionary<string,Player> dataToSend = new Dictionary<string,Player>();
        dataToSend.Add("Palyer", PlayerToSend);

        string jsonData = JsonConvert.SerializeObject(dataToSend);
        byte[] dataBytes = Encoding.UTF8.GetBytes(jsonData);

        Dictionary<string, Player> p = new Dictionary<string, Player>();
          p =  JsonConvert.DeserializeObject<Dictionary<string,Player>>(jsonData);
        if (p["Palyer"].playerTypes == 0)
        {
            Debug.Log("oyuncu türü hatası");
        }

        await client.GetStream().WriteAsync(dataBytes, 0, dataBytes.Length);


    }
}



