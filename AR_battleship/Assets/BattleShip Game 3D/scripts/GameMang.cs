using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameMang : MonoBehaviour
{
    // Start is called before the first frame update

    //this is the number of boats that the players place in the respective order
    public GameObject[] boats;

    //evolutionary game variable from:
    // 1 --> player 1 places boats  
    // 1 --> player 2 places boats  
    // -1 --> player 1's turn 
    // -2 --> player 2's turn 
    public int player = 1;
    
    //to iterate along the different boats
    public int selectedBoat=0;
    //gameobjects that are selected by clicking or passing over in pointer events
    public GameObject overObject, clickObject;

    //evolutionary variable that counts the number of clicks
    public int click = 0;

    //set to true if the boat can be placed
    public bool canPlace=true;
    //camera script
    CamScript cmS;
    //positions for the player 1 and player 2 turn
    public Transform P1pos;
    public Transform P2pos;
    //messages
    public GameObject mess_player1_start, mess_player2_start, mess_player1, mess_player2, mess_gameOver;
    //player 1 and player 2 boats
    public GameObject[] boats_player1, boats_player2;
    //this is the button used to shoot a missile
    public GameObject shooting_square;
    //used to indicate if a shoot has been performed
    public bool shoot;
    //containers for the boats for each player
    public Transform c_p1, c_p2;
    //shooting script
    Shooting shootScript;

    void Start()
    {
        //initialize the boats variable
        boats_player1 = new GameObject[boats.Length];
        boats_player2 = new GameObject[boats.Length];

        mess_player1_start.SetActive(true);
        cmS = Camera.main.GetComponent<CamScript>();

        //instantiate boat and set parent
        boats_player1[selectedBoat] = GameObject.Instantiate(boats[selectedBoat],new Vector3(20000,20000,20000), Quaternion.Euler(0,0,0)) as GameObject;
        boats_player1[selectedBoat].transform.SetParent(c_p1);


        //get shooting script
        shootScript =shooting_square.GetComponent<Shooting>();

        cmS.objective = P1pos;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        //////////////////////////////////
        // placing boats
        //////////////////////////////////
        //case of player one
        if (player==1)
        {

            if(overObject!=null)
            {
                //only if selected a grid square of player 1
                if (overObject.tag=="grid_square_P1")
                {
                    if (click == 0)
                    {
                        boats_player1[selectedBoat].transform.position = overObject.transform.position;
                    }
                    else if (click == 1)
                    {
                        //we obtain the direction for rotation
                        Vector3 dir = overObject.transform.position - boats_player1[selectedBoat].transform.position;
                        // diagonals are not allowed
                        if (Mathf.Abs(dir.z / dir.x) > 1e5 || Mathf.Abs(dir.z / dir.x) < 0.1)
                        {
                            boats_player1[selectedBoat].transform.forward = dir;
                        }
                    }
                    else if (click == 2)
                    {
                        //only if it is possible to place the boat, create a new boat
                        if (canPlace)
                        {
                            boats_player1[selectedBoat].transform.GetChild(1).GetComponent<SimpleFloating>().enabled = true;
                            boats_player1[selectedBoat].transform.GetChild(0).GetComponent<Boat>().disableCanvas();

                            //increase the boat number
                            selectedBoat += 1;

                            //when there is no more boats to place
                            if (selectedBoat >= boats.Length)
                            {
                                player = 2;
                                mess_player2_start.SetActive(true);
                                selectedBoat = 0;
                                //go to player2 zone
                                cmS.objective = P2pos;
                                cmS.Phi = 180-45;

                                //disable render of boats
                                disableBoats(boats_player1);


                                //start with boat of player 2
                                boats_player2[selectedBoat] = GameObject.Instantiate(boats[selectedBoat], new Vector3(20000, 20000, 20000), Quaternion.Euler(0, 0, 0)) as GameObject;
                                boats_player2[selectedBoat].transform.SetParent(c_p2);
                            }
                            else
                            {
                                boats_player1[selectedBoat] = GameObject.Instantiate(boats[selectedBoat], new Vector3(20000, 20000, 20000), Quaternion.Euler(0, 0, 0)) as GameObject;
                                boats_player1[selectedBoat].transform.SetParent(c_p1);
                            }
                            click = 0;
                        }
                        else
                        {
                            click = 0;
                        }
                    }
                }
            }        

            
        }
        
        //case of player two
        else if (player == 2)
        {
            if (overObject != null)
            {
                //only if selected a grid square of player 1
                if (overObject.tag == "grid_square_P2")
                {
                    if (click == 0)
                    {
                        boats_player2[selectedBoat].transform.position = overObject.transform.position;
                    }
                    else if (click == 1)
                    {
                        //we obtain the direction for rotation
                        Vector3 dir = overObject.transform.position - boats_player2[selectedBoat].transform.position;
                        // diagonals are not allowed
                        if (Mathf.Abs(dir.z / dir.x) > 1e5 || Mathf.Abs(dir.z / dir.x) < 0.1)
                        {
                            boats_player2[selectedBoat].transform.forward = dir;
                        }
                    }
                    else if (click == 2)
                    {
                        //only if it is possible to place the boat, create a new boat
                        if (canPlace)
                        {
                            boats_player2[selectedBoat].transform.GetChild(1).GetComponent<SimpleFloating>().enabled = true;
                            boats_player2[selectedBoat].transform.GetChild(0).GetComponent<Boat>().disableCanvas();

                            //increase the boat number
                            selectedBoat += 1;

                            //when there is no more boats to place
                            if (selectedBoat >= boats.Length)
                            {
                                player = -1;
                                                                
                                //go to player2 zone
                                cmS.objective = P1pos;
                                cmS.Phi =-45;

                                //disable boats
                                disableBoats(boats_player2);
                                mess_player1.SetActive(true);

                            }
                            else
                            {
                                //instantiate boat and set parent
                                boats_player2[selectedBoat] = GameObject.Instantiate(boats[selectedBoat], new Vector3(20000, 20000, 20000), Quaternion.Euler(0, 0, 0)) as GameObject;
                                boats_player2[selectedBoat].transform.SetParent(c_p2);
                            }
                            click = 0;
                        }
                        else
                        {
                            click = 0;
                        }
                    }
                }
            }
        }


        ///////////////////////
        //normal gameMOde
        //////////////////////
        else if(player==-1)
        {
            //show menu of player 1
           
           //onoly display boats that you are interested in
           enableBoats(boats_player1);
           disableBoats(boats_player2);
            if (clickObject != null)
            {
                if (click > 0 && clickObject.tag == "grid_square_P2" && !shoot)
                {
                    //display shooting canvas in place
                    shooting_square.SetActive(true);
                    shooting_square.transform.position = clickObject.transform.position;
                    shootScript.p = 1;

                }
                else
                {
                    shooting_square.SetActive(false);
                }
            }

            if (checkGameOver(boats_player1, boats_player2))
            {
                mess_gameOver.SetActive(true);
            }
        }

        else if (player == -2)
        {
            //show menu of player 1

            //onoly display boats that you are interested in
            enableBoats(boats_player2);
            disableBoats(boats_player1);
            if (clickObject != null)
            {
                if (click > 0 && clickObject.tag == "grid_square_P1" && !shoot)
                {
                    //display shooting canvas in place
                    shooting_square.SetActive(true);
                    shooting_square.transform.position = clickObject.transform.position;
                    shootScript.p = 2;

                }
                else
                {
                    shooting_square.SetActive(false);
                }
            }

            if (checkGameOver(boats_player1, boats_player2))
            {
                mess_gameOver.SetActive(true);
            }
        }

       
    }

    //this part changes the player of position
    public void changePlayer()
    {
        if(player==-1)
        {
            mess_player2.SetActive(true);
            player = -2;
            shoot = false;
            cmS.objective = P2pos;
            cmS.Phi = 180-45;
        }
        else if(player==-2)
        {
            mess_player1.SetActive(true);
            player = -1;
            shoot = false;
            cmS.objective = P1pos;
            cmS.Phi = -45;
        }
    } 


    //change renders of boats to active or inactive
    public void disableBoats(GameObject[] go)
    {
        for(int ii=0; ii<go.Length;ii++)
        {
            //boat script in order to check if it is destroyed or not
            Boat boatS;
            boatS=go[ii].transform.GetChild(0).GetComponent<Boat>();
            //if it is destroyed, show up the boat
            if (boatS.destroyed == false)
            {
                //we need to dissable all the meshrenderers of the game object
                MeshRenderer[] renderers;
                renderers = go[ii].GetComponentsInChildren<MeshRenderer>();

                foreach (MeshRenderer renderer in renderers)
                {
                    renderer.enabled = false;
                }
            }
        }
    }

    public void enableBoats(GameObject[] go)
    {
        for (int ii = 0; ii < go.Length; ii++)
        {
            //we need to enable all the meshrenderers of the game object
            MeshRenderer[] renderers;
            renderers = go[ii].GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer renderer in renderers)
            {
                renderer.enabled = true;
            }
        }
    }


    bool checkGameOver(GameObject[] g1, GameObject[] g2)
    {
        //check if the player 1 or player 2 has been destroyed
        bool end1 = true;
        bool end2 = true;

        for (int ii = 0; ii < g1.Length; ii++)
        {
            end1=end1&g1[ii].transform.GetChild(0).GetComponent<Boat>().destroyed;
        }

        for (int ii = 0; ii < g2.Length; ii++)
        {
            end2 = end2 & g2[ii].transform.GetChild(0).GetComponent<Boat>().destroyed;
        }

        return (end1||end2);
   }


}
