using System.Diagnostics;

namespace AsyncAwait;

internal class Program
{
	static async Task Main(string[] args)
	{
		//Synchron
		Stopwatch sw = Stopwatch.StartNew();
		//Toast();
		//Tasse();
		//Kaffee();
		//Console.WriteLine(sw.ElapsedMilliseconds); //7s

		//////////////////////////////////////////////////////////////////

		//Parallelisierung mit Tasks

		//Task t1 = new Task(Toast);
		//Task t2 = new Task(Tasse);
		//t1.ContinueWith(x =>
		//{
		//	if (t2.IsCompletedSuccessfully)
		//		Console.WriteLine(sw.ElapsedMilliseconds);
		//});
		//t1.Start();
		//t2.ContinueWith(x => Kaffee()).ContinueWith(x =>
		//{
		//	if (t1.IsCompletedSuccessfully)
		//		Console.WriteLine(sw.ElapsedMilliseconds);
		//});
		//t2.Start();

		//t2.Wait(); //Wait() sollte nicht verwendet werden
		//Task.WaitAll(t1, t2); //WaitAll() sollte nicht verwendet werden

		//Console.WriteLine(sw.ElapsedMilliseconds);

		//Sehr aufwändig zu Implementieren ohne Wait/WaitAll

		//////////////////////////////////////////////////////////////////

		//Parallelisierung mit Tasks und await
		//Task t1 = Task.Run(Toast);
		//Task t2 = Task.Run(Tasse).ContinueWith(x => Kaffee());
		////await t1; //t1.Wait()
		////await t2; //t2.Wait()
		//await Task.WhenAll(t1, t2); //Äquivalent zu Task.WaitAll(...)
		//Console.WriteLine(sw.ElapsedMilliseconds); //4s

		//////////////////////////////////////////////////////////////////

		//Parallelisierung mit eigenen async-Methoden
		//Task t1 = ToastAsync(); //Wenn eine async Task Methode gestartet wird, wird diese vollautomatisch parallel gestartet (kein Start() oder Run() notwendig)
		//Task t2 = TasseAsync();
		//await t2;
		//Task t3 = KaffeeAsync();
		////await t1;
		////await t3;
		//await Task.WhenAll(t1, t3);
		//Console.WriteLine(sw.ElapsedMilliseconds); //4s

		//////////////////////////////////////////////////////////////////

		//Parallelisierung mit eigenen async-Methoden und Objekten
		//Task<Toast> t1 = ToastObjectAsync();
		//Task<Tasse> t2 = TasseObjectAsync();
		//Tasse tasse = await t2; //await kann Objekte zurückgeben, wenn der Task ein Ergebnis hat
		//Task<Kaffee> t3 = KaffeeObjectAsync(tasse);
		//Toast toast = await t1;
		//Kaffee kaffee = await t3;
		//Fruehstueck f = new Fruehstueck(toast, kaffee);
		//Console.WriteLine(sw.ElapsedMilliseconds); //4s

		//////////////////////////////////////////////////////////////////

		//Vereinfachung von oben
		Task<Toast> t1 = ToastObjectAsync();
		Task<Tasse> t2 = TasseObjectAsync();
		Task<Kaffee> t3 = KaffeeObjectAsync(await t2);
		Fruehstueck f = new Fruehstueck(await t1, await t3);
		Console.WriteLine(sw.ElapsedMilliseconds); //4s

		Console.ReadKey();
	}

	#region Synchron
	static void Toast()
	{
		Thread.Sleep(4000);
		Console.WriteLine("Toast fertig");
	}

	static void Tasse()
	{
		Thread.Sleep(1500);
		Console.WriteLine("Tasse fertig");
	}

	static void Kaffee()
	{
		Thread.Sleep(1500);
		Console.WriteLine("Kaffee fertig");
	}
	#endregion

	#region Asynchron
	static async Task ToastAsync()
	{
		await Task.Delay(4000);
		Console.WriteLine("Toast fertig");
		//Kein return (gibt sich selbst zurück)
	}

	static async Task TasseAsync()
	{
		await Task.Delay(1500);
		Console.WriteLine("Tasse fertig");
	}

	static async Task KaffeeAsync()
	{
		await Task.Delay(1500);
		Console.WriteLine("Kaffee fertig");
	}
	#endregion

	#region Asynchron mit Objekten
	static async Task<Toast> ToastObjectAsync()
	{
		await Task.Delay(4000);
		Console.WriteLine("Toast fertig");
		return new Toast();
	}

	static async Task<Tasse> TasseObjectAsync()
	{
		await Task.Delay(1500);
		Console.WriteLine("Tasse fertig");
		return new Tasse();
	}

	static async Task<Kaffee> KaffeeObjectAsync(Tasse t)
	{
		await Task.Delay(1500);
		Console.WriteLine("Kaffee fertig");
		return new Kaffee(t);
	}
	#endregion
}

public class Toast;

public class Tasse;

public class Kaffee(Tasse t);

public class Fruehstueck(Toast t, Kaffee f);