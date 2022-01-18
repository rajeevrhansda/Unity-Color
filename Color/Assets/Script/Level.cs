using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{

	#region Singleton class: Level

	public static Level Instance;

	void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
	}

	#endregion




	[SerializeField] Transform objectsParent;
	public int totalObjects;
	public int objectsInScene;


    private void Start()
    {
		CountObjects();

	}
	



	void CountObjects()
	{
		totalObjects = objectsParent.childCount;
		objectsInScene = totalObjects;
	}

	public void LoadNextLevel()
	{
		//SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void RestartLevel()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		
	}

	public void QuitGame()
	{
		Application.Quit();
	}
}
