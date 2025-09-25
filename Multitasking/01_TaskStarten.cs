namespace Multitasking;

internal class _01_TaskStarten
{
	static void Main(string[] args)
	{
		Task t = new Task(Run);
		t.Start();

		Task t2 = Task.Factory.StartNew(Run); //.NET Framework 4.0

		Task t3 = Task.Run(Run); //.NET Framework 4.5

		for (int i = 0; i < 100; i++)
			Console.WriteLine($"Main Thread: {i}");
	}

	static void Run()
	{
		for (int i = 0; i < 100; i++)
			Console.WriteLine($"Side Task: {i}");
	}
}
