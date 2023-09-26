using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersMeneger : MonoBehaviour
{
   
    public GameObject prefab; // Prefab nesnesi
    public GameObject prefab2; // Prefab nesnesi
    private Dictionary<string, GameObject> objectDictionary = new Dictionary<string, GameObject>(); // Objeleri ve ID'lerini saklayacak sözlük

    // Objeleri oluþturmak için bir fonksiyon
    public void CreateObject(Player player)
    {
        if (player.playerTypes != 0)
        {
            if (objectDictionary.ContainsKey(player.id))
            {
                try
                {
                    ModifyObject(player);
                }
                catch
                {

                }
            }
            else
            {
              

                GameObject newObject;
                // Prefab'tan yeni bir klon oluþtur
                if (player.playerTypes == 1)
                {
                    newObject = Instantiate(prefab);
                }
                else
                {
                    newObject = Instantiate(prefab2);
                }


                // Klonun transform 
                newObject.transform.position = new Vector3(player.transforms.pozitions.x, player.transforms.pozitions.y, player.transforms.pozitions.z);
                newObject.transform.rotation = Quaternion.Euler(player.transforms.rotations.x, player.transforms.rotations.y, player.transforms.rotations.z);
                // Klonun ID'sini atayýn ve sözlüðe ekleyin
                objectDictionary.Add(player.id, newObject);
            }


        }


    }
    // Transform resultTransform;
    // Belirli bir ID ile objenin transform deðerlerini deðiþtirmek için bir fonksiyon
    public void ModifyObject(Player player)
    {
        if (objectDictionary.ContainsKey(player.id) )
        {
            // ID'ye sahip objenin transform deðerlerini deðiþtir
            GameObject objToModify = objectDictionary[player.id];
            Transform resultTransform = objToModify.transform;
            if (objToModify != null && player.transforms  != null && player.transforms.pozitions != null)
            {

                resultTransform.position = new Vector3(player.transforms.pozitions.x, player.transforms.pozitions.y, player.transforms.pozitions.z);
                resultTransform.rotation = Quaternion.Euler(player.transforms.rotations.x, player.transforms.rotations.y, player.transforms.rotations.z);

                bool resultBool = false;

                if (resultTransform.position.y - objToModify.transform.position.y == 0)
                {
                    resultBool = false;
                }
                else
                {
                    resultBool = true;
                }

                Animator animator = objToModify.GetComponent<Animator>();
                animator.SetBool("Jump", resultBool);
                animator.SetFloat("Horizontal", resultTransform.position.x - objToModify.transform.position.x);
                animator.SetFloat("Vertical", resultTransform.position.z - objToModify.transform.position.z);


                objToModify.transform.position = resultTransform.position;
                objToModify.transform.rotation = resultTransform.rotation;
            }
            
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
