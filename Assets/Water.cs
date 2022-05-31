using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
            other.GetComponent<Rigidbody>().AddForce(new Vector3(0,0.45f,0),ForceMode.Impulse);

        }
    }
}
