using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelectImporting : MonoBehaviour
{
    [SerializeField] GameObject male;
    [SerializeField] GameObject female;


    private void Awake()
    {
       int playerNuber = PlayerPrefs.GetInt("Player", 1);


        if (playerNuber == 1)
        {
            male.SetActive(true);
            male.GetComponent<MessageReceiver>().enabled = true;
            female.GetComponent<MessageReceiver>().enabled = false;
            female.SetActive(false);
            
        }
        else
        {
            female.SetActive(true);
            female.GetComponent <MessageReceiver>().enabled = true;
            male.GetComponent<MessageReceiver>().enabled = false;
            male.SetActive(false);

        }


    }

   
}
