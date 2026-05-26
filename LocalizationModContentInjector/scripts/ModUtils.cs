using Godot;

public static class ModUtils
{
	public const string MOD_CSV_NAMING_SUFFIX = "_Injected";
	private const string MOD_DEBUG_PREFIX = "LOC_INJ_MOD: ";
	private const string INJECTOR_MOD_NAME = "Localization Mod Content Injector";
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
		
		foreach (ModManifest mod in ModManager.Mods)
		{
			if (!hasFoundInjectorModPath && mod.Name == INJECTOR_MOD_NAME)
			{
				_injectorModPath = mod.Directory;
				hasFoundInjectorModPath = true;
			}
			else if (!hasFoundMainModPath && mod.Name == MAIN_MOD_NAME)
			{
				_mainModPath = mod.Directory;
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
