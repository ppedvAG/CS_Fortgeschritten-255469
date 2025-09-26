using System.Collections.Concurrent;

namespace TPL_Uebung;

public class Program
{
	public static ConcurrentQueue<string> ImagePaths = [];
	public static List<string> ProcessedImages = [];

	public static string SourcePath = @"C:\Users\lk3\Unterlagen\PPKURS-CS-Fortgeschritten\Other\DIV2K_train_HR\DIV2K_train_HR";
	public static string OutputPath = @"C:\Users\lk3\source\repos\CSharp_Fortgeschritten_2025_09_24\TPL_Uebung\ImagesNew\";

	static void Main(string[] args)
	{
		while (true)
		{
			Console.WriteLine("Eingaben: ");
			Console.WriteLine("1: Neuen Scanner erstellen");
			Console.WriteLine("2: Anzahl Worker Tasks anpassen");
			Console.WriteLine("3: Speicherpfad anpassen");
			Console.WriteLine("4: Prozess starten/fortsetzen");
			Console.WriteLine("5: Prozess pausieren");

			ConsoleKey inputKey = Console.ReadKey(true).Key;

			switch (inputKey)
			{
				case ConsoleKey.D1:
					CreateScanner();
					break;

				case ConsoleKey.D2:
					AdjustWorkerAmount();
					break;

				case ConsoleKey.D3:
					AdjustOutputPath();
					break;

				case ConsoleKey.D4:
					StartProcess();
					break;

				case ConsoleKey.D5:
					PauseProcess();
					break;
			}
		}
	}

	#region Input Methoden
	private static void CreateScanner()
	{
		//string path = Console.ReadLine();
		string path = SourcePath;
		Scanner s = new Scanner(path);
	}

	private static void AdjustWorkerAmount()
	{
		Console.Write("Anzahl Worker eingeben: ");
		string input = Console.ReadLine();
		if (int.TryParse(input, out int value))
		{
			for (int i = 0; i < value; i++)
			{
				Worker w = new Worker();
			}
		}
	}

	private static void AdjustOutputPath()
	{

	}

	private static void StartProcess()
	{

	}

	private static void PauseProcess()
	{

	}
	#endregion
}