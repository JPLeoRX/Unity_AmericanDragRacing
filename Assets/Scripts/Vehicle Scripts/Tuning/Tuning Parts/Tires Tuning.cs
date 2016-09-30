using UnityEngine;
using System;

[Serializable]
public class TiresTuning 
{
	public TuningType type = TuningType.Level0Stock;

	public TiresTuning(TuningType type) {
		this.type = type;
	}

	public double GetRollingFrictionCoefficient() {
		if (type == TuningType.Level1StreetSpec)
			return 0.018;
		else if (type == TuningType.Level2TunerSpec)
			return 0.016;
		else if (type == TuningType.Level3SportsSpec)
			return 0.014;
		else if (type == TuningType.Level4RaceSpec)
			return 0.012;
		else if (type == TuningType.Level5DragSpec)
			return 0.010;
		else if (type == TuningType.Level6MagicSpec)
			return 0.005;
		else
			return 0.02;
	}
}