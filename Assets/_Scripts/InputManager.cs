using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {
    public static InputManager instance = null;
    public string _player1Prefix;
    public string _player2Prefix;
    public bool _debug = false;

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
        for (int i = 1; i <= Input.GetJoystickNames().Length; i++)
        {
            if (Input.GetJoystickNames()[i-1] != "") {
                if(_player1Prefix == "")
                {
                    _player1Prefix = "Joy" + i;
                }
                else if(_player2Prefix == "")
                {
                    _player2Prefix = "Joy" + i;
                }
            }
        }
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
    }
}
