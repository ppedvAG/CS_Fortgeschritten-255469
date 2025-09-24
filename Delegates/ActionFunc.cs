namespace Delegates;

internal class ActionFunc
{
	static void Main(string[] args)
	{
		//Action und Func
		//Vorgegebene Delegates, die an vielen Stellen in der Sprache vorkommen
		//z.B.: Multitasking, Linq, Reflection, ...
		//Essentiell für die Fortgeschrittene Programmierung

		//Action
		//Delegate, welches immer void zurückgibt, und bis zu 16 Parameter hat
		//Über Generics werden die Typen der Parameter gesetzt
		//https://github.com/dotnet/runtime/blob/main/src/libraries/System.Private.CoreLib/src/System/Action.cs

		Action<int, int> add = new Action<int, int>(Addiere);

		//Alles aus dem vorherigen Kapitel möglich
		add(3, 4);
		add?.Invoke(3, 4);
		add += Addiere;
		add -= Addiere;

		Action a = Test; //Action ohne Generics = keine Parameter

		//Praktisches Beispiel
		List<int> list = [1, 2, 3, 4, 5];
		list.ForEach(Print); //Aufgabe: Schreibe jedes Element in die Konsole mittels ForEach

		////////////////////////////////////////////////////////////////////////

		//Func
		//Delegate, welches einen beliebigen Rückgabetypen hat, und bis zu 16 Parameter hat
		//Über Generics werden die Typen der Parameter und des Rückgabewertes gesetzt
		//Der letzte Generische Typenparameter (Generic) bestimmt den Rückgabetyp
		Func<int> f = Zufallszahl;

		Func<int, int, double> div = Dividiere;
		//double quotient = div?.Invoke(5, 3); //Problem: Invoke kann null zurückgeben, wenn die Func selbst null ist
		double? q = div?.Invoke(5, 3); //Lösung 1

		//Lösung 2
		double quotient;
		if (q != null)
			quotient = q.Value;
		else
			quotient = double.NaN;

		double q2 = div?.Invoke(5, 3) ?? double.NaN; //Lösung 2 aber besser

		//Praktisches Beispiel
		list.Where(TeilbarDurch2);

		////////////////////////////////////////////////////////////////////////

		//Anonyme Funktionen: Methodenzeiger, welche nicht dediziert erstellt werden, sondern nur bei dem Action-/Funcparameter eingesetzt werden
		//-> werden einmal verwendet und weggeworfen
		div = delegate (int x, int y)
		{
			return (double) x / y;
		};

		div += (int x, int y) =>
		{
			return (double) x / y;
		};

		div += (int x, int y) => (double) x / y;

		div += (x, y) => (double) x / y;

		//Where mit anonymer Methode
		list.Where(e => e % 2 == 0);

		//Aufgabe: Alle Elemente einer Liste in der Konsole ausgeben
		list.ForEach(x => Console.WriteLine($"Zahl: {x}"));

		//Funktionszeiger übergeben von C#-internen Funktionen
		list.ForEach(Console.WriteLine);
	}

	#region Action
	static void Addiere(int a, int b) => Console.WriteLine($"{a} + {b} = {a + b}");

	static void Test() => Console.WriteLine("Hallo");

	static void Print(int x) => Console.WriteLine($"Zahl: {x}");
	#endregion

	#region Func
	static int Zufallszahl() => Random.Shared.Next();

	static double Dividiere(int x, int y) => (double) x / y;

	static bool TeilbarDurch2(int x) => x % 2 == 0;
	#endregion
}
