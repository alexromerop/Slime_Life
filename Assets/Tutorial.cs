using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] private string value;
    private bool textIn = true;
    float targetTime = 5.0f;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        targetTime -= Time.deltaTime;

        if (!textIn&&targetTime <=0)
        {
            text.text = "";
            targetTime = 5;
        }

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            text.text = value;
            targetTime = 5;
            textIn = false;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            textIn = true;
        }
    }


}
