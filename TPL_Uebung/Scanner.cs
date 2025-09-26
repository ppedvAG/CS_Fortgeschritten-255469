namespace TPL_Uebung;

/// <summary>
/// Die Scanner Klasse soll kontinuierlich einen Ordner überprüfen, ob neue Images zur Verarbeitung aufgetaucht sind.
/// Dafür soll diese Klasse intern einen Task besitzen, der diese Arbeit übernimmt.
/// Es soll auch sicher gestellt werden, das bereits gescannte/verarbeitete Images nicht doppelt gescannt/verarbeitet werden.
/// Der Benutzer soll die Möglichkeit haben, mehrere Scanner zu Erstellen und dadurch mehrere Ordner gleichzeitig zu Verarbeiten.
/// </summary>
public class Scanner
{
	private string Path { get; init; }

    public Scanner(string path)
    {
		Path = path;

		Task t = Task.Run(DoScan);
    }

	private void DoScan()
	{
		while (true)
		{
			string[] imagePaths = Directory.GetFiles(Path);
			IEnumerable<string> newPaths = imagePaths.Except(Program.ImagePaths).Except(Program.ProcessedImages);

			foreach (string path in newPaths)
			{
				Program.ImagePaths.Enqueue(path);
				Console.WriteLine($"Neues File: {path}");
			}

			Thread.Sleep(1000);
		}
	}
}