using UnityEngine;
using System.Collections;

public class EventManagerScript : MonoBehaviour {
	public delegate void Action();
	public static event Action OnPause;
	public static event Action OnResume;

	void Update(){
		if(Input.GetKeyDown(KeyCode.P)){
			if(!GameManagerScript.instance.isPaused){
				if(OnPause!=null)OnPause();
			}else if(OnResume!=null)OnResume();
		}
	}
}