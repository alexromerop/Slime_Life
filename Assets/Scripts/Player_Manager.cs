using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Player_Manager : MonoBehaviour
{
    public static Player_Manager instance { get; private set; }

    [SerializeField] private Character[] character;
    


    private void Awake()
    {

        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    
	}


    public void GetPlayers()
    {

        character = FindObjectsOfType<Character>();



    }

    public void ChangePlayer(Character player, int value)
    {
        //pivota de personaje al que cambiaras, pasando al final de l array si no es el 0 i al reves
        Player_Manager.instance.GetPlayers();
        if (character != null)
        {
            for (int i = 0; i < character.Length; i++)
            {
                if(character[i] == player)
                {
                    int pivot = i + value;

                    ActivePlayer(player, false);
                    if(pivot>= character.Length)
                    {
                        pivot = 0;
                    }
                    if (pivot == -1)
                    {
                        pivot =character.Length-1; 
                    }
                    ActivePlayer(character[pivot], true);

                }
            }
        }
    }

    public void ActivePlayer(Character ca, bool value)
    {
        //aqui segun el pivote se combia el personaje
        if (character != null)
        {
            ca.gameObject.GetComponent<Character_movment_scr>().enabled = value;
            ca.gameObject.GetComponent<PlayerInput>().enabled = value;
            ca.enabled = value;
            if (value)
            {
                ca.gameObject.GetComponent<Character_movment_scr>().ChangeCamera();

            }
        }

    }

   
}
