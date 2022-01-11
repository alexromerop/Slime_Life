using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Enemy_scr : MonoBehaviour
{



    NavMeshAgent myNavMeshAgent;
    public GameObject target;
    [SerializeField] GameObject[] targets_to_move;
    public bool targeted;

    // Start is called before the first frame update
    void Start()
    {
        myNavMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        myNavMeshAgent.SetDestination(target.transform.position);
        Chek_target();




    }



    IEnumerator go()
    {


        yield return new WaitForSeconds(5);



    }

    
    public void Chek_target()
    {
        if (myNavMeshAgent.remainingDistance < 1 )
        {
            if (targeted == false)
            {

                int random = Random.Range(0, targets_to_move.Length);
                target = targets_to_move[random];


            }
            
             
        }
       

    }


    private void OnTriggerEnter(Collider other)
    {
       
        if (other.gameObject.CompareTag("Player"))
        {
            targeted=true;

            target = other.gameObject;




        }
    }
    private void OnTriggerExit(Collider other)
    {
        targeted=false;

        Chek_target();
    }


}
