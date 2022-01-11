using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jail_scr : MonoBehaviour
{
    public GameObject manager;
    public GameObject Child;

    // Start is called before the first frame update
    void Start()
    {
        if (manager == null)
        {
            manager = GameObject.Find("Playermanager");
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnCollisionEnter(Collision collision)
    {


        if (collision.gameObject.CompareTag("Pickable"))
        {
            gameObject.GetComponentInChildren<SphereCollider>().enabled = true;
            Child.transform.SetParent(null);
            Child.GetComponent<Slimes_scr>().In_Jail = false;
           


            gameObject.SetActive(false);
            //call particles and sound destruct
            

        }
    }
}
