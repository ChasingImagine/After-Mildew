using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersAnim : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private bool isGrounded = false;
    private bool isJump;

    private void OnTriggerEnter(Collider other)
    {
        // Çarpýþan nesne bir zemine temaseti ise
        if (other.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Çarpýþan nesne zeminden ayrýldýysa
        if (other.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }


    public bool IsGrounded()
    {
        return isGrounded;
    }


}
