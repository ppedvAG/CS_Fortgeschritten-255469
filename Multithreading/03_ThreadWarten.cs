namespace Multithreading;

internal class _03_ThreadWarten
{
	static void Main(string[] args)
	{
		Thread t = new Thread(Add);
		t.Start();

		t.Join(); //Wartet auf den Thread/führe die beiden Threads zusammen

		for (int i = 0; i < 100; i++)
			Console.WriteLine($"Main Thread: {i}");
	}

	static void Add()
	{
		for (int i = 0; i < 100; i++)
			Console.WriteLine($"Side Thread: {i}");
	}
}
