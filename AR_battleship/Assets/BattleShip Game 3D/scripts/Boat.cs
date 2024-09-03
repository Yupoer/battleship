using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour
{
    // Start is called before the first frame update

    //game manager script
    GameMang gameS;
    //canvas in which
    Canvas cv;
    //this is the number of received impacts
    public int nb_impacts;
    //this is the initial life of the boat
    public int life =3;
    //this variable shows if the boat has been destroyed or not.
    public bool destroyed=false;

    void Start()
    {
        gameS = GameObject.FindGameObjectWithTag("GameMang").GetComponent<GameMang>();
        cv = transform.GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        //life changes in function of number of impacts received
        if(life<=nb_impacts)
        {
            //destroyed is set to true if it life gets to zero
            destroyed = true;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        // can place means that the boat can be placed on the water if there is no collision
        gameS = GameObject.FindGameObjectWithTag("GameMang").GetComponent<GameMang>();
        gameS.canPlace = false;
        //Debug.Log("collision");
    }

    public void OnTriggerExit(Collider other)
    {
        gameS.canPlace = true;
    }
    public void disableCanvas()
    {
        cv.enabled = false;
    }
}
