using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemanager : MonoBehaviour
{
    public GameObject[] Jails;
    public GameObject[] Players;
    public GameObject[] Slimes;



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
}
