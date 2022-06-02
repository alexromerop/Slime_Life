using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Cinemachine;


public class Character_movment_scr : MonoBehaviour
{
    [SerializeField] private GameObject tarjet;

    [SerializeField] private GameObject cam;
    [SerializeField] private GameObject prefab;
    [SerializeField] public int num_clone = 0;
    public GameObject Cam_cinemachine;
    public GameObject Cam_AIM_cinemachine;

    public GameObject score;

    public AudioClip[] Footsteps;

    public AudioSource audioSource;

    private Rigidbody rb;
    [SerializeField] float speed_jump;

    private float movementX;
    private float movementY;

    [SerializeField] private float speed = 0f;
    [SerializeField] private float jumpForce = 0f;

    public bool isGrounded;

    private void Awake()
    {
        cam = GameObject.Find("Main Camera");
        Cam_cinemachine = FindObjectOfType<CinemachineFreeLook>().gameObject;
        audioSource = gameObject.GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        tarjet = FindObjectOfType<Target_cam>().gameObject;

    }

    void Start()
    {
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
        //movimiento laterla con la camera
        rb.AddTorque(new Vector3(cam.transform.forward.x * movementX * -speed, 0, cam.transform.forward.z * movementX * -speed));
        rb.AddTorque(new Vector3(cam.transform.forward.z * movementY * speed, 0, -cam.transform.forward.x * movementY * speed));

    }

    //Inputs
    #region Inputs
    public void OnJump(InputValue movementValue)
    {
        if (isGrounded)
        {
            rb.AddForce(new Vector3(0, jumpForce, 0));
            isGrounded = false;
        }
    }
    public void OnMove(InputValue movementValue)
    {
       
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
        

    }
    public void OnChangePlayer(InputValue movementValue)
    {
        int value= (int) movementValue.Get<float>();
        Player_Manager.instance.ChangePlayer(gameObject.GetComponent<Character>(), value );
        
    }
    public void OnUnirse(InputValue movementValue)
    {
        
        if (gameObject.GetComponent<Character>().slimeNear != null)
            this.gameObject.GetComponent<Character>().Polifusion(gameObject.GetComponent<Character>().slimeNear);
    }
    public void OnDividirse(InputValue movementValue)
    {
        Dividirse();
    }
    public void OnInputActionW(InputValue movementValue)
    {
    }
    public void OnInputActionE(InputValue movementValue)
    {
        //this.gameObject.GetComponent<Character>().get_ston();
        this.gameObject.GetComponent<Character>().get_water();
    }
    public void OnInputActionN(InputValue movementValue)
    {

    }
    #endregion
    #region collisons
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.GetComponent<Rigidbody>().AddForce((cam.transform.forward*3),ForceMode.Impulse);
        }

        if (collision.gameObject.CompareTag("OnGround"))
        {
            footstepaudioPlayer();
        }


    }
    #endregion



    public void Dividirse()
    {
        //Segun la escal partes la escala en 2 i instancias otroa player de ese tamaño
        if (gameObject.transform.localScale.x >= 0.6f)
        {
            GameObject Clone1 = Instantiate(prefab, transform.position, Quaternion.identity);
            Clone1.transform.localScale = gameObject.transform.localScale / 2;
            Clone1.GetComponent<Character_movment_scr>().enabled = false;
            Clone1.GetComponent<Character>().enabled = false;
            Clone1.GetComponent<PlayerInput>().enabled = false;
            gameObject.transform.localScale = gameObject.transform.localScale / 2;
            Player_Manager.instance.GetPlayers();

        }
        else
        {
            Debug.Log("chiquito");

            //error sound
        }
    }
    public void footstepaudioPlayer()
    {
        //sonido hehcos por raycast
        if (audioSource != null)
        if (!audioSource.isPlaying && (Mathf.Abs(gameObject.GetComponent<Rigidbody>().velocity.z) >= 1 || Mathf.Abs(gameObject.GetComponent<Rigidbody>().velocity.x) >= 1) || gameObject.GetComponent<Rigidbody>().velocity.y >= 1)
        {
            //Segun la altura que caigas sonara mas fuerte
            speed_jump = Mathf.Abs(gameObject.GetComponent<Rigidbody>().velocity.y);
            audioSource.volume = (speed_jump / 10);

            //si la altura es muy poca se pone el volumen a 0
            if (audioSource.volume < 0.05f)
            {
                audioSource.volume = 0.0f;
            }

            

            audioSource.Play();

        }
    }
    public AudioClip random(AudioClip[] audio)
    {
        //retorna un audios random en el array usado para diferentes pasos
        AudioClip audioClip = Footsteps[Random.Range(0, audio.Length)];

        audioSource.clip = audioClip;


        return audioClip;

    }
    public void playAuido(AudioClip audioClip)
    {
        AudioClip pivot;
        pivot = audioSource.clip;
        audioSource.clip = audioClip;


        audioSource.Play();



    }

    public void ChangeCamera()
    {
        Cam_cinemachine.GetComponent<CinemachineFreeLook>().LookAt = gameObject.transform;
        tarjet.GetComponent<Target_cam>().player = gameObject;


    }
}
