using UnityEngine;
using System;
using UnityEngine.UI;

[Serializable]
public class PlayerOutput : MonoBehaviour 
{
	public Car playerCar;
	public Text textSpeed;
	public Text textRPM;
	public Text textGear;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		textSpeed.text = playerCar.GetCurrentSpeedKMH() + " KM/H";
		textRPM.text = playerCar.GetCurrentRPM() + " RPM";
		textGear.text = playerCar.GetCurrentGearActual() + " GEAR";
	}
}
