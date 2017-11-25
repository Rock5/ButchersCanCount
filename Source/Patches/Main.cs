using Harmony;
using System.Reflection;
using Verse;

namespace ButchersCanCountMeat.Patches
{
	[StaticConstructorOnStartup]
	class Main
	{
		static Main()
		{
			var harmony = HarmonyInstance.Create("com.github.rock5.butcherscancountmeat");
			harmony.PatchAll(Assembly.GetExecutingAssembly());
		}
	}
}
