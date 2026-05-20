using System.Collections.Generic;
using System.IO;
using System.Linq;
using Godot;
using Microsoft.VisualBasic.FileIO;

public static class ModEntry
{
	public static void ModInit()
	{
		ModUtils.Print("Initializing Localization Injector Mod...");

		ModUtils.Init();
		if (!ModUtils.HasMainMod)
			return;

		ContentInjector.CopyCSVsToMainMod();
	}

	public static void ModDestroy()
	{
		ModUtils.Print("Localization Injector Mod destroyed!");
	}
}
