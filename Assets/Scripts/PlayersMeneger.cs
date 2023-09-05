using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersMeneger : MonoBehaviour
{
    public GameObject prefab; // Prefab nesnesi
    private Dictionary<string, GameObject> objectDictionary = new Dictionary<string, GameObject>(); // Objeleri ve ID'lerini saklayacak sözlük

    // Objeleri oluþturmak için bir fonksiyon
    public void CreateObject(Player player)
    {
        if (objectDictionary.ContainsKey(player.id))
        {
        
            ModifyObject(player);
        
        }
        else
        {
            // Prefab'tan yeni bir klon oluþtur
            GameObject newObject = Instantiate(prefab);

            // Klonun transform deðerlerini ayarlayabilirsiniz (örneðin, konum)
            newObject.transform.position = new Vector3(player.transforms.pozitions.x, player.transforms.pozitions.y, player.transforms.pozitions.z);
            newObject.transform.rotation = Quaternion.Euler(player.transforms.rotations.x, player.transforms.rotations.y, player.transforms.rotations.z);
            // Klonun ID'sini atayýn ve sözlüðe ekleyin
            objectDictionary.Add(player.id, newObject);
        }
      

        
    }

    // Belirli bir ID ile objenin transform deðerlerini deðiþtirmek için bir fonksiyon
    public void ModifyObject(Player player)
    {
        if (objectDictionary.ContainsKey(player.id))
        {
            // ID'ye sahip objenin transform deðerlerini deðiþtir
            GameObject objToModify = objectDictionary[player.id];
            objToModify.transform.position = new Vector3(player.transforms.pozitions.x,player.transforms.pozitions.y,player.transforms.pozitions.z);
            objToModify.transform.rotation =  Quaternion.Euler(player.transforms.rotations.x,player.transforms.rotations.y,player.transforms.rotations.z);
        
        }
        else
        {
            Debug.Log("ID bulunamadý.");
        }
    }

    public void DeletePlayer(string id)
    {
        Destroy(objectDictionary[id]);
        objectDictionary.Remove(id);
    }
}
