using UnityEngine;
using System.Collections;

public class LadderScript : MonoBehaviour {
	private bool onLadder = false;
	private GameObject player;
	private float savedSpeed;
	
	void Start(){
	}
	
	void OnTriggerStay(){
		Debug.Log(player.transform.position);
		if (onLadder){
			player.GetComponent<PlayerCharacter>().stopGravity();
		}
		
		if ((Input.GetAxis("Ladder") > 0)){
			onLadder = true;
			player.transform.position += new Vector3(0.0f, 0.2f, 0.0f);
		}
	}
	
	void OnTriggerEnter(Collider collider){
		if (collider.gameObject.tag == "Player"){
			//savedSpeed = collider.gameObject.GetComponent<PlayerCharacter>().m_movementSpeed;
			player = collider.gameObject;
		}
	}
	
	void OnTriggerExit(Collider collider){
		if (collider.gameObject.tag == "Player"){
			//collider.gameObject.GetComponent<PlayerCharacter>().m_movementSpeed = savedSpeed;
			player.GetComponent<PlayerCharacter>().startGravity();
			player = null;
			onLadder = false;
		}
	}
}