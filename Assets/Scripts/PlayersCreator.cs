using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersCreator : MonoBehaviour
{
    public GameObject prefabToSpawn; // Diger oyuncularý temsil eden  Prefab
    
    void PlayersCreate()
    {
        Vector3 spawnPosition = new Vector3(0f, 0f, 0f); //  konum
        Quaternion spawnRotation = Quaternion.identity; //  rotasyon
        GameObject spawnedPrefab = Instantiate(prefabToSpawn, spawnPosition, spawnRotation);
    }


    void Update()
    {
        
    }



}
