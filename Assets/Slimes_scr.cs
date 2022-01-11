using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slimes_scr : MonoBehaviour
{
    public bool In_Jail;
    public GameObject manager;
    private bool check = false;

    // Start is called before the first frame update
    void Start()
    {
        In_Jail = true;
        if (manager == null)
        {
            manager = GameObject.Find("Playermanager");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            if (In_Jail==true)
            {
                //say heelp mee, make me free. this idiots me an encerrado
            }
            else if(check==false)
            {
                check = true;
                StartCoroutine(Timer());
                manager.GetComponent<Gamemanager>().addpunt();
                //+1 en puntación
            }


        }
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(1);
        check = false;
        gameObject.SetActive(false);

    }
   
}
