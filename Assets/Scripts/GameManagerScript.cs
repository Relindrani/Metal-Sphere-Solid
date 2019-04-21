using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class GameManagerScript : MonoBehaviour {
	public static GameManagerScript instance;

	public Vector3 spawnPoint;

	public Texture2D heartTexture;
	public Texture2D fileTexture, keyTexture;

	public GameObject pauseOverlay;

	public bool isPaused=false;
	public bool hasFiles, hasBlueKey, hasRedKey, hasGreenKey;

	public int lives=3;

	void Awake(){
		if(!instance)instance=this;
		else Destroy(gameObject);
		spawnPoint=transform.position;
		Time.timeScale=1;
	}
	void Start(){
		pauseOverlay.SetActive(false);

	}
	void OnEnable(){
		EventManagerScript.OnPause+=Pause;
		EventManagerScript.OnResume+=Resume;
	}
	void OnDisable(){
		EventManagerScript.OnPause-=Pause;
		EventManagerScript.OnResume-=Resume;
	}
	public void Pause(){
		isPaused=true;
		Time.timeScale=0;
		GetComponent<FirstPersonController>().enabled=false;
		pauseOverlay.SetActive(true);
	}
	public void Resume(){
		isPaused=false;
		Time.timeScale=1;
		GetComponent<FirstPersonController>().enabled=true;
		pauseOverlay.SetActive(false);
	}
	void OnGUI(){
		if(!isPaused){
			for(int i=0;i<lives;i++){
				GUI.DrawTexture(new Rect(Screen.width-100-(32*i),Screen.height-50,32,32),heartTexture);
			}
			Color prevColor=GUI.color;
			if(hasRedKey){
				GUI.color=Color.red;
				GUI.DrawTexture(new Rect(40,Screen.height-50,32,32),keyTexture);
			}if(hasBlueKey){
				GUI.color=Color.blue;
				GUI.DrawTexture(new Rect(80,Screen.height-50,32,32),keyTexture);
			}if(hasGreenKey){
				GUI.color=Color.green;
				GUI.DrawTexture(new Rect(120,Screen.height-50,32,32),keyTexture);
			}
			GUI.color=prevColor;
			if(hasFiles){
				GUI.DrawTexture(new Rect(160,Screen.height-50,32,32),fileTexture);
			}
		}
	}
}