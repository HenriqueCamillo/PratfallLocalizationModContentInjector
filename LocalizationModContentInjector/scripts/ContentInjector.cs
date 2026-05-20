using System.IO;

public static class ContentInjector
{
	private static string INJECTOR_MOD_LOCALIZATION_FOLDER => Path.Combine(ModUtils.INJECTOR_MOD_PATH, "Localization");
	private static string MAIN_MOD_LOCALIZATION_FOLDER => Path.Combine(ModUtils.MAIN_MOD_PATH, "Localization");

	public static void CopyCSVsToMainMod()
	{
		string[] csvFiles = Directory.GetFiles(INJECTOR_MOD_LOCALIZATION_FOLDER, "*.csv");
		string targetFolder = MAIN_MOD_LOCALIZATION_FOLDER;

		foreach (var csv in csvFiles)
		{
			string fileName = $"{Path.GetFileNameWithoutExtension(csv)}{ModUtils.MOD_CSV_NAMING_SUFFIX}.csv";
			ModUtils.Print($"Copying {fileName} to {targetFolder}");
			string newFilePath = Path.Combine(targetFolder, fileName);
			File.Copy(csv, newFilePath, true);
		}
	}
}
