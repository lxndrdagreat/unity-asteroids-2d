using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {

	public void StartGame() {
        SceneManager.LoadScene(1);
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(1));
	}

	public void QuitGame() {
		// Note: this is ignored in the editor.
		Application.Quit ();
	}
}
