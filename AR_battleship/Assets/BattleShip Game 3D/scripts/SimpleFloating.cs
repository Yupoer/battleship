using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleFloating : MonoBehaviour
{

                        
    //these are the internal parameters that are obtained in the script;
    
    //this is the rigidbody attached to the gameobject
    private Rigidbody rb;
    //initial angle in floatation
    private float InitialAngle;
    //aplitude of the sinusoid
    public float bouncingAmplitude = 20f;
    //speed of the sinusoid
    public float boundcingSpeed = 0.5f;
    //initial conditions
    Quaternion initialRot;
    Vector3 initialPosition;

    void Start()
    {
        //initial references
        rb = GetComponent<Rigidbody>();
        initialRot = transform.rotation;
        InitialAngle =Random.Range(-Mathf.PI, Mathf.PI);
        initialPosition =transform.position;
    }


  
    // Update is called once per frame
    void FixedUpdate()
    {
        //sinusoid movement
        Quaternion Rot= initialRot*Quaternion.Euler(0, 0,+bouncingAmplitude * Mathf.Sin(boundcingSpeed * Time.fixedTime + InitialAngle));
        Vector3 pos = initialPosition+new Vector3(0,1,0)* bouncingAmplitude/100 * Mathf.Sin(boundcingSpeed * Time.fixedTime + InitialAngle);

        rb.MovePosition(pos);

        rb.MoveRotation(Rot);

    }

        
}