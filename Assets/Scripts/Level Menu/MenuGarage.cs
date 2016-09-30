using UnityEngine;
using System;
using UnityEngine.UI;

[Serializable]
public class MenuGarage 
{
	private LevelMenu level;
	private Camera cam;
	private Button buttonNextCar;
	private Button buttonPreviousCar;

	public MenuGarage (LevelMenu level, Camera cam, Button buttonNextCar, Button buttonPreviousCar)
	{
		this.level = level;
		this.cam = cam;
		this.buttonNextCar = buttonNextCar;
		this.buttonPreviousCar = buttonPreviousCar;
	}
	

	public void Hide() {
		buttonNextCar.enabled = false;
		buttonPreviousCar.enabled = false;

		cam.enabled = false;
	}

	public void Show() {
		buttonNextCar.enabled = true;
		buttonPreviousCar.enabled = true;

		cam.enabled = true;
	}
}