using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]

public class PlayerControler : MonoBehaviour
{

   
    public float move_Speed;
    public Rigidbody Rb;



    float horizontal;
    float vertical;

    // Start is called before the first frame update
    void Start()
    {

            this.GetComponent<Rigidbody>().drag = 0.5f;



        
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        
    }

    private void FixedUpdate()
    {
        Rb.AddForce(new Vector3(horizontal,  0, vertical) *move_Speed ,ForceMode.Force);
    }


}

