using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Target_cam : MonoBehaviour
{

    [SerializeField] GameObject player;
    [SerializeField] GameObject cam;
    

    [SerializeField] float distance_h_player;
    [SerializeField] float dis_jump;

    [SerializeField] float eyes_roration;

    private float speed_jump;

    public AudioClip[] Footsteps;

    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        Footsteps = new AudioClip[] { (AudioClip)Resources.Load("Auido/Player") };
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 cameraForward = cam.transform.forward;
        cameraForward.y = 0;

        transform.position = player.transform.position+new Vector3(0, distance_h_player,0);
        
        transform.rotation = Quaternion.LookRotation(cameraForward);


        CheckGroundStatus();

    }

    private void FixedUpdate()
    {
        
    }

    void CheckGroundStatus()
    {
        dis_jump = distance_h_player + 1f;
        RaycastHit hit;
        Ray landingRay = new Ray(transform.position, Vector3.down);
        Debug.DrawRay(transform.position, Vector3.down * dis_jump);

       
        if (Physics.Raycast(landingRay, out hit, dis_jump))
        {
            if (hit.collider == null && player.GetComponent<Character_movment_scr>().isGrounded == true)
            {
                player.GetComponent<Character_movment_scr>().isGrounded = false;
            }
            else
            {
                player.GetComponent<Character_movment_scr>().isGrounded = true;
                footstepaudioPlayer();
            }

        }
    }





    public void footstepaudioPlayer()
    {
        if (!audioSource.isPlaying && (Mathf.Abs(player.GetComponent<Rigidbody>().velocity.z)>= 1 || Mathf.Abs(player.GetComponent<Rigidbody>().velocity.x) >=1) || player.GetComponent<Rigidbody>().velocity.y >=1)
        {
         //Segun la altura que caigas sonara mas fuerte
            speed_jump = Mathf.Abs(player.GetComponent<Rigidbody>().velocity.y);
            audioSource.volume = (speed_jump / 10);


            //si la altura es muy poca se pone el volumen a 0
        if (audioSource.volume < 0.05f)
        {
            audioSource.volume = 0.0f;
        }


        audioSource.Play();
       
        }
    }

}
