using Harmony;
using RimWorld;
using Verse;

namespace ButchersCanCountMeat.Patches
{
	/// <summary>
	/// Bypasses CanCountProducts and returns true for all butcher bills
	/// </summary>
	[HarmonyPatch(typeof(RecipeWorkerCounter), "CanCountProducts")]
	class RecipeWorkerCounter_CanCountProducts_Patch
	{
		[HarmonyPostfix]
		static void CanCountProducts_Patch(ref bool __result, Bill_Production bill)
		{
			if (bill.recipe.specialProducts != null &&
				bill.recipe.specialProducts.Contains(SpecialProductType.Butchery))
			{
				__result = true;
			}
		}
	}
}
