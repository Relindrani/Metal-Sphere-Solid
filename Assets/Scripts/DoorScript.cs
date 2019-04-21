using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class DoorScript : MonoBehaviour {
	GameManagerScript player;

	public AudioClip open,victory;

	void Start () {
		player=GameManagerScript.instance;
	}
	void OnTriggerEnter(Collider col){
		if(col.gameObject==player.gameObject){
			if(gameObject.CompareTag("Exit")&&player.hasFiles){
				StartCoroutine("LoadWinScreen");
			}if((gameObject.CompareTag("Red")&&player.hasRedKey)||(gameObject.CompareTag("Blue")&&player.hasBlueKey)||(gameObject.CompareTag("Green")&&player.hasGreenKey)){
				AudioSource.PlayClipAtPoint(open,transform.position);
				Destroy (gameObject);
			}
		}
	}

	IEnumerator LoadWinScreen(){
		GameObject.Find("Music Manager").GetComponent<AudioSource>().Stop();
		GameManagerScript.instance.GetComponent<FirstPersonController>().enabled=false;
		AudioSource.PlayClipAtPoint(victory,transform.position);
		yield return new WaitForSeconds(victory.length);
		LevelManagerScript.instance.LoadLevel ("Win Screen");
		StopCoroutine("LoadWinScreen");
	}
}