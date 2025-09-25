namespace Multithreading;

internal class _05_Lock
{
	public static int Counter;

	public static object LockObject = new();

	static void Main(string[] args)
	{
		List<Thread> threads = new List<Thread>();
		for (int i = 0; i < 100; i++)
		{
			Thread t = new Thread(Increment);
			t.Start();
			threads.Add(t);
		}
	}

	static void Increment()
	{
		for (int i = 0; i < 100; i++)
		{
			//Lock: Sperrt einen Codeblock, sodass nicht mehrere Thread gleichzeitig darauf zugreifen können
			lock (LockObject)
			{
				Counter++;
				//Interlocked.Increment(ref Counter); Counter++
				Console.WriteLine(Counter);
			}

			//Monitor: 1:1 identisch zu Lock, kann aber mit if's/try-catch/... kombiniert werden
			Monitor.Enter(LockObject);
			Counter++;
			Console.WriteLine(Counter);
			Monitor.Exit(LockObject);
		}
	}
}
