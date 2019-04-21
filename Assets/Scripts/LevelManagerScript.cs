using UnityEngine;
using System.Collections;

public class LevelManagerScript : MonoBehaviour {
	public static LevelManagerScript instance;

	public int previousLevel;

	void Awake(){
		if(!instance)instance=this;
		else Destroy(gameObject);
		DontDestroyOnLoad(gameObject);
	}
	public void LoadLevel(string name){
		Application.LoadLevel(name);
		previousLevel=Application.loadedLevel;
	}
	public void QuitRequest(){
		Debug.Log ("Quit Game");
		Application.Quit();
	}
	public void LoadNextLevel(){
		Application.LoadLevel (Application.loadedLevel+1);
	}
	public void LoadPreviousLevel(){
		Application.LoadLevel(instance.previousLevel);
	}
}