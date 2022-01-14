using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    PickUp PickUpItem;
    [SerializeField]
    
    public float Health = 100.0f;
    [SerializeField]
    GameObject PickUpUI;
    public GameObject Pick;
    public GameObject Bullet;




    public AudioClip[] Error;


    private Material Orignal_mat;
    public Material Slime_mat;
    Cinemachine.CinemachineImpulseSource source;


    [SerializeField] public int num_bullets = 0;
    public float force;



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        
    }
    // Update is called once per frame
    void Update()
    {

        if (PickUpItem!= null)
        {

            PickUpUI.transform.position = PickUpItem.transform.position + Vector3.up;
            PickUpUI.transform.rotation = Quaternion.LookRotation((PickUpItem.transform.position - transform.position).normalized);
        }
    }

    private void SetInteractFocus(PickUp NewInteract)
    {
        if (PickUpItem != null)
        {
            PickUpItem.bBeingTargeted = false;
        }
        PickUpItem = NewInteract;
        if (PickUpItem != null)
        {
            PickUpItem.bBeingTargeted = true;
            PickUpUI.SetActive(true);
        }
    }

    private void GetClosestPickUpItem(PickUp OtherPickUp)
    {
        if (PickUpItem == null)
        {
            SetInteractFocus(OtherPickUp);
        }

        float ClosestItemDistance = Vector3.Distance(transform.position, PickUpItem.transform.position);
        float OtherPickUpDistance = Vector3.Distance(transform.position, OtherPickUp.transform.position);
        if (OtherPickUpDistance < ClosestItemDistance)
        {
            SetInteractFocus(OtherPickUp);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        

        PickUp PickUpComponent;
        if (other.gameObject.TryGetComponent<PickUp>(out PickUpComponent))
        {
            if (PickUpItem == null)
            {
                SetInteractFocus(PickUpComponent);
            }
            else
            {
                GetClosestPickUpItem(PickUpComponent);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PickUp PickUpComponent = other.gameObject.GetComponent<PickUp>();
        if (PickUpComponent && PickUpComponent == PickUpItem)
        {
            
            SetInteractFocus(null);
            PickUpItem = null;
            PickUpUI.SetActive(false);
        }
        Pick = null;

    }

    private void OnTriggerEnter(Collider other)
    {

        if(other.CompareTag("Pickable"))
        Pick = other.gameObject;


       

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            GameObject.Find("Canvas").gameObject.GetComponent<Score>().Lose.SetActive(true);


        }
    }

    public void get_ston()
    {
        
        if (Pick != null)
        if(Pick.tag == "Pickable")
        {
            PickUp PickUpComponent = Pick.gameObject.GetComponent<PickUp>();
            if (PickUpComponent && PickUpComponent == PickUpItem)
            {
                num_bullets++;

                PickUpUI.SetActive(false);
                Destroy(Pick);

                //seria mejor transportarlas dentro del player sin colision i hacerlas hijop para qeu se viera que estan dentro i ver el numero de municion
                
                Pick = null;




            }

        }
        else
        {
        Debug.Log("w2");

        }




    }


    public void shoot(GameObject cam)
    {
        //if iming
        if (num_bullets > 0)
        {
            GameObject a = Instantiate(Bullet, transform.position, Quaternion.identity);
          //  a.gameObject.GetComponent<Rigidbody>().centerOfMass = a.gameObject.transform.position;

            Vector3 cameraForward = cam.transform.forward;
            cameraForward.y = 0;


            a.gameObject.GetComponent<Rigidbody>().AddForce(cameraForward * (10 * Random.Range(1.3f, 1.7f)), ForceMode.Impulse);
            a.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0,1,0) * 5, ForceMode.Impulse);
            //a.gameObject.GetComponent<Rigidbody>().AddForce


            StartCoroutine(Change_Material(a));
          



            num_bullets--;


            this.gameObject.GetComponent<Character_movment_scr>().playAuido(gameObject.GetComponent<Character_movment_scr>().Footsteps[1]);

        }
        else
        {
            num_bullets = 0;
            this.gameObject.GetComponent<Character_movment_scr>().playAuido(gameObject.GetComponent<Character_movment_scr>().random(Error));


        }



    }


   

    




    IEnumerator Change_Material(GameObject Drop)
    {
        this.gameObject.GetComponent<SphereCollider>().enabled = false;

        Drop.gameObject.layer = LayerMask.NameToLayer("Ignore_Player");

        Orignal_mat = Drop.gameObject.GetComponent<Renderer>().material;
        Drop.gameObject.GetComponent<Renderer>().material = Slime_mat;

        yield return new WaitForSeconds(2);
        this.gameObject.GetComponent<SphereCollider>().enabled = true;
        

        yield return new WaitForSeconds(1);
        Drop.gameObject.layer = LayerMask.NameToLayer("All_les_enemys");

        Drop.gameObject.GetComponent<Renderer>().material = Orignal_mat;


    }
}
