using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour {

	public void ChangeToStory(){
		SceneManager.LoadScene ("Story");
	}
	public void ChangeToMain(){
		SceneManager.LoadScene ("main (1)");
	}
	public void ChangeToStartMenu(){
		SceneManager.LoadScene ("StartMenu");
	}
}
