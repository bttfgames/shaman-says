using UnityEngine;
using System.Collections;

public class ShotCheck : MonoBehaviour {

    public bool _isP1 = false;

	void OnTriggerStay(Collider other)
    {
        if( other.tag == "Spell")
        {
            other.GetComponent<Spell>()._isOnTrigger = true;
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
            other.GetComponent<Spell>()._isOnTrigger = false;
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
