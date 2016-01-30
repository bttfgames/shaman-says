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
    public List<GameObject> SpellList;
    public float SpellVelocity = 1.0f;
    private float _timeToStart = 3.0f;
    private int round = 1;
    private int turn = 1;
    private bool _started = false;
    public float _spellDistance = 1.0f;
    private GameObject _spellList;
    public int _bpm = 0;
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
    }

    IEnumerator StartRound()
    {
        while(_timeToStart > 0.0f)
        {
            GameObject.Find("TextInit").GetComponent<Text>().text = _timeToStart.ToString();
            _timeToStart -= 1.0f;
            yield return new WaitForSeconds(1.0f);
        }
        _started = true;
    }

    void Update()
    {
        if (_started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GameObject p1;
                p1 = Instantiate(_spell, LeftPos.position, Quaternion.identity) as GameObject;
                SpellList.Add(p1);

            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                foreach (GameObject spell in SpellList)
                {
                    if (spell.GetComponent<Spell>()._isOnTrigger)
                    {
                        spell.GetComponent<Spell>().SetType(Spell.SpellType.UP);
                    }
                }
            }
        }
    }
}
