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
    private int _round = 1;
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

    void StartRound(int round)
    {
        SpellList = new List<GameObject>();

        BeatControl.instance.SetSpeedSpell();
        int count = 0;
        _spellList.transform.position = LeftPos.position;
        foreach (GameObject sc in _spellCount)
        {
            sc.transform.localPosition = new Vector3(-(_spellDistance * count), 0, 0);
            count++;
        }

        _round = round;
        _started = true;

        NextTurn();
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
        
        if (_turn == 1) {
            p1.transform.localPosition = new Vector3(-(_spellDistance * 3), 0, 0);
        }
        else
        {
            _spellList.transform.position = pos.position;
            int count = 0;
            if (_player1Turn)
            {
                _bpm += 10;

                GameObject.Find("LeftPosition").GetComponent<BoxCollider>().enabled = false;
                GameObject.Find("RightPosition").GetComponent<BoxCollider>().enabled = true;
                foreach (GameObject sp in SpellList)
                {
                    sp.transform.localPosition = new Vector3(-(_spellDistance * count), 0, 0);
                    count++;
                }
                
            }
            else
            {
                GameObject.Find("LeftPosition").GetComponent<BoxCollider>().enabled = true;
                GameObject.Find("RightPosition").GetComponent<BoxCollider>().enabled = false;
                foreach (GameObject sp in SpellList)
                {
                    sp.transform.localPosition = new Vector3((_spellDistance * count), 0, 0);
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
                StartRound(1);
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
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    foreach (var sp in SpellList)
                    {
                        if (sp.GetComponent<Spell>()._isOnTrigger)
                        {
                            if (Input.GetKey(KeyCode.W))
                            {
                                check = sp.GetComponent<Spell>().CheckType(Spell.SpellType.UP);
                                if (check)
                                    P1Anim.SetTrigger("Up");
                            }
                            if (Input.GetKey(KeyCode.S))
                            {
                                check = sp.GetComponent<Spell>().CheckType(Spell.SpellType.DOWN);
                                if (check)
                                    P1Anim.SetTrigger("Down");
                            }
                            if (Input.GetKey(KeyCode.A))
                            {
                                check = sp.GetComponent<Spell>().CheckType(Spell.SpellType.LEFT);
                                if (check)
                                    P1Anim.SetTrigger("Left");
                            }
                            if (Input.GetKey(KeyCode.D))
                            {
                                check = sp.GetComponent<Spell>().CheckType(Spell.SpellType.RIGHT);
                                if (check)
                                    P1Anim.SetTrigger("Right");
                            }
                        }
                    }
                    if (!check)
                    {
                        P1Anim.SetTrigger("Lose");
                        P2Anim.SetTrigger("Win");
                        Debug.Log("PERDEUUUUU");
                    }
                }

                if (Input.GetKeyDown(KeyCode.KeypadEnter))
                {
                    foreach (var sp in SpellList)
                    {
                        if (sp.GetComponent<Spell>()._isOnTrigger)
                        {
                            if (Input.GetKey(KeyCode.UpArrow))
                            {
                                check = sp.GetComponent<Spell>().CheckType(Spell.SpellType.UP);
                                if (check)
                                    P2Anim.SetTrigger("Up");
                            }
                            if (Input.GetKey(KeyCode.DownArrow))
                            {
                                check = sp.GetComponent<Spell>().CheckType(Spell.SpellType.DOWN);
                                if (check)
                                    P2Anim.SetTrigger("Down");
                            }
                            if (Input.GetKey(KeyCode.LeftArrow))
                            {
                                check = sp.GetComponent<Spell>().CheckType(Spell.SpellType.LEFT);
                                if (check)
                                    P2Anim.SetTrigger("Right");
                            }
                            if (Input.GetKey(KeyCode.RightArrow))
                            {
                                check = sp.GetComponent<Spell>().CheckType(Spell.SpellType.RIGHT);
                                if (check)
                                    P2Anim.SetTrigger("Left");
                            }
                        }
                    }
                    if (!check)
                    {
                        P2Anim.SetTrigger("Lose");
                        P1Anim.SetTrigger("Win");
                        Debug.Log("PERDEUUUUU");
                    }
                }
            }
            
        }
    }
}
