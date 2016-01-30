using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BeatControl : MonoBehaviour {

	public MeshRenderer TotenRight;
	public MeshRenderer TotenLeft;
	public float TotensDistance;
	public float SpellBetween;
	public float SpellSize;

	public float SpellSpeed;

	private float InitTime;
	private float NextBeat;
	private int BeatCounter = 1;

	// Use this for initialization
	void Start () {
		InitTime = Time.realtimeSinceStartup;
		NextBeat = InitTime + (60f / GameManager.instance._bpm);
		TotenLeft.enabled = false;
		TotenRight.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {

		float _bts = (60f / GameManager.instance._bpm);
		SpellSpeed = (TotensDistance / SpellBetween) / _bts;

		//dispara a batida
		if (Time.realtimeSinceStartup > NextBeat) {
			NextBeat = InitTime + (_bts * BeatCounter);
			BeatCounter++;
			Beat ();
		}
	
	}

	void Beat() {
		//toca a batida do tambor
		TotenLeft.enabled = true;
		TotenRight.enabled = true;
		float BlinkTime = SpellSize / SpellSpeed;
		Invoke ("HideToten", BlinkTime);
		Debug.Log (BlinkTime);
	}

	void HideToten (){
		TotenLeft.enabled = false;
		TotenRight.enabled = false;
	}
}
