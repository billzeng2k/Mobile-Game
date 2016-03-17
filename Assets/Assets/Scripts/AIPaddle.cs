using UnityEngine;
using System.Collections;

public class AIPaddle : MonoBehaviour {
	//flag to check if the user has tapped / clicked. 
	//Set to true on click. Reset to false on reaching destination
	public float speed = 0.7f;
	private bool flag = false;
	//destination point
	private Vector3 endPoint;
	//alter this to change the speed of the movement of player / gameobject
	public float duration = 0.5f;
	//vertical position of the gameobject
	private float yAxis;
	//Bounce force for puck
	public float bounceForce = 50.0f;
	public GameObject targ = GameObject.FindGameObjectWithTag ("Puck");
	private double t;
	private double glitch;

	void Start(){
		//save the y axis value of gameobject
		yAxis = gameObject.transform.position.y;
	}

	// Update is called once per frame
	void Update () {
		if (Time.time > t) {
			flag = false;
		}
		if (!flag) {
			transform.position = Vector3.MoveTowards (transform.position, targ.transform.position, speed);
		}
	}

	void OnCollisionEnter(Collision hit) {
		//Debug.Log (hit.gameObject.name + " has been hit");
		glitch = Time.time + 0.3;
		if (hit.gameObject.tag == "Puck") {
			t = Time.time + 0.2;
			flag = true;
			Debug.Log ("Puck has been hit");
			float velX = GetComponent<Rigidbody>().velocity.x + 1;
			float velZ = GetComponent<Rigidbody>().velocity.z + 1;
			//Vector3 velocity = Vector3((GetComponent<Rigidbody>().velocity.x) * bounceForce, 0.0f, (GetComponent<Rigidbody>().velocity.z) * bounceForce);
			//hit.rigidbody.AddForce( hit.contacts[0].normal * bounceForce * -1 * GetComponent<Rigidbody>().velocity, ForceMode.VelocityChange);
			hit.rigidbody.AddForceAtPosition(new Vector3(velX * bounceForce, 0, velZ * bounceForce), hit.contacts[0].normal, ForceMode.Impulse);
		}
	}
	void OnCollisionStay(Collision hit){
		if (Time.time >= glitch) {
			Debug.Log ("Glitch");
			t = Time.time + 0.3;
			flag = true;	
			transform.position = Vector3.MoveTowards (transform.position, -targ.transform.position, speed);
			float velX = GetComponent<Rigidbody>().velocity.x + 1;
			float velZ = GetComponent<Rigidbody>().velocity.z + 1;
			hit.rigidbody.AddForceAtPosition(new Vector3(velX * bounceForce, 0, velZ * bounceForce), hit.contacts[0].normal, ForceMode.Impulse);
		}
	}
}
