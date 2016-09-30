using UnityEngine;
using System;

[Serializable]
public class TransmissionGearsTuning
{
	public GearsTuningType type = GearsTuningType.Stock;

	public TransmissionGearsTuning (GearsTuningType type) {
		this.type = type;
	}

	public double GetPercentageIncrease() {
		if (type == GearsTuningType.ShortSportsSpec)
			return 0.2;
		else if (type == GearsTuningType.ShortDragSpec)
			return 0.4;
		else
			return 0;
	}
}