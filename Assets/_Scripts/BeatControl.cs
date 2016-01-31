using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BeatControl : MonoBehaviour {

    public static BeatControl instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.
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
    public AudioClip _tambor;

    //Awake is always called before any Start functions
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        //Sets this to not be destroyed when reloading scene
        //DontDestroyOnLoad(gameObject);
    }

        // Use this for initialization
    void Start () {
		SpellPosInit = RightPosition.transform.position.x - TotenRight.transform.position.x ;
        HideToten();
        BeatStart();
	}
	
	// Update is called once per frame
	void Update () {
        if (GameManager.instance._waitingStart)
        {
            return;
        }

        if (GameManager.instance._started)
        {
            if (GameManager.instance._bpm != OldBpm)
                SetSpeedSpell();

            //dispara a batida
            if (Time.realtimeSinceStartup > NextBeat)
            {
                NextBeat = InitTime + (_bts * BeatCounter);
                BeatCounter++;
                Beat();
            }
        }
	
	}

	void Beat() {
		//toca a batida do tambor (efeito visual totem)
        TotenLeft.GetComponentInChildren<SpriteRenderer>().enabled = true;
        TotenRight.GetComponentInChildren<SpriteRenderer>().enabled = true;
        //Toca audio tambor (efeito sonoro)
        GameObject.Find("Tambor").GetComponent<AudioSource>().PlayOneShot(_tambor);

        float BlinkTime = SpellSize / GameManager.instance._spellVelocity * 1.2f;
		Invoke ("HideToten", BlinkTime);
	}

	void HideToten (){
		TotenLeft.GetComponentInChildren<SpriteRenderer>().enabled = false;
        TotenRight.GetComponentInChildren<SpriteRenderer>().enabled = false;
    }

	public void SetSpeedSpell(){
		OldBpm = GameManager.instance._bpm;
		_bts = (60f / GameManager.instance._bpm);
		GameManager.instance._spellVelocity = (TotensDistance / GameManager.instance._spellBetween) / _bts;
		GameManager.instance._spellDistance = GameManager.instance._spellVelocity * _bts;

		//posiciona o inicio dos spell
		float NewDist = GameManager.instance._spellDistance * (Mathf.Abs(SpellPosInit / GameManager.instance._spellDistance)+1) + SpellSize/2;
		RightPosition.transform.position = new Vector3 (TotenRight.transform.position.x + NewDist, TotenRight.transform.position.y, TotenRight.transform.position.z);
		LeftPosition.transform.position = new Vector3 (- TotenRight.transform.position.x - NewDist, TotenRight.transform.position.y, TotenRight.transform.position.z);
	}

    public void BeatStart()
    {
        InitTime = Time.realtimeSinceStartup;
        NextBeat = InitTime + (60f / GameManager.instance._bpm);
        BeatCounter = 1;
        SetSpeedSpell();
    }
}
