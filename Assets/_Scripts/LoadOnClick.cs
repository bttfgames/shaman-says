using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadOnClick : MonoBehaviour {

	public GameObject loadingImage;

	public void LoadScene(string level)
	{
		if(loadingImage != null)
			loadingImage.SetActive (true);
		string _gameType = "Versus";
		//Application.LoadLevel (level);

		if (level == "MainVersus") {
			_gameType = "Versus";
			level = "Main";
		} else if (level == "MainChalenge") {
			_gameType = "Chalenge";
			level = "Main";
		}


		GameObject.Find ("InputManager").GetComponent<InputManager> ()._gameType = _gameType;

		SceneManager.LoadScene (level);
	}

	public void ExitApplication()
	{
		Application.Quit();
	}
		
}