using UnityEngine;
using System;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour 
{
	private MenuMain menuMain;
	public Camera menuMainCamera;
	public Button menuMainButtonRace;
	public Button menuMainButtonPractice;
	public Button menuMainButtonGarage;
	public Button menuMainButtonTuningShop;
	public Button menuMainButtonCarDealership;
	public Button menuMainButtonAbout;
	public Button menuMainButtonExit;



	// Use this for initialization
	void Start () {
		this.menuMain = new MenuMain(this, menuMainCamera, menuMainButtonRace, menuMainButtonPractice, menuMainButtonGarage, menuMainButtonTuningShop, menuMainButtonCarDealership, menuMainButtonAbout, menuMainButtonExit);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void GoToMenuGarage() {
		this.menuMain.Hide();
	}

	public void GoToMenuMain() {
		this.menuMain.Show();
	}
}
