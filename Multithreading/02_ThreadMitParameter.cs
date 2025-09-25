namespace Multithreading;

internal class _02_ThreadMitParameter
{
	static void Main(string[] args)
	{
		Thread t = new Thread(Run);
		t.Start(new ThreadData(10, 200));

		for (int i = 0; i < 100; i++)
			Console.WriteLine($"Main Thread: {i}");
	}

	public static void Run(object? o)
	{
		if (o is ThreadData dt)
		{
			for (int i = dt.start; i < dt.max; i++)
				Console.WriteLine($"Side Thread: {i}");
		}
	}
}

public record ThreadData(int start, int max);