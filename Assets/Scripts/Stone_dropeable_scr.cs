using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Stone_dropeable_scr : PickUp
{

    [SerializeField] bool stong_interacting = false;
    public bool Shoting;
    
 



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!stong_interacting)
            {
                Debug.Log("Mostar boton E");

                stong_interacting = true;
            }

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (stong_interacting)
            {
                Debug.Log("Dejar de mostar boton E");
                 
                stong_interacting = false;
            }

        }
    }
 

    public void delet()
    {
        
    }


    
    
}
