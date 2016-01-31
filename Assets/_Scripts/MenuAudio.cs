using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class MenuAudio : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler {

	public AudioClip MenuHover;
	public AudioClip MenuSelect;

	public virtual void OnPointerEnter( PointerEventData ped )
	{
		Debug.Log ("Hover Menu");
		GameObject.Find ("Main Camera").GetComponent<AudioSource> ().PlayOneShot (MenuHover);
	}

	public virtual void OnPointerDown( PointerEventData ped )
	{
		Debug.Log ("Enter Menu");
		GameObject.Find ("Main Camera").GetComponent<AudioSource> ().PlayOneShot (MenuSelect);
	}
}

