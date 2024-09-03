using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamScript : MonoBehaviour
{
    // Start is called before the first frame update

    //this is the objective the camera will be looking and moving towards
    public Transform objective;
    //distance to the camera in polar coordinates
    public float Rdist = 15;
    //angles in polar coordinates
    public float Teta=45, Phi=45;
    //script that controls the camera "look at"
    LookAtGiven lookingObject;
    //limits for the zoom (Rdist)
    public float minZoom, maxZoom;
    //variable that is used to change from 2D to 3D
    bool view2D=false;


    void Start()
    {
        lookingObject = transform.GetChild(0).transform.GetComponent<LookAtGiven>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //set the objective as the looking objective in looking script
        lookingObject.lookObjective = objective;

        //polar coordinates
        float x = Rdist * Mathf.Sin(Teta* Mathf.PI/180) * Mathf.Cos(Phi * Mathf.PI / 180);
        float z = Rdist * Mathf.Sin(Teta * Mathf.PI / 180) * Mathf.Sin(Phi * Mathf.PI / 180);
        float y = Rdist *Mathf.Cos(Teta * Mathf.PI / 180);

        //position x y z
        Vector3 pos = objective.position + new Vector3(x, y, z);

        //set postion with lerp and rotation with lerp
        transform.position = Vector3.Lerp(transform.position,pos,0.05f);
        transform.rotation = Quaternion.Lerp(transform.rotation,lookingObject.transform.rotation,0.06f);

    }

    //used to change the angle (rotate along the Y axis)
    public void change_angle(float angle)
    {
        Phi += angle;
    }
    //change the distance of the camera --> zoom
    public void change_zoom(float zoom)
    {
        Rdist += zoom;

        if(Rdist< minZoom)
        {
            Rdist = minZoom;
        }
        if(Rdist> maxZoom)
        {
            Rdist = maxZoom;
        }

    }

    //change from 2D to 3D and vice-versa
    public void change_2D3D()
    {
        view2D= !view2D;
        if (view2D)
        {
            Rdist = 40;
            Teta = 0;
            Phi = 0;
        }
        else
        {
            Rdist = 40;
            Teta = 45;
            Phi = -45;
        }

    }

}
