using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jail_scr : MonoBehaviour
{
    public GameObject manager;


    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("Playermanager");

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnCollisionEnter(Collision collision)
    {


        if (collision.gameObject.CompareTag("Pickable"))
        {
            gameObject.SetActive(false);

        }
    }
}
