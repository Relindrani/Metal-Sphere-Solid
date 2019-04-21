using UnityEngine;
using System.Collections;

public class KeyScript : MonoBehaviour {

	GameManagerScript player;

	public AudioClip pickup;

	void Start(){
		player=GameManagerScript.instance;
	}
	void OnTriggerEnter(Collider col){
		if(col.gameObject==player.gameObject){
			if(gameObject.CompareTag("File")){
				player.hasFiles=true;
				Destroy(gameObject.transform.parent.GetChild(2).gameObject);
			}if(gameObject.CompareTag("Red"))player.hasRedKey=true;
			if(gameObject.CompareTag("Blue"))player.hasBlueKey=true;
			if(gameObject.CompareTag("Green"))player.hasGreenKey=true;
			AudioSource.PlayClipAtPoint(pickup,transform.position);
			player.spawnPoint=gameObject.transform.position;
			Destroy(gameObject);
		}
	}
}