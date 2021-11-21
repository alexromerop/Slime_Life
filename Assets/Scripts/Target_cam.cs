using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Target_cam : MonoBehaviour
{

    [SerializeField] GameObject player;
    [SerializeField] float distance_h_player;
    [SerializeField] float dis_jump;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position+new Vector3(0, distance_h_player,0);
    }

    private void FixedUpdate()
    {
        CheckGroundStatus();
        
    }

    void CheckGroundStatus()
    {
        dis_jump = distance_h_player + 1f;
        RaycastHit hit;
        Ray landingRay = new Ray(transform.position, Vector3.down);
        Debug.DrawRay(transform.position, Vector3.down * dis_jump);

       
        if (Physics.Raycast(landingRay, out hit, dis_jump))
        {
            if (hit.collider == null)
            {
                player.GetComponent<Character_movment_scr>().isGrounded = false;
            }
            else
            {
                player.GetComponent<Character_movment_scr>().isGrounded = true;
            }

        }
    }

}
