using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class controlsShowJoystick : MonoBehaviour {

    public Text P1;
    public Text P2;

	// Use this for initialization
	void Start () {
        if (InputManager.instance.getPlayerDescription("P1") != "")
        {
            P1.text = "Player 1 (Left)\n\nJoystick #1:\n" + InputManager.instance.getPlayerDescription("P1") + "\nUp, Left, Down, Right\nAny Fire Button (Fire)\n\nKeyboard:\nW, A, S, D\nSpace (Fire)";
        }
        else if (InputManager.instance.getPlayerDescription("P2") != "")
        {
            P2.text = "Player 2 (Right)\n\nJoystick #2:\n" + InputManager.instance.getPlayerDescription("P2") + "\nUp, Left, Down, Right\nAny Fire Button (Fire)\n\nKeyboard:\nUp, Left, Down, Right\nEnter (Fire)";
        }


	}
	
}
