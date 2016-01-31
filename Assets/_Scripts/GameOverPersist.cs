using UnityEngine;
using System.Collections;

public class GameOverPersist : MonoBehaviour {

    public bool _p1Win;

	// Use this for initialization
	void Awake () {

        DontDestroyOnLoad(gameObject);
	}
	
}
