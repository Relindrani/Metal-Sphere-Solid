using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class SearchlightScript : MonoBehaviour {
	public float fieldOfView;

	public Vector3 pointA,pointB;

	public AudioClip loser;
	public AudioClip alarm;

	bool isSeen;

	void Start(){
		StartCoroutine("StartMoving");
		GetComponent<Light>().spotAngle=fieldOfView*3f;
	}

	void Update(){
		if(GameManagerScript.instance.lives>0)CanSee (GameManagerScript.instance.gameObject);
	}

	IEnumerator StartMoving(){
		/*transform.rotation=Quaternion.Euler(pointA);
		while(true){
			yield return StartCoroutine(Move (transform,Quaternion.Euler(pointA),Quaternion.Euler(pointB),5));
			yield return StartCoroutine(Move (transform,Quaternion.Euler(pointB),Quaternion.Euler(pointA),5));
		}*/
		transform.position=pointA;
		float lerpTime=Vector3.Distance (pointA,pointB)/20;
		while(true){
			yield return StartCoroutine(Move (transform,pointA,pointB,lerpTime));
			yield return StartCoroutine(Move (transform,pointB,pointA,lerpTime));
		}
	}

	/*IEnumerator Move(Transform trans,Quaternion startRot, Quaternion endRot,float time){
		float i=0.0f;
		float rate=1.0f/time;
		while(i<1.0f){
			i+=Time.deltaTime*rate;
			trans.rotation=Quaternion.Lerp(startRot,endRot,i);
			yield return null;
		}
	}*/
	IEnumerator Move(Transform trans,Vector3 startPos,Vector3 endPos,float time){
		float i=0.0f;
		float rate=1.0f/time;
		while(i<1.0f){
			i+=Time.deltaTime*rate;
			trans.position=Vector3.Lerp(startPos,endPos,i);
			yield return null;
		}
	}

	void CanSee(GameObject target){
		Vector3 targetPosition=target.transform.position;
		Vector3 agentToTargetVector=targetPosition-transform.position;

		float angleToTarget=Vector3.Angle(agentToTargetVector,transform.forward);

		if(angleToTarget<fieldOfView){
			Ray rayToTarget=new Ray();

			rayToTarget.origin=transform.position;
			rayToTarget.direction=agentToTargetVector;

			RaycastHit hit;
			if(Physics.Raycast(rayToTarget,out hit,Mathf.Infinity))if(hit.collider.gameObject==target){
				if(!isSeen)GameManagerScript.instance.lives-=1;
				isSeen=true;
				if(GameManagerScript.instance.lives<=0)StartCoroutine("LoadLoseScreen");
				else StartCoroutine("Respawn");
			}
		}
	}

	IEnumerator Respawn(){
		GameManagerScript.instance.GetComponent<FirstPersonController>().enabled=false;
		AudioSource.PlayClipAtPoint(alarm,transform.position);
		yield return new WaitForSeconds(alarm.length);
		GameManagerScript.instance.transform.position=GameManagerScript.instance.spawnPoint;
		GameManagerScript.instance.GetComponent<FirstPersonController>().enabled=true;
		isSeen=false;
		StopCoroutine("Respawn");
	}

	IEnumerator LoadLoseScreen(){
		GameObject.Find("Music Manager").GetComponent<AudioSource>().Stop();
		GameManagerScript.instance.GetComponent<FirstPersonController>().enabled=false;
		AudioSource.PlayClipAtPoint(loser,transform.position);
		yield return new WaitForSeconds(loser.length);
		LevelManagerScript.instance.LoadLevel ("Lose Screen");
		StopCoroutine("LoadLoseScreen");
	}
}