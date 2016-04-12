using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {
    public static InputManager instance = null;
    public string _player1Prefix;
    public string _player2Prefix;
    public bool _debug = false;
    private string _player1Description;
    private string _player2Description;

	[HideInInspector]
	public string _gameType = "Versus";
    [HideInInspector]
    public bool _player1Fire, _player1Up, _player1Down, _player1Left, _player1Right, _player1Played;
    [HideInInspector]
    public bool _player2Fire, _player2Up, _player2Down, _player2Left, _player2Right, _player2Played;


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
    }

    // Use this for initialization
    void Start () {
        for (int i = 1; i <= Input.GetJoystickNames().Length; i++)
        {
            if (Input.GetJoystickNames()[i-1] != "") {
                if(_player1Prefix == "")
                {
                    _player1Prefix = "Joy" + i;
                    _player1Description = Input.GetJoystickNames()[i - 1];
                }
                else if(_player2Prefix == "")
                {
                    _player2Prefix = "Joy" + i;
                    _player2Description = Input.GetJoystickNames()[i - 1];
                }
            }
        }
        reset(true);
        reset(false);

    }
	
	// Update is called once per frame
	void Update () {
        if (_debug)
        {
            int i = 1;
            while (i < 4)
            {
                if (Mathf.Abs( Input.GetAxis("Joy" + i + "_X") ) == 1.0f)
                    Debug.Log(Input.GetJoystickNames()[i - 1] + " X moved. Joy:" + i);

                if (Mathf.Abs( Input.GetAxis("Joy" + i + "_Y") ) == 1.0f)
                    Debug.Log(Input.GetJoystickNames()[i - 1] + " Y moved. Joy: " + i);

                if (Input.GetAxis("Joy" + i + "_Fire") != 0.0f)
                    Debug.Log(Input.GetJoystickNames()[i - 1] + " button X pressed");

                i++;
            }
        }


        //Controle do input do player 1
        if ((Input.GetKey(KeyCode.Space) || Input.GetButton(_player1Prefix + "_Fire")) && !_player1Fire)
        {
            _player1Fire = true;
        }

        if ((Input.GetKey(KeyCode.W) || Input.GetAxis(_player1Prefix + "_Y") > 0.8f) && !_player1Up)
        {
            _player1Up = true;
        }

        if ((Input.GetKey(KeyCode.S) || Input.GetAxis(_player1Prefix + "_Y") < -0.8f) && !_player1Down)
        {
            _player1Down = true;
        }

        if ((Input.GetKey(KeyCode.A) || Input.GetAxis(_player1Prefix + "_X") < -0.8f) && !_player1Left)
        {
            _player1Left = true;
        }
        if ((Input.GetKey(KeyCode.D) || Input.GetAxis(_player1Prefix + "_X") > 0.8f) && !_player1Right)
        {
            _player1Right = true;
        }


        //Controle do input do player 2
        if ((Input.GetKeyDown(KeyCode.Comma) || Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetButtonDown(_player2Prefix + "_Fire")) && !_player2Fire)
        {
            _player2Fire = true;
        }

        if ((Input.GetKey(KeyCode.UpArrow) || Input.GetAxis(_player2Prefix + "_Y") > 0.8f) && !_player2Up)
        {
            _player2Up = true;
        }

        if ((Input.GetKey(KeyCode.DownArrow) || Input.GetAxis(_player2Prefix + "_Y") < -0.8f) && !_player2Down)
        {
            _player2Down = true;
        }

        if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetAxis(_player2Prefix + "_X") < -0.8f) && !_player2Left)
        {
            _player2Left = true;
        }
        if ((Input.GetKey(KeyCode.RightArrow) || Input.GetAxis(_player2Prefix + "_X") > 0.8f) && !_player2Right)
        {
            _player2Right = true;
        }
    }

    public void reset(bool p1)
    {
        if (p1)
        {
            //Debug.Log("Reset player1");
            _player1Fire = false;
            _player1Up = false;
            _player1Down = false;
            _player1Left = false;
            _player1Right = false;
            _player1Played = false;
        }
        else
        {
            //Debug.Log("Reset player2");
            _player2Fire = false;
            _player2Up = false;
            _player2Down = false;
            _player2Left = false;
            _player2Right = false;
            _player2Played = false;
        }
    }

    public string getPlayerDescription(string player)
    {
        if (player == "P1")
        {
            return _player1Description;
        }
        else if( player == "P2")
        {
            return _player2Description;
        }
        return "";
    }
}
