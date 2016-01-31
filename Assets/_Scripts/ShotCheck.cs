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
            if( _isP1)
            {
                if(!other.GetComponent<Spell>()._p1Check)
                {
                    GameManager.instance.FinishRound(false);
                }
            }
            else
            {
                if (!other.GetComponent<Spell>()._p2Check)
                {
                    GameManager.instance.FinishRound(true);
                }
            }
        }
    }

}
