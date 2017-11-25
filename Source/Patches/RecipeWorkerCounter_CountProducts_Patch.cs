using Harmony;
using RimWorld;
using Verse;

namespace ButchersCanCountMeat.Patches
{
	[HarmonyPatch(typeof(RecipeWorkerCounter), "CountProducts")]
	class RecipeWorkerCounter_CountProducts_Patch
	{
		// Determines if settings allow type of meat to be counted
		private static bool shouldCount(string defName)
		{
			if (defName == "Human_Meat")
				return BCCMController.CountHumanMeat;

			if (defName == "Megaspider_Meat")
				return BCCMController.CountInsectMeat;

			return BCCMController.CountAnimalMeat;
		}

		[HarmonyPrefix]
		static bool CountProducts_Patch(ref int __result, Bill_Production bill)
		{
			// If butchery bill
			if (bill.recipe.specialProducts != null &&
				bill.recipe.specialProducts.Contains(SpecialProductType.Butchery))
			{
				int num = 0;
				for (int i = 0; i < ThingCategoryDefOf.MeatRaw.childThingDefs.Count; i++)
				{
					if (shouldCount(ThingCategoryDefOf.MeatRaw.childThingDefs[i].defName))
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
