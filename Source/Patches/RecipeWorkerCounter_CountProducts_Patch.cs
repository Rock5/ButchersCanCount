using Harmony;
using RimWorld;
using Verse;

namespace ButchersCanCountMeat.Patches
{
	/// <summary>
	/// Bypasses CountProducts to return a count of meat to be used with the Do Until option.
	/// </summary>
	[HarmonyPatch(typeof(RecipeWorkerCounter), "CountProducts")]
	class RecipeWorkerCounter_CountProducts_Patch
	{
		// Determines if settings allow type of meat to be counted
		private static bool meatDefFilter(string defName, bool countAnimalMeat, bool countHumanMeat, bool countInsectMeat)
		{
			if (defName == "Human_Meat")
				return countHumanMeat;

			if (defName == "Megaspider_Meat")
				return countInsectMeat;

			return countAnimalMeat;
		}

		[HarmonyPrefix]
		static bool CountProducts_Patch(ref int __result, Bill_Production bill)
		{
			// If butchery bill
			if (bill.recipe.specialProducts != null &&
			bill.recipe.specialProducts.Contains(SpecialProductType.Butchery))
			{
				bool countAnimalMeat = false;
				bool countHumanMeat = false;
				bool countInsectMeat = false;

				// Count all meat types if not 1 of my custom recipes
				if( bill.recipe.defName !="ButcherAnimalFlesh" && bill.recipe.defName != "ButcherHumanFlesh" &&
					bill.recipe.defName != "ButcherInsectFlesh" && bill.recipe.defName != "ButcherAnimalHumanFlesh" &&
					bill.recipe.defName != "ButcherHumanInsectFlesh" )
				{
					countAnimalMeat = true;
					countHumanMeat = true;
					countInsectMeat = true;
				}
				else
				{
					// Include only meats relevant to my custom recipe 
					if (bill.recipe.defName == "ButcherAnimalFlesh" || bill.recipe.defName == "ButcherAnimalHumanFlesh")
						countAnimalMeat = true;
					if (bill.recipe.defName == "ButcherHumanFlesh" || bill.recipe.defName == "ButcherAnimalHumanFlesh" || bill.recipe.defName == "ButcherHumanInsectFlesh")
						countHumanMeat = true;
					if (bill.recipe.defName == "ButcherInsectFlesh" || bill.recipe.defName == "ButcherHumanInsectFlesh")
						countInsectMeat = true;
				}

				// Count all child things of MeatRaw that pass the filter
				int num = 0;
				for (int i = 0; i < ThingCategoryDefOf.MeatRaw.childThingDefs.Count; i++)
				{
					if (meatDefFilter(ThingCategoryDefOf.MeatRaw.childThingDefs[i].defName, countAnimalMeat, countHumanMeat, countInsectMeat))
					{
						num += bill.Map.resourceCounter.GetCount(ThingCategoryDefOf.MeatRaw.childThingDefs[i]);
					}
				}
				__result = num;

				// Skip original method
				return false;
			}

			// Not a butchery bill. Run original method
			return true;
		}
	}
}
