using UnityEngine;
using System.Collections;

public class GameOverPersist : MonoBehaviour {

    public bool _p1Win;
    public int _p1Points;
    public GameManager.gameType _gameType;

	// Use this for initialization
	void Awake () {

        DontDestroyOnLoad(gameObject);
	}
	
}
