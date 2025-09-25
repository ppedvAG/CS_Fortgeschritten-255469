namespace Multitasking;

internal class _05_TaskExceptions
{
	static void Main(string[] args)
	{
		Task t = new Task(Run); //Wenn ein Task abstürzt, läuft das Programm normal weiter
		t.Start();

		//Die AggregateException wird von t.Result, t.Wait() und Task.WaitAll(...) geworfen

		bool hasPrinted = false;
		for (int i = 0; i < 100; i++)
		{
			if (!hasPrinted && t.IsFaulted) //Alternative: ContinueWith
			{
				Console.WriteLine(t.Exception.ToString());
				hasPrinted = true;
			}

			Console.WriteLine($"Main Thread: {i}");
			Thread.Sleep(10);
		}

		try
		{
			Task.WaitAll(t);
		}
		catch (AggregateException e)
		{
			foreach (Exception ex in e.InnerExceptions)
			{
				Console.WriteLine(ex.ToString());
			}
		}

		Console.ReadKey();
	}

	static void Run()
	{
		Thread.Sleep(Random.Shared.Next(200, 500));
		throw new Exception("Hallo");
	}
}
