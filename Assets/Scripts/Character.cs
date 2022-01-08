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


    private Material Orignal_mat;
    public Material Slime_mat;



    [SerializeField] public int num_bullets = 0;



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
        Pick = other.gameObject;

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

            //a.gameObject.GetComponent<Rigidbody>().AddForce


            StartCoroutine(Change_Material(a));
          



            num_bullets--;


        }
        else
        {
            num_bullets = 0;
            //play sound error
        }



    }







    IEnumerator Change_Material(GameObject Drop)
    {
        this.gameObject.GetComponent<SphereCollider>().enabled = false;

        Orignal_mat = Drop.gameObject.GetComponent<Renderer>().material;
        Drop.gameObject.GetComponent<Renderer>().material = Slime_mat;

        yield return new WaitForSeconds(1);
        this.gameObject.GetComponent<SphereCollider>().enabled = true;


        yield return new WaitForSeconds(2);
        Drop.gameObject.GetComponent<Renderer>().material = Orignal_mat;


    }
}