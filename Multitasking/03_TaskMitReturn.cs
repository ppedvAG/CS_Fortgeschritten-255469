namespace Multitasking;

internal class _03_TaskMitReturn
{
	static void Main(string[] args)
	{
		Task<int> t = new Task<int>(Calculate);
		t.Start();

		//Console.WriteLine(t.Result); //Problem: Wenn hier das Result abgefragt wird, wird die Schleife blockiert
		bool hasPrinted = false;
		for (int i = 0; i < 100; i++)
		{
			if (!hasPrinted && t.IsCompletedSuccessfully) //Alternative: ContinueWith
			{
				Console.WriteLine(t.Result);
				hasPrinted = true;
			}

			Console.WriteLine($"Main Thread: {i}");
			Thread.Sleep(25);
		}
		//Console.WriteLine(t.Result); //Problem: Wenn hier das Result abgefragt wird, kommt das Ergebnis erst in die Konsole, wenn die Schleife fertig ist

		Console.ReadKey();
	}

	static int Calculate()
	{
		Thread.Sleep(500);
		return Random.Shared.Next();
	}
}
