using UnityEngine;
using System.Collections;

public class GameOverLoad : MonoBehaviour {

    public GameObject _p1Win;
    public GameObject _p1Lose;
    public GameObject _p2Win;
    public GameObject _p2Lose;

    // Use this for initialization
    void Start () {
        if (GameObject.Find("GameOver").GetComponent<GameOverPersist>()._p1Win)
        {
            _p1Lose.SetActive(false);
            _p2Win.SetActive(false);
        }
        else
        {
            _p1Win.SetActive(false);
            _p2Lose.SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
