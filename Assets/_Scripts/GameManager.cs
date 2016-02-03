using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.
    public bool DrawGizmos = true;
    public GameObject _spell;
    public Transform LeftPos;
    public Transform RightPos;
    private List<GameObject> SpellList;
    public float _spellVelocity = 1.0f;
    private int _round = 0;
    private int _turn = 0;
    public bool _started = false;
    public bool _turnOn = false;
    public bool _startDetect = false;
    public float _spellDistance = 1.0f;
	public int _spellBetween = 3;
    public GameObject _spellList;
    public int _bpm = 0;
    public bool _player1Turn = false;
    public GameObject[] _spellCount;

    public Animator P1Anim;
    public Animator P2Anim;
    private Animator AnimAtual;
    public bool _waitingStart = true;
    public GameObject P1Grade;
    public GameObject P2Grade;
    private int _p1Life = 5;
    private int _p2Life = 5;
    public AudioClip _loose;
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

    public void FinishRound(bool p1Win)
    {
        if (p1Win)
        {
            _p2Life -= 2;
            P2Grade.GetComponent<GUIMeatGrade>().IncreaseMeatGrade();
            P2Grade.GetComponent<GUIMeatGrade>().IncreaseMeatGrade();
            Debug.Log("P1 win");
            P2Anim.SetTrigger("Lose");
            P1Anim.SetTrigger("Win");
        }
        else
        {
            _p1Life -= 2;
            P1Grade.GetComponent<GUIMeatGrade>().IncreaseMeatGrade();
            P1Grade.GetComponent<GUIMeatGrade>().IncreaseMeatGrade();
            Debug.Log("P2 win");
            P1Anim.SetTrigger("Lose");
            P2Anim.SetTrigger("Win");
        }

        GameObject.Find("LoseAudio").GetComponent<AudioSource>().PlayOneShot(_loose);
        int minLife = Mathf.Min(_p1Life, _p2Life);

        GameObject.Find("Fire").transform.position = new Vector3(0, 0.4f - (minLife * 0.4f), 5);
        
        _started = false;
        _turnOn = false;
        _spellList.SetActive(false);
        //_bpm = (int)(_bpm * 0.8f);

        _bpm = Mathf.FloorToInt((_bpm / 10) * 0.8f) * 10;
        
        if (_bpm < 60) _bpm = 60;

        foreach(Transform child in _spellList.transform)
        {
            Destroy(child.gameObject);
        }

        if (minLife <= 0)
        {

            if(_p1Life > _p2Life)
            {
                GameObject.Find("GameOver").GetComponent<GameOverPersist>()._p1Win = true;
            }
            else
            {
                GameObject.Find("GameOver").GetComponent<GameOverPersist>()._p1Win = false;
            }

            Invoke("GameOver", 5.0f);
            return;
        }

        Invoke("StartRound", 2.0f);
    }

    void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    void StartRound()
    {
        _round++;
        SpellList = new List<GameObject>();
        _spellList.SetActive(true);
        BeatControl.instance.SetSpeedSpell();
        int count = 0;

        if (_round % 2 != 0 )//inicia com o p1
        {
            _spellList.transform.position = LeftPos.position;
            _player1Turn = false;
        }
        else//p2
        {
            _spellList.transform.position = RightPos.position;
            _player1Turn = true; //Inicio oposto pois vai trocar no NextTurn
        }
        if (_round == 1)
        {
            foreach (GameObject sc in _spellCount)
            {
                sc.transform.localPosition = new Vector3(-(_spellDistance * count), 0, 0);
                count++;
            }
        }

        //P1Anim.SetTrigger("Idle_back");
        //P2Anim.SetTrigger("Idle_back");
        _started = true;
        _turn = 0;
        Debug.Log("Iniciando Turno");
        Invoke("NextTurn", 1.0f);
        //NextTurn();
    }

    public void NextTurn()
    {
        _turn++;
        //_spellVelocity += 0.5f;
        GameObject p1;
        Transform pos;
        _player1Turn = !_player1Turn;
        pos = (_player1Turn) ? LeftPos : RightPos;
        AnimAtual = (_player1Turn) ? P1Anim : P2Anim;
        p1 = Instantiate(_spell, pos.position, Quaternion.identity) as GameObject;
        p1.transform.SetParent(_spellList.transform);
        p1.GetComponent<Spell>()._last = true;
        SpellList.Add(p1);
        
        if (_turn == 1 && _round == 1) {
            p1.transform.localPosition = new Vector3(-(_spellDistance * 3), 0, 0);
        }
        else
        {
            _spellList.transform.position = pos.position;
            int count = 0;
            if (_player1Turn)
            {
                if( _round % 2 != 0 )
                    _bpm += 10;

                GameObject.Find("LeftPosition").GetComponent<BoxCollider>().enabled = false;
                GameObject.Find("RightPosition").GetComponent<BoxCollider>().enabled = true;
                foreach (GameObject sp in SpellList)
                {
                    sp.transform.localPosition = new Vector3(-(_spellDistance * count), 0, 0);
                    sp.GetComponent<Spell>()._p1Check = false;
                    sp.GetComponent<Spell>()._p2Check = false;
                    count++;
                }
                
            }
            else
            {
                if (_round % 2 == 0 && _turn > 2)
                    _bpm += 10;

                GameObject.Find("LeftPosition").GetComponent<BoxCollider>().enabled = true;
                GameObject.Find("RightPosition").GetComponent<BoxCollider>().enabled = false;
                foreach (GameObject sp in SpellList)
                {
                    sp.transform.localPosition = new Vector3((_spellDistance * count), 0, 0);
                    sp.GetComponent<Spell>()._p1Check = false;
                    sp.GetComponent<Spell>()._p2Check = false;
                    count++;
                }
            }

        }
        _turnOn = true;

        //reinicia o ritmo
        BeatControl.instance.BeatStart();

    }
    
    void Update()
    {
        if(_waitingStart)
        {
            if (Input.anyKeyDown)
            {

                _waitingStart = false;
                GameObject.Find("TextStart").SetActive(false);
                StartRound();
            }

                return;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _started = false;
            _turnOn = false;

            SceneManager.LoadScene("Menu");
            
        }

        if (_started && _turnOn)
        {
            if (_player1Turn)
            {
                _spellList.transform.Translate(Vector3.right * (_spellVelocity * Time.deltaTime));
            }
            else
            {
                _spellList.transform.Translate(Vector3.left * (_spellVelocity * Time.deltaTime));
            }

            bool check = false;

            if (_startDetect)
            {
                //Check de input do player 1
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown(InputManager.instance._player1Prefix + "_Fire"))
                {
                    foreach (var sp in SpellList)
                    {
                        if (sp.GetComponent<Spell>()._isOnTriggerLeft)
                        {
                            if (Input.GetKey(KeyCode.W) || Input.GetAxis(InputManager.instance._player1Prefix + "_Y") > 0.8f)
                            {
                                check = sp.GetComponent<Spell>().CheckType(Spell.SpellType.UP, true);
                                if (check)
                                {
                                    P1Anim.SetTrigger("Up");
                                }
                            }
                            if (Input.GetKey(KeyCode.S) || Input.GetAxis(InputManager.instance._player1Prefix + "_Y") < -0.8f)
                            {
                                check = sp.GetComponent<Spell>().CheckType(Spell.SpellType.DOWN, true);
                                if (check)
                                    P1Anim.SetTrigger("Down");
                            }
                            if (Input.GetKey(KeyCode.A) || Input.GetAxis(InputManager.instance._player1Prefix + "_X") < -0.8f)
                            {
                                check = sp.GetComponent<Spell>().CheckType(Spell.SpellType.LEFT, true);
                                if (check)
                                    P1Anim.SetTrigger("Left");
                            }
                            if (Input.GetKey(KeyCode.D) || Input.GetAxis(InputManager.instance._player1Prefix + "_X") > 0.8f )
                            {
                                check = sp.GetComponent<Spell>().CheckType(Spell.SpellType.RIGHT, true);
                                if (check)
                                    P1Anim.SetTrigger("Right");
                            }
                        }
                    }
                    if (!check)
                    {
                        FinishRound(false);
                    }
                }

                if (Input.GetKeyDown(KeyCode.Comma) || Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetButtonDown(InputManager.instance._player2Prefix + "_Fire"))
                {
                    foreach (var sp in SpellList)
                    {
                        if (sp.GetComponent<Spell>()._isOnTriggerRight)
                        {
                            if (Input.GetKey(KeyCode.UpArrow) || Input.GetAxis(InputManager.instance._player2Prefix + "_Y") > 0.8f)
                            {
                                check = sp.GetComponent<Spell>().CheckType(Spell.SpellType.UP, false);
                                if (check)
                                    P2Anim.SetTrigger("Up");
                            }
                            if (Input.GetKey(KeyCode.DownArrow) || Input.GetAxis(InputManager.instance._player2Prefix + "_Y") < -0.8f)
                            {
                                check = sp.GetComponent<Spell>().CheckType(Spell.SpellType.DOWN, false);
                                if (check)
                                    P2Anim.SetTrigger("Down");
                            }
                            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetAxis(InputManager.instance._player2Prefix + "_X") < -0.8f)
                            {
                                check = sp.GetComponent<Spell>().CheckType(Spell.SpellType.LEFT, false);
                                if (check)
                                    P2Anim.SetTrigger("Right");//Invertido por causa da animação
                            }
                            if (Input.GetKey(KeyCode.RightArrow) || Input.GetAxis(InputManager.instance._player2Prefix + "_X") > 0.8f)
                            {
                                check = sp.GetComponent<Spell>().CheckType(Spell.SpellType.RIGHT, false);
                                if (check)
                                    P2Anim.SetTrigger("Left");//Invertido por causa da animação
                            }
                        }
                    }
                    if (!check)
                    {
                        FinishRound(true);
                        
                    }
                }
            }
            
        }
    }
}
