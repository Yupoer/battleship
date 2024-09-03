using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    // Start is called before the first frame update

    //this is the missile gameobject
    public GameObject missile;
    //these are the starting points of the missiles
    public GameObject silo1, silo2;
    //gamemanager
    GameMang gameS;
    //container for the instances
    Transform container;
    //player p=1 or p=2
    public int p = 0;

    void Start()
    {
        //get initial references
        gameS = GameObject.FindGameObjectWithTag("GameMang").GetComponent<GameMang>();
        container= GameObject.FindGameObjectWithTag("missiles").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void launch_player(Transform dest)
    {
        //set the clicks and the shooting events on the gamemanager
        Transform origin;
        gameS.click = 0;
        gameS.shoot = true;

        //select silo according to the player state
        if (p == 1)
        {
            origin = silo1.transform;
        }
        else
        {
            origin = silo2.transform;
        }
        //generate missile
        GameObject m = Instantiate(missile, origin.position, Quaternion.Euler(0, 0, 0));
        m.transform.SetParent(container);

        //set origin and destination of missile
        Missile miss_Script =m.GetComponent<Missile>();
        miss_Script.dest = dest;
        miss_Script.origin = origin;


    }


}
