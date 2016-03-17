using UnityEngine;
using System.Collections;

public class PuckController : MonoBehaviour {
	//public float bounceForce = 5;

	void Start () 
	{
		GetComponent<Rigidbody> ().AddForce (100, 0, 100);
	}


}
