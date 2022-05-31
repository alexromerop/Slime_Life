using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemanager : MonoBehaviour
{
    public GameObject[] Jails;
    public GameObject[] Players;
    public GameObject[] Slimes;
    [SerializeField] public int Slimes_free =0;


    // Start is called before the first frame update
    void Start()
    {
        Jails = GameObject.FindGameObjectsWithTag("Colect");
        Players = GameObject.FindGameObjectsWithTag("Player");
        Slimes = GameObject.FindGameObjectsWithTag("Slime");


    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void addpunt()
    {
        Slimes_free++;
        if(Slimes_free== Slimes.Length)
        {
            Debug.Log("You win");
        }

    }

    public void RechargePlayer()
    {
        Players = GameObject.FindGameObjectsWithTag("Player");
    }
}
