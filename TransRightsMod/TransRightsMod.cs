using HarmonyLib;
using OWML.Common;
using OWML.ModHelper;
using System.Reflection;
using System;

namespace TransRightsMod;

public class TransRightsMod : ModBehaviour
{
	public static TransRightsMod Instance;
	public static double WrongsPercentage;

	public void Awake()
	{
		Instance = this;
		// You won't be able to access OWML's mod helper in Awake.
		// So you probably don't want to do anything here.
		// Use Start() instead.
	}

	public void Start()
	{
		// Starting here, you'll have access to OWML's mod helper.
		ModHelper.Console.WriteLine($"My mod {nameof(TransRightsMod)} is loaded!", MessageType.Success);

		new Harmony("CrystalENVT.TransRightsMod").PatchAll(Assembly.GetExecutingAssembly());
	}

	public override void Configure(IModConfig config)
	{

		WrongsPercentage = config.GetSettingsValue<double>("Trans Wrongs Percentage");
		// If Wrongs Percentage is greater than 100%, assume user entered percentage as whole numbers
		if (WrongsPercentage > 1.0)
		{
			if (WrongsPercentage > 100.0)
			{
				// huge value entered, just set to 100%
				WrongsPercentage = 1.0;
			}
			else
			{
				// divide by 100 to convert to a percentage value
				WrongsPercentage /= 100.0;
			}
			config.SetSettingsValue("Trans Wrongs Percentage", Math.Round( WrongsPercentage, 2) );
		}
		// Preventing potential errors early
		else if (WrongsPercentage < 0.0)
		{
			WrongsPercentage = 0.0;
			config.SetSettingsValue("Trans Wrongs Percentage", WrongsPercentage);
		}
	}

}
