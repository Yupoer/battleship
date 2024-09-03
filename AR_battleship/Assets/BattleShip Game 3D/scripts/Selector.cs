using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour
{
    // Start is called before the first frame update

    //camera script
    CamScript camS;
    //game manager
    GameMang gameS;

    void Start()
    {
        gameS = GameObject.FindGameObjectWithTag("GameMang").GetComponent<GameMang>();
        camS = GameObject.Find("Main Camera").GetComponent<CamScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //when clicking increment number of clicks of the gamemanager +1 and set clickobject to the one that was clicked (this)
    public void onclick()
    {
        if (gameS.player==-1 || gameS.player == -2)
        {
            camS.objective = transform;

        }
        gameS.clickObject = gameObject;
        gameS.click += 1;
    }

    // set the overObject of the gamescript to this
    public void p_enter()
    {
        gameS.overObject = gameObject;
       
    }
    // set the overObject of the gamescript to null
    public void p_exit()
    {
        gameS.overObject = null;
    }
}
