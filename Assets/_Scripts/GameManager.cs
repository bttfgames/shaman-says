using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

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
    private bool _started = false;
    public bool _turnOn = false;
    public float _spellDistance = 1.0f;
	public int _spellBetween = 3;
    public GameObject _spellList;
    public int _bpm = 0;
    public bool _player1Turn = false;

    public Animator P1Anim;
    public Animator P2Anim;
    private Animator AnimAtual;

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
        DontDestroyOnLoad(gameObject);

        SpellList = new List<GameObject>();

        StartRound(1);
    }

    void StartRound(int round)
    {
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
            p1.transform.Translate(Vector3.left * (_spellDistance * 3));
        }
        else
        {
            _spellList.transform.position = pos.position;
            int count = 0;
            if (_player1Turn)
            {
                
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
        Debug.Log("Turno: " + _turn);

    }

    void Update()
    {
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

            if (Input.GetKeyDown(KeyCode.Space))
            {
                foreach (var sp in SpellList)
                {
                    if (sp.GetComponent<Spell>()._isOnTrigger)
                    {
                        if (Input.GetKey(KeyCode.W))
                        {
                            check = sp.GetComponent<Spell>().CheckType(Spell.SpellType.UP);
                            P1Anim.SetTrigger("Up");
                        }
                        if (Input.GetKey(KeyCode.S))
                        {
                            check = sp.GetComponent<Spell>().CheckType(Spell.SpellType.DOWN);
                            P1Anim.SetTrigger("Down");
                        }
                        if (Input.GetKey(KeyCode.A))
                        {
                            check = sp.GetComponent<Spell>().CheckType(Spell.SpellType.LEFT);
                            P1Anim.SetTrigger("Left");
                        }
                        if (Input.GetKey(KeyCode.D))
                        {
                            check = sp.GetComponent<Spell>().CheckType(Spell.SpellType.RIGHT);
                            P1Anim.SetTrigger("Right");
                        }
                    }
                }
                if (!check)
                {
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
                            P2Anim.SetTrigger("Up");
                        }
                        if (Input.GetKey(KeyCode.DownArrow))
                        {
                            check = sp.GetComponent<Spell>().CheckType(Spell.SpellType.DOWN);
                            P2Anim.SetTrigger("Down");
                        }
                        if (Input.GetKey(KeyCode.LeftArrow))
                        {
                            check = sp.GetComponent<Spell>().CheckType(Spell.SpellType.LEFT);
                            P2Anim.SetTrigger("Left");
                        }
                        if (Input.GetKey(KeyCode.RightArrow))
                        {
                            check = sp.GetComponent<Spell>().CheckType(Spell.SpellType.RIGHT);
                            P2Anim.SetTrigger("Right");
                        }
                    }
                }
                if (!check)
                {
                    Debug.Log("PERDEUUUUU");
                }
            }
            
        }
    }
}
