using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelManeger : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject connectionError;
    [SerializeField] private InputField ip;
    [SerializeField] private InputField port;

    public void ConnectionSetings()
    {
        panel.SetActive(true);
        try
        {
            PlayerPrefs.SetString("ip", ip.text);
            PlayerPrefs.SetInt("port", Convert.ToInt32(port.text));
        }catch(System.Exception e)
        {
            Debug.Log(e.ToString());
            PlayerPrefs.SetString("ip", "0");
            PlayerPrefs.SetInt("port", 0);
            ip.text = null;
            port.text = null;
            connectionError.SetActive(true);
        }
    }



}
