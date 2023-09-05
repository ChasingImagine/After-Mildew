using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersMeneger : MonoBehaviour
{
    public GameObject prefab; // Prefab nesnesi
    private Dictionary<string, GameObject> objectDictionary = new Dictionary<string, GameObject>(); // Objeleri ve ID'lerini saklayacak s�zl�k

    // Objeleri olu�turmak i�in bir fonksiyon
    public void CreateObject(Player player)
    {
        if (objectDictionary.ContainsKey(player.id))
        {
        
            ModifyObject(player);
        
        }
        else
        {
            // Prefab'tan yeni bir klon olu�tur
            GameObject newObject = Instantiate(prefab);

            // Klonun transform de�erlerini ayarlayabilirsiniz (�rne�in, konum)
            newObject.transform.position = new Vector3(player.transforms.pozitions.x, player.transforms.pozitions.y, player.transforms.pozitions.z);
            newObject.transform.rotation = Quaternion.Euler(player.transforms.rotations.x, player.transforms.rotations.y, player.transforms.rotations.z);
            // Klonun ID'sini atay�n ve s�zl��e ekleyin
            objectDictionary.Add(player.id, newObject);
        }
      

        
    }

    // Belirli bir ID ile objenin transform de�erlerini de�i�tirmek i�in bir fonksiyon
    public void ModifyObject(Player player)
    {
        if (objectDictionary.ContainsKey(player.id))
        {
            // ID'ye sahip objenin transform de�erlerini de�i�tir
            GameObject objToModify = objectDictionary[player.id];
            objToModify.transform.position = new Vector3(player.transforms.pozitions.x,player.transforms.pozitions.y,player.transforms.pozitions.z);
            objToModify.transform.rotation =  Quaternion.Euler(player.transforms.rotations.x,player.transforms.rotations.y,player.transforms.rotations.z);
        
        }
        else
        {
            Debug.Log("ID bulunamad�.");
        }
    }

    public void DeletePlayer(string id)
    {
        Destroy(objectDictionary[id]);
        objectDictionary.Remove(id);
    }
}
