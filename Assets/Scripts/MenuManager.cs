using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//*accessing scene management
//using UnityEditor;//*for quitting playmode (when pressing "QUIT" buttons)

public class MenuManager : MonoBehaviour 
{
	//public GameObject winPanel;
	//public GameObject deathPanel;
	//public GameObject pausePanel;
	//public GameObject buttons;

	public GameObject swordImage_01;
	public GameObject swordImage_02;
	public GameObject swordImage_03;

	void Start ()
	{
		swordImage_01 = GameObject.FindGameObjectWithTag ("MenuSword_01");
		swordImage_02 = GameObject.FindGameObjectWithTag ("MenuSword_02");
		swordImage_03 = GameObject.FindGameObjectWithTag ("MenuSword_03");

		swordImage_01.SetActive (false);
		swordImage_02.SetActive (false);
		swordImage_03.SetActive (false);
		//winPanel.SetActive (false);
		//deathPanel.SetActive (false);
		//pausePanel.SetActive (false);
		//buttons.SetActive (false);
	}

	public void StartGame()
	{
		SceneManager.LoadScene ("3.MainScene");
	}

	public void MainMenu()
	{
		SceneManager.LoadScene ("1.Menu");
	}

	public void Quit()
	{
		Application.Quit ();
	}

	public void Instructions()
	{
		SceneManager.LoadScene ("2.Instructions");
	}

	public void Back()
	{
		SceneManager.LoadScene ("1.Menu");
	}

	public void RetryMainScene()
	{
		SceneManager.LoadScene ("3.MainScene");
	}

	public void RetryBossScene()
	{
		SceneManager.LoadScene ("4.BossScene");
	}

	public void MouseIsOver_01()
	{
		swordImage_01.SetActive (true);
	}
	public void MouseIsOver_02()
	{
		swordImage_02.SetActive (true);
	}
	public void MouseIsOver_03()
	{
		swordImage_03.SetActive (true);
	}

	public void MouseIsNOTOver_01()
	{
		swordImage_01.SetActive (false);
	}
	public void MouseIsNOTOver_02()
	{
		swordImage_02.SetActive (false);
	}
	public void MouseIsNOTOver_03()
	{
		swordImage_03.SetActive (false);
	}
}
