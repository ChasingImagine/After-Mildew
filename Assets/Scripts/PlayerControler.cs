using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]

public class PlayerControler : MonoBehaviour
{
    private bool isGrounded = false;
    private bool isJump;
    public float move_Speed;
    public float rotationSpeed;
    public Rigidbody Rb;
    [SerializeField]  PlayerAnimationManeger playerAnimationManeger;
    [SerializeField] private float jumpHeight;


    float horizontal;
    float vertical;
   

    // Start is called before the first frame update
    void Start()
    {




        
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        if (isGrounded && Input.GetKey(KeyCode.Space) && !isJump)
        {
            playerAnimationManeger.Jumping(true);
            transform.position += new Vector3(0f,0.1f, 0f);
            Rb.AddForce(new Vector3(0f, jumpHeight , 0f), ForceMode.Impulse);
            isJump = true;
        }
        else if(isJump && isGrounded)
        {
            playerAnimationManeger.Jumping(false);
            isJump=false;
        }
        
    }

    private void FixedUpdate()
    {
        Rb.AddRelativeForce(new Vector3(0f,  0f, vertical * move_Speed)  ,ForceMode.VelocityChange);
        Vector3 rotation = new Vector3(0.0f, horizontal * rotationSpeed, 0.0f);
        Rb.rotation *= Quaternion.Euler(rotation*rotationSpeed);

        

    }




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

