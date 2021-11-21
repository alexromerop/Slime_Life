using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Character_movment_scr : MonoBehaviour
{
   
    [SerializeField] private GameObject cam;
    [SerializeField] private GameObject prefab;
    [SerializeField] public int num_clone =0 ;



    private Rigidbody rb;


    private float movementX;
    private float movementY;

    [SerializeField] private float speed = 0f;
    [SerializeField] private float jumpForce = 0f;

    public bool isGrounded;
   



 
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    
    void Update()
    {
        Vector3 cameraForward = cam.transform.forward;
        cameraForward.y = 0;

    }




    private void FixedUpdate()
    {
       // rb.AddForce(new Vector3(movementX * speed, 0.0f, movementY * speed));

        //movimiento laterla con la camera
        rb.AddTorque(new Vector3(cam.transform.forward.x * movementX*-speed, 0 , cam.transform.forward.z * movementX * -speed));
        rb.AddTorque(new Vector3(cam.transform.forward.z*movementY * speed, 0, -cam.transform.forward.x * movementY * speed));
        


    }





    //Inputs
    void OnJump(InputValue movementValue)
    {
        if (isGrounded)
        {
            rb.AddForce(new Vector3(0, jumpForce, 0));
            isGrounded = false;
        }
    }
    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void OnInputAction1 (InputValue movementValue)
    {
        Debug.Log("a");

        Destroy(this.gameObject);   
        var go =Instantiate(prefab, transform.position,Quaternion.identity).gameObject.transform.localScale = new Vector3 (0.5f, 0.5f, 0.5f);
        Instantiate(prefab, transform.position, Quaternion.identity).gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

        

    }



    //Collision
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "OnGround")
        {
            
        }
    }

    


}
