namespace Multitasking;

internal class _04_TaskWarten
{
	static void Main(string[] args)
	{
		Task t = new Task(Run);
		t.Start();

		t.Wait(); //GUI Alternative: await

		Task.WaitAll(t); //Wartet auf alle (mehrere) Tasks

		Task.WaitAny(t); //Wartet auf den schnellsten Task
	}

	static void Run()
	{
		for (int i = 0; i < 100; i++)
			Console.WriteLine($"Side Task: {i}");
	}
}
