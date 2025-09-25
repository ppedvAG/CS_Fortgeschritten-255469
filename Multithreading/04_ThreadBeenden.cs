using System.Collections.Concurrent;

namespace Multithreading;

internal class _04_ThreadBeenden
{
	static void Main(string[] args)
	{
		//Alternative: CancellationToken
		//Quelle: Produziert Tokens, diese sind mit der Quelle verbunden
		//Wenn auf der Quelle das Cancel-Signal gesendet wird, kann dieses bei jedem Token verarbeitet werden
		CancellationTokenSource cts = new();
		CancellationToken ct = cts.Token; //Token ist ein Struct, wenn die Variable angesprochen (cts.Token) wird ein neuer Token erzeugt

		Thread t = new Thread(Run);
		t.Start(ct);

		//t.Abort(); //Obsolet ab .NET (Core) 5

		Thread.Sleep(500);

		cts.Cancel();
	}

	static void Run(object? o)
	{
		if (o is CancellationToken ct)
		{
			for (int i = 0; i < 100; i++)
			{
				//ct.ThrowIfCancellationRequested();
				if (ct.IsCancellationRequested)
				{
					Console.WriteLine("Thread abgebrochen");
					break;
				}

				Console.WriteLine($"Side Thread: {i}");
				Thread.Sleep(25);
			}
		}
	}
}
