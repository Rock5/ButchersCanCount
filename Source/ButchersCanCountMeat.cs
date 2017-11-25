using HugsLib;
using HugsLib.Settings;
using Verse;

namespace ButchersCanCountMeat
{
	public class BCCMController : ModBase
	{
		public override string ModIdentifier
		{
			get { return "ButchersCanCountMeat"; }
		}

		public static SettingHandle<bool> CountAnimalMeat;
		public static SettingHandle<bool> CountInsectMeat;
		public static SettingHandle<bool> CountHumanMeat;

		public override void DefsLoaded()
		{
			CountAnimalMeat = Settings.GetHandle<bool>("animalToggle", "Include_Animal_Meat".Translate(), "Include_Animal_Meat_Tooltip".Translate(), true);
			CountInsectMeat = Settings.GetHandle<bool>("insectToggle", "Include_Insect_Meat".Translate(), "Include_Insect_Meat_Tooltip".Translate(), false);
			CountHumanMeat = Settings.GetHandle<bool>("humanToggle", "Include_Human_Meat".Translate(), "Include_Human_Meat_Tooltip".Translate(), false);
		}
	}
}
