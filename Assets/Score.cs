using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class Score : MonoBehaviour
{

    public int score = 1;

    public int total = 5;
    public GameObject manager;
    public GameObject gameObject;

    public GameObject Win;
    public GameObject Lose;
    public bool wait = false;



    // Start is called before the first frame update
    void Start()
    {
        if (manager == null)
        {
            manager = GameObject.Find("Playermanager");
        }

        total = manager.GetComponent<Gamemanager>().Slimes.Length;

       StartCoroutine(late());
    }

    // Update is called once per frame
    void Update()
    {
        
       score = manager.GetComponent<Gamemanager>().Slimes_free;
        
        gameObject.GetComponent<TextMeshProUGUI>().text = "Score: " + score + "/" + total;

        if(score == total && wait ==true)
        {
            Win.SetActive(true);
            

        }
        /*
        if(manager.GetComponent<Gamemanager>().Players[0].activeSelf==false&& manager!=null)
        {
            manager.GetComponent<Gamemanager>().RechargePlayer();
            manager.GetComponent<Gamemanager>().Players[0].isStatic = true;
            Debug.Log(Lose);
            Lose.SetActive(true);
            wait = true;

        }
        */
    }


   


    IEnumerator late()
    {


        yield return new WaitForSeconds(0.1f);
        total = manager.GetComponent<Gamemanager>().Slimes.Length;
        wait = true;
    }
}
