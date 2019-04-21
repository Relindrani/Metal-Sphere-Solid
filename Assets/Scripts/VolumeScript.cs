using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VolumeScript : MonoBehaviour {

	Text curVolume;

	// Use this for initialization
	void Start () {
		curVolume=GameObject.Find ("CurrentVolume").GetComponent<Text>();
		updateVolume();
	}

	public void increaseVolume(){
		AudioListener.volume+=.05f;
		if(AudioListener.volume>=1.0f)AudioListener.volume=1.0f;
		updateVolume();
	}
	public void reduceVolume(){
		AudioListener.volume-=.05f;
		if(AudioListener.volume<=0.0f)AudioListener.volume=0.0f;
		updateVolume();
	}
	void updateVolume(){
		curVolume.text=(Mathf.Round (AudioListener.volume*100)).ToString();
	}
}
