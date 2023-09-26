using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class selectPlayers : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }




    public void SelectPlayer1()
    {

        PlayerPrefs.SetInt("Player", 1);
        Debug.Log(1);
        SceneManager.LoadScene("SampleScene");
    }
    public void SelectPlayer2()
    {
        PlayerPrefs.SetInt("Player", 2);
        Debug.Log(2);
        SceneManager.LoadScene("SampleScene");
    }


}
