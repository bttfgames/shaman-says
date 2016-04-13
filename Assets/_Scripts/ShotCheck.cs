using UnityEngine;
using System.Collections;

public class ShotCheck : MonoBehaviour {

    public bool _isP1 = false;
	enum TriggerSide{Left, Right};

	void OnTriggerStay(Collider other)
    {
        if( other.tag == "Spell")
        {
			if (_isP1) {
				other.GetComponent<Spell> ()._isOnTriggerLeft = true;
				other.GetComponent<Spell> ()._isOnTriggerRight = false;
			} else {
				other.GetComponent<Spell> ()._isOnTriggerLeft = false;
				other.GetComponent<Spell> ()._isOnTriggerRight = true;
			}
        }

        if (other.tag == "SpellCount")
        {
            GameManager.instance._startDetect = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Spell")
        {
            other.GetComponent<Spell>()._isOnTriggerLeft = false;
			other.GetComponent<Spell>()._isOnTriggerRight = false;

            if (GameManager.instance._stateGameType == GameManager.gameType.Challenge)
            {
                if (!other.GetComponent<Spell>()._p1Check)
                {
                    GameManager.instance.FinishGameChallenge();
                }
                InputManager.instance.reset(true);
            }
            else
            {

                if (_isP1)
                {
                    if (!other.GetComponent<Spell>()._p1Check)
                    {
                        GameManager.instance.FinishRound(false);
                    }
                    InputManager.instance.reset(true);
                }
                else
                {
                    if (!other.GetComponent<Spell>()._p2Check)
                    {
                        GameManager.instance.FinishRound(true);
                    }
                    InputManager.instance.reset(false);
                }
            }
        }
    }

}
