using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Character_movment_scr : MonoBehaviour
{

    [SerializeField] private GameObject cam;
    [SerializeField] private GameObject prefab;
    [SerializeField] public int num_clone = 0;
    public GameObject Cam_cinemachine;
    public GameObject Cam_AIM_cinemachine;

    public GameObject score;



    private Rigidbody rb;


    private float movementX;
    private float movementY;

    [SerializeField] private float speed = 0f;
    [SerializeField] private float jumpForce = 0f;

    public bool isGrounded;





    void Start()
    {
        rb = GetComponent<Rigidbody>();

      
            if (score == null)
            {
                score = GameObject.Find("Canvas");
            }

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
        rb.AddTorque(new Vector3(cam.transform.forward.x * movementX * -speed, 0, cam.transform.forward.z * movementX * -speed));
        rb.AddTorque(new Vector3(cam.transform.forward.z * movementY * speed, 0, -cam.transform.forward.x * movementY * speed));



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

    void OnInputActionN(InputValue movementValue)
    {
        Debug.Log("a");

        if (gameObject.transform.localScale.x >= 0.6f)
        {


            GameObject Clone1 = Instantiate(prefab, transform.position, Quaternion.identity);
            Clone1.transform.localScale = gameObject.transform.localScale / 2;

            //gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            gameObject.transform.localScale = gameObject.transform.localScale / 2;

        }
        else
        {
            Debug.Log("chiquito");

            //error sound
        }







        // GameObject Clone2 = Instantiate(prefab, transform.position, Quaternion.identity);
        //Clone2.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);


        // Clone2.GetComponent<Character_movment_scr>().enabled = false;
        //Clone2.GetComponent<Character>().enabled = false;


        //Instantiate(prefab, transform.position, Quaternion.identity).gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);


    }

    

    public void OnAiming()
    {

        Debug.Log("aaa");

        if (Cam_cinemachine.activeInHierarchy == true)
        {
            Cam_cinemachine.SetActive(false);
            Cam_AIM_cinemachine.SetActive(true);

        }
        else
        {
            Cam_cinemachine.SetActive(true);
            Cam_AIM_cinemachine.SetActive(false);

        }

    }


    public void OnInputActionW()
    {
        Debug.Log("W");
        this.gameObject.GetComponent<Character>().get_ston();



    }
    public void OnInputActionE()
    {

        this.gameObject.GetComponent<Character>().shoot(cam);

        if (score.gameObject.GetComponent<Score>().Win.active)
        {
            SceneManager.LoadScene(0);
            Cursor.lockState = CursorLockMode.None;
        }

        if (score.gameObject.GetComponent<Score>().Lose.active)
        {
            SceneManager.LoadScene(1);
            Cursor.lockState = CursorLockMode.None;
        }


    }

    //Collisiond
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.GetComponent<Rigidbody>().AddForce((cam.transform.forward*3),ForceMode.Impulse);



        }


    }
}
