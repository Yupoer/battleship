using UnityEngine;
using System.Collections;

public class LookAtGiven : MonoBehaviour {

	// Use this for initialization
	public Transform lookObjective;

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(lookObjective!=null)
		{
			transform.LookAt(lookObjective);
		}
	
	}
}
