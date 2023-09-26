using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationManeger : MonoBehaviour
{
    Animator animator;
    Rigidbody rb;

  
   


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 vector =  transform.InverseTransformDirection(rb.velocity).normalized;
        animator.SetFloat("Horizontal", vector.x);
        animator.SetFloat("Vertical", vector.z);
        
        if (vector.y >0.2 || vector.y < -0.2  )
        {
            animator.SetBool("Jump", true);
        }
        else
        {
            animator.SetBool("Jump", false);
        }

       

    }


    public void Jumping(bool jump)
    {
        animator.SetBool("Jump", jump);
    }



}
