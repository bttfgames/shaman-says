using UnityEngine;
using System.Collections;

public class ShotCheck : MonoBehaviour {

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
        }
    }

}
