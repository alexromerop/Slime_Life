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

    public GameObject slimeNear;

    public AudioClip[] Error;
    public bool onWater = false;
    public bool onWaterTimer = true;


    private Material Orignal_mat;
    public Material Slime_mat;
    Cinemachine.CinemachineImpulseSource source;


    [SerializeField] public int num_bullets = 0;
    public float force;


    private void Start()
    {
        PickUpUI = GameObject.Find("PickUpUi");
        onWaterTimer = true;

    }

    // Update is called once per frame
    void Update()
    {

        //if (PickUpItem!= null)
        //{
        //    //inventario sin uso
        //    PickUpUI.transform.position = PickUpItem.transform.position + Vector3.up;
        //    PickUpUI.transform.rotation = Quaternion.LookRotation((PickUpItem.transform.position - transform.position).normalized);
        //}
    }

    private void SetInteractFocus(PickUp NewInteract)
    {
        //alterna el focus dsegun el ovjeto mas cercano
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
        //Coger objeto sin uso
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

        //para saber con que slime unirte
        if (other.CompareTag("Player") && other != gameObject)
        {
            slimeNear = other.gameObject;
        }
        if (other.CompareTag("water"))
        {
            onWater = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //para el cogerobjetos
        PickUp PickUpComponent = other.gameObject.GetComponent<PickUp>();
        if (PickUpComponent != null)
        if (PickUpComponent && PickUpComponent == PickUpItem)
        {
            
            SetInteractFocus(null);
            PickUpItem = null;
            PickUpUI.SetActive(false);
        }
        Pick = null;

        //no slimes cerca
        slimeNear=null;

        if (other.CompareTag("water"))
        {
            onWater = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Pickable"))
        Pick = other.gameObject;

    }
    private void OnCollisionEnter(Collision collision)
    {
        //sistema de perdido simple
        if (collision.gameObject.CompareTag("Enemy")){
            if (transform.localScale.x <= 1.8)
            {
                GameObject.Find("Canvas").gameObject.GetComponent<Score>().Lose.SetActive(true);

            }
            else
            {
                Destroy(collision.gameObject);
                transform.transform.localScale += new Vector3(0.5f, 0.5f, 0.5f);
            } 
        }

    }



    public void get_water()
    {
        Debug.Log(onWaterTimer);
        //Crecer al dar input en el agua
        if (onWater && onWaterTimer)
        {
            Debug.Log("water2");

            onWaterTimer = false;
            StartCoroutine(TimeToWater());
            transform.localScale += new Vector3(0.3f,0.3f,0.3f);
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
        //para los disparos ahora sin uso
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
    public void Polifusion(GameObject other )
    {
        //Juntar 2 slimes de cuaklquier tamaño
        gameObject.transform.localScale += other.transform.localScale;
        Destroy(other);
        Player_Manager.instance.GetPlayers();

    }


    IEnumerator TimeToWater()
    {
        yield return new WaitForSeconds(30);
        onWaterTimer = true;

    }
}
