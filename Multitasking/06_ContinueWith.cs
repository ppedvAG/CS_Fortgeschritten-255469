namespace Multitasking;

internal class _06_ContinueWith
{
	static void Main(string[] args)
	{
		Task<int> t = new Task<int>(Calculate); //Der Task berechnet ein Ergebnis, wenn dieser fertig ist, soll das Ergebnis ausgegeben werden
		t.ContinueWith(Folgetask, TaskContinuationOptions.OnlyOnRanToCompletion); //Ohne Lambda
		t.ContinueWith(vorherigerTask => Console.WriteLine(vorherigerTask.Result), TaskContinuationOptions.OnlyOnRanToCompletion); //Mit Lambda
		t.ContinueWith(vorherigerTask => Console.WriteLine(vorherigerTask.Exception.ToString()), TaskContinuationOptions.OnlyOnFaulted); //Mit Lambda
		t.Start(); //ContinueWith sollte vor dem Start ausgeführt werden, sonst kann der Task VOR dem ContinueWith fertig sein

		for (int i = 0; i < 50; i++)
		{
			Console.WriteLine($"Main Thread: {i}");
			Thread.Sleep(25);
		}

		Console.ReadKey();
	}

	static int Calculate()
	{
		int time = Random.Shared.Next(200, 500);
		Thread.Sleep(time);

		if (Random.Shared.Next() % 2 == 0)
			throw new Exception("Task ist abgestürzt");

		return Random.Shared.Next();
	}

	/// <summary>
	/// Bei ContinueWith kann immer auf den vorherigen Task zugegriffen werden
	/// 
	/// Code in dieser Methode wird als Task ausgeführt -> keine Blockade des Main Threads
	/// 
	/// u.a. t.Result, t.Exception
	/// </summary>
	static void Folgetask(Task<int> t)
	{
		Console.WriteLine(t.Result);
	}
}
