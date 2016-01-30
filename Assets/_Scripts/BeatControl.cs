﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BeatControl : MonoBehaviour {

	public GameObject TotenRight;
	public GameObject TotenLeft;
	public float TotensDistance;
	public float SpellSize;
	public GameObject LeftPosition;
	public GameObject RightPosition;

	private float InitTime;
	private float NextBeat;
	private int BeatCounter = 1;
	private float OldBpm;
	private float _bts;
	private float SpellPosInit;

	// Use this for initialization
	void Start () {
		InitTime = Time.realtimeSinceStartup;
		NextBeat = InitTime + (60f / GameManager.instance._bpm);
		HideToten ();
		SpellPosInit = RightPosition.transform.position.x - TotenRight.transform.position.x ;
		SetSpeedSpell ();
	}
	
	// Update is called once per frame
	void Update () {

		if (GameManager.instance._bpm != OldBpm)
			SetSpeedSpell ();

		//dispara a batida
		if (Time.realtimeSinceStartup > NextBeat) {
			NextBeat = InitTime + (_bts * BeatCounter);
			BeatCounter++;
			Beat ();
		}
	
	}

	void Beat() {
		//toca a batida do tambor
		TotenLeft.SetActive(true);
		TotenRight.SetActive(true);
		float BlinkTime = SpellSize / GameManager.instance._spellVelocity;
		Invoke ("HideToten", BlinkTime);
	}

	void HideToten (){
		TotenLeft.SetActive(false);
		TotenRight.SetActive(false);
	}

	void SetSpeedSpell(){
		OldBpm = GameManager.instance._bpm;
		_bts = (60f / GameManager.instance._bpm);
		GameManager.instance._spellVelocity = (TotensDistance / GameManager.instance._spellBetween) / _bts;
		GameManager.instance._spellDistance = GameManager.instance._spellVelocity * _bts;

		//posiciona o inicio dos spell
		float NewDist = GameManager.instance._spellDistance * (Mathf.Abs(SpellPosInit / GameManager.instance._spellDistance)+1);
		RightPosition.transform.position = new Vector3 (TotenRight.transform.position.x + NewDist, TotenRight.transform.position.y, TotenRight.transform.position.z);
		LeftPosition.transform.position = new Vector3 (- TotenRight.transform.position.x - NewDist, TotenRight.transform.position.y, TotenRight.transform.position.z);
	}
}
