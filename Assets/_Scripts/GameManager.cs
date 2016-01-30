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
    public GameObject _spellList;
    public int _bpm = 0;
    public bool _player1Turn = false;
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
        _spellVelocity += 0.5f;
        GameObject p1;
        Transform pos;
        _player1Turn = !_player1Turn;
        pos = (_player1Turn) ? LeftPos : RightPos;
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
                    Debug.Log(_spellDistance * count);
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
                    Debug.Log(_spellDistance * count);
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

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Apertou espaço");
                foreach (var sp in SpellList)
                {
                    if (sp.GetComponent<Spell>()._isOnTrigger)
                    {
                        Debug.Log("Está no trigger");
                        if (Input.GetKey(KeyCode.UpArrow))
                        {
                            sp.GetComponent<Spell>().SetType(Spell.SpellType.UP);
                        }
                        if (Input.GetKey(KeyCode.DownArrow))
                        {
                            sp.GetComponent<Spell>().SetType(Spell.SpellType.DOWN);
                        }
                        if (Input.GetKey(KeyCode.LeftArrow))
                        {
                            sp.GetComponent<Spell>().SetType(Spell.SpellType.LEFT);
                        }
                        if (Input.GetKey(KeyCode.RightArrow))
                        {
                            sp.GetComponent<Spell>().SetType(Spell.SpellType.RIGHT);
                        }
                    }
                }
            }
        }
    }
}
