using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadOnClick : MonoBehaviour {

	public GameObject loadingImage;

	public void LoadScene(string level)
	{
		if(loadingImage != null)
			loadingImage.SetActive (true);
		
		//Application.LoadLevel (level);
		SceneManager.LoadScene (level);
	}

	public void ExitApplication()
	{
		Application.Quit();
	}
		
}