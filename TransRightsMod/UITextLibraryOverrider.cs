using HarmonyLib;
using System;
// unused, but is used if uncommenting debug lines
using OWML.Common;

namespace TransRightsMod;

[HarmonyPatch]
public class UITextLibraryOverrider
{
	[HarmonyPrefix]
	[HarmonyPatch(typeof(UITextLibrary), nameof(UITextLibrary.GetString))]
	public static bool UITextLibrary_GetString_Prefix(UITextType TextID, ref String __result)
	{

		//For Debug
		//TransRightsMod.Instance.ModHelper.Console.WriteLine($"In TransRightsMod Prefix", MessageType.Debug);

		String uiText = TextTranslation.Translate_UI((int)TextID);
		if ( uiText.EndsWith("--|-..|-."))
		{

			//For Debug
			//TransRightsMod.Instance.ModHelper.Console.WriteLine($"Attempting to Modify UI Text", MessageType.Info);

			String transRightsOrWrongs = "Trans Rights!";
			// 5% chance of "Trans Wrongs!"
			if ( (new Random().NextDouble()) <= TransRightsMod.WrongsPercentage )
			{
				transRightsOrWrongs = "Trans Wrongs!";
			}

			uiText = uiText.TrimEnd("--|-..|-.".ToCharArray());
			uiText += transRightsOrWrongs;
		}
		__result = uiText;
		return false;

	}
}
