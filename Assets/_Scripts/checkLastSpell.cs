using UnityEngine;
using System.Collections;

public class checkLastSpell : MonoBehaviour {

	void OnTriggerEnter(Collider other)
    {
        if (other.tag == "SpellCount")
        {
            Debug.Log("Count spell");
            other.gameObject.transform.parent.gameObject.SetActive(false);
        }

        if ( other.tag == "Spell" && other.GetComponent<Spell>()._last)
        {
            other.GetComponent<Spell>()._last = false;
            GameManager.instance._turnOn = false;
            GameManager.instance.NextTurn();
        }
        
    }
}
