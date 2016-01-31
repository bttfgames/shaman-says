using UnityEngine;
using System.Collections;

public class PlaySound : MonoBehaviour {

	public AudioClip audioClip;

	public void playSound(){
		GameObject.Find ("Main Camera").GetComponent<AudioSource> ().PlayOneShot (audioClip);
	}
}
