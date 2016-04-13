using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOverLoad : MonoBehaviour {

    public GameObject _p1Win;
    public GameObject _p1Lose;
    public GameObject _p2Win;
    public GameObject _p2Lose;

    // Use this for initialization
    void Start () {
        switch (GameObject.Find("GameOver").GetComponent<GameOverPersist>()._gameType)
        {
            case GameManager.gameType.Versus:
                GameObject.Find("PointsText").SetActive(false);
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
                break;
            case GameManager.gameType.Challenge:
                GameObject.Find("PointsText").GetComponent<Text>().text = "RITUAL FINISHED WITH\n "+ GameObject.Find("GameOver").GetComponent<GameOverPersist>()._p1Points +" SPELLS";


                _p1Lose.SetActive(false);
                _p2Lose.SetActive(false);
                break;
            default:
                break;
        }
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
