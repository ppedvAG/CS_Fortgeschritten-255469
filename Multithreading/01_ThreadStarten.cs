namespace Multithreading;

internal class _01_ThreadStarten
{
	static void Main(string[] args)
	{
		Thread t = new Thread(Run);
		t.Start();

		for (int i = 0; i < 100; i++)
			Console.WriteLine($"Main Thread: {i}");
	}

	public static void Run()
	{
		for (int i = 0; i < 100; i++)
			Console.WriteLine($"Side Thread: {i}");
	}
}
