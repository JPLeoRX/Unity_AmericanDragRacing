using UnityEngine;
using System;
using UnityEngine.UI;

[Serializable]
public class MenuMain 
{
	private LevelMenu level;
	private Camera cam;
	private Button buttonRace;
	private Button buttonPractice;
	private Button buttonGarage;
	private Button buttonTuningShop;
	private Button buttonCarDealership;
	private Button buttonAbout;
	private Button buttonExit;

	public MenuMain (LevelMenu level, Camera memuMainCamera, Button buttonRace, Button buttonPractice, Button buttonGarage, Button buttonTuningShop, Button buttonCarDealership, Button buttonAbout, Button buttonExit)
	{
		this.level = level;
		this.cam = memuMainCamera;
		this.buttonRace = buttonRace;
		this.buttonPractice = buttonPractice;
		this.buttonGarage = buttonGarage;
		this.buttonTuningShop = buttonTuningShop;
		this.buttonCarDealership = buttonCarDealership;
		this.buttonAbout = buttonAbout;
		this.buttonExit = buttonExit;
	}

	public void InitializeListeners () {
		buttonGarage.onClick.AddListener(() => level.GoToMenuGarage());
	}

	public void Hide() {
		buttonRace.enabled = false;
		buttonPractice.enabled = false;
		buttonGarage.enabled = false;
		buttonTuningShop.enabled = false;
		buttonCarDealership.enabled = false;
		buttonAbout.enabled = false;
		buttonExit.enabled = false;

		cam.enabled = false;
	}

	public void Show() {
		buttonRace.enabled = true;
		buttonPractice.enabled = true;
		buttonGarage.enabled = true;
		buttonTuningShop.enabled = true;
		buttonCarDealership.enabled = true;
		buttonAbout.enabled = true;
		buttonExit.enabled = true;

		cam.enabled = true;
	}
}
