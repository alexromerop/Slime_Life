using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public virtual void StartInteract(Character_movment_scr Other) { }
    public virtual void StopInteract(Character_movment_scr Other) { }
    public Transform UIPosition;
    public bool bBeingTargeted = false;
    public float RotationSpeed = 0.1f;
    float ActualAngle = 0.0f;
    TextMesh PickText;
    GameObject TextGameObject;



    public void Awake()
    {

        if (TextGameObject == null)
        {
            TextGameObject = gameObject;
        }

    }

    public void Update()
    {
        ActualAngle += RotationSpeed;
        transform.rotation = Quaternion.Euler(Vector3.up * ActualAngle);
    }
}
