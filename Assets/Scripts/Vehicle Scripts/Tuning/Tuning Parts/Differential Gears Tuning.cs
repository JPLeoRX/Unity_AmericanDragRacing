using UnityEngine;
using System;

[Serializable]
public class DifferentialGearsTuning
{
	public GearsTuningType type = GearsTuningType.Stock;

	public DifferentialGearsTuning (GearsTuningType type) {
		this.type = type;
	}

	public double GetPercentageIncrease() {
		if (type == GearsTuningType.ShortSportsSpec)
			return 0.1;
		else if (type == GearsTuningType.ShortDragSpec)
			return 0.2;
		else
			return 0;
	}
}