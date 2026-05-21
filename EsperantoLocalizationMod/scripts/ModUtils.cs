using System;
using System.Collections;
using Godot;

public static class ModUtils
{
	public const string MOD_CSV_NAMING_SUFFIX = "_Injected";
	private const string MOD_DEBUG_PREFIX = "LOC_INJ_MOD: ";
	private const string INJECTOR_MOD_NAME = "Esperanta Traduko";
	private const string MAIN_MOD_NAME = "Pratfall Localization Mod";

	private static bool _hasMainMod;

	private static string _injectorModPath;
	private static string _mainModPath;

	public static string INJECTOR_MOD_PATH => _injectorModPath;
	public static string MAIN_MOD_PATH => _mainModPath;

	public static bool HasMainMod => _hasMainMod;

	public static void Init()
	{
		FetchModPaths();
	}

	private static void FetchModPaths()
	{
		bool hasFoundInjectorModPath = false;
		bool hasFoundMainModPath = false;
		
		Type modManagerType = null;
		foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
		{
			modManagerType = assembly.GetType("ModManager");
			if (modManagerType != null) 
				break;
		}

		var modsProperty = modManagerType.GetProperty("Mods");
		var modsList = modsProperty.GetValue(null) as IEnumerable;
		foreach (var mod in modsList)
		{
			var nameProperty = mod.GetType().GetProperty("Name");
	   		string modName = nameProperty?.GetValue(mod) as string;

			if (!hasFoundInjectorModPath && modName == INJECTOR_MOD_NAME)
			{
				var dirProperty = mod.GetType().GetProperty("Directory");
				_injectorModPath = dirProperty?.GetValue(mod) as string;
			
				hasFoundInjectorModPath = true;
			}
			else if (!hasFoundMainModPath && modName == MAIN_MOD_NAME)
			{
				var dirProperty = mod.GetType().GetProperty("Directory");
				_mainModPath = dirProperty?.GetValue(mod) as string;
			
				hasFoundMainModPath = true;
			}

			if (hasFoundInjectorModPath && hasFoundMainModPath)
				break;
		}

		_hasMainMod = hasFoundMainModPath;
		if (!_hasMainMod)
			Print($"Couldn't find dependecy: {MAIN_MOD_NAME}");
	}
	
	public static void Print(string message)
	{
		GD.Print($"{MOD_DEBUG_PREFIX}{message}");
	}

	public static void PrintErr(string message)
	{
		GD.PrintErr($"{MOD_DEBUG_PREFIX}{message}");
	}
}
