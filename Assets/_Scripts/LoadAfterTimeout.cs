using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadAfterTimeout : MonoBehaviour {

	public string LevelToLoad;
	public int SecondsToWait;

	// Use this for initialization
	void Awake () {
		Invoke ("LoadLevel", SecondsToWait);
	}

	private void LoadLevel()
	{
		//Application.LoadLevel (LevelToLoad);
		SceneManager.LoadScene (LevelToLoad);
	}
}
