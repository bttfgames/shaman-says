using UnityEngine;
using System.Collections;

public class CountPlaySound : MonoBehaviour {

	void OnTriggerEnter(Collider other)
    {
        if(other.tag == "TriggerLeft")
        {
            GetComponent<AudioSource>().Play();
        }
    }
}
