using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]

public class PlayerControler : MonoBehaviour
{

   
    public float move_Speed;
    public float rotationSpeed;
    public Rigidbody Rb;



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
        
    }

    private void FixedUpdate()
    {
        Rb.AddRelativeForce(new Vector3(0f,  0f, vertical) *move_Speed ,ForceMode.Impulse);
        Vector3 rotation = new Vector3(0.0f, horizontal * rotationSpeed, 0.0f);
        Rb.rotation *= Quaternion.Euler(rotation);
        
    }



}

