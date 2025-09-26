using System.Collections;
using System.Diagnostics;
using System.Xml.Serialization;

namespace LinqErweiterungsmethoden;

internal class Program
{
	static void Main(string[] args)
	{
		//IEnumerable
		//Nur eine Anleitung zur Erstellung der Daten
		//Hält selbst keine Daten
		IEnumerable<int> zahlen = Enumerable.Range(0, 1_000_000_000); //1ms

		//Anleitung ausführen
		//List<int> zahlen2 = Enumerable.Range(0, 1_000_000_000).ToList(); //1s

		//Enumerator
		List<int> ints = [1, 2, 3, 4, 5];
		foreach (int i in ints)
		{
			Console.WriteLine(i);
		}

		//foreach ohne foreach
		IEnumerator e = ints.GetEnumerator();
		e.MoveNext();
		start:
		Console.WriteLine(e.Current);
		if (e.MoveNext())
			goto start; //goto nicht verwenden
		e.Reset();

		///////////////////////////////////////////////////////////

		List<int> z = Enumerable.Range(1, 20).ToList();
		Console.WriteLine(z.Sum());
		Console.WriteLine(z.Average());
		Console.WriteLine(z.Min());
		Console.WriteLine(z.Max());

		//Console.WriteLine(z.First(e => e % 200 == 0)); //Absturz
		Console.WriteLine(z.FirstOrDefault(e => e % 200 == 0)); //0

		Console.WriteLine(z.Last());
		Console.WriteLine(z.LastOrDefault());

		///////////////////////////////////////////////////////////

		List<Fahrzeug> fahrzeuge =
		[
			new Fahrzeug(251, FahrzeugMarke.BMW),
			new Fahrzeug(274, FahrzeugMarke.BMW),
			new Fahrzeug(146, FahrzeugMarke.BMW),
			new Fahrzeug(208, FahrzeugMarke.Audi),
			new Fahrzeug(189, FahrzeugMarke.Audi),
			new Fahrzeug(133, FahrzeugMarke.VW),
			new Fahrzeug(253, FahrzeugMarke.VW),
			new Fahrzeug(304, FahrzeugMarke.BMW),
			new Fahrzeug(151, FahrzeugMarke.VW),
			new Fahrzeug(250, FahrzeugMarke.VW),
			new Fahrzeug(217, FahrzeugMarke.Audi),
			new Fahrzeug(125, FahrzeugMarke.Audi)
		];

		//Alle BMWs finden
		fahrzeuge.Where(e => e.Marke == FahrzeugMarke.BMW);

		//Alle BMWs finden, welche über 200km/h fahren können
		fahrzeuge.Where(e => e.Marke == FahrzeugMarke.BMW && e.MaxV > 200);
		fahrzeuge.Where(e => e.Marke == FahrzeugMarke.BMW).Where(e => e.MaxV > 200);

		//Nach Marken sortieren
		fahrzeuge.OrderBy(e => e.Marke);

		//Subsequente Sortierungen
		fahrzeuge.OrderBy(e => e.Marke).ThenBy(e => e.MaxV);

		//Fahren alle Fahrzeuge mind. 200km/h?
		if (fahrzeuge.All(e => e.MaxV > 200))
		{
			//...
		}

		//Fährt mind. ein Fahrzeug mind. 200km/h?
		if (fahrzeuge.Any(e => e.MaxV > 200))
		{
			//...
		}

		//In einem String prüfen, ob alle Zeichen Buchstaben sind
		string text = "Hallo Welt";
		if (text.All(char.IsLetter))
		{

		}

		if (text.All(e => e >= 'a' && e <= 'Z'))
		{

		}

		//Wie viele BMWs haben wir?
		fahrzeuge.Count(e => e.Marke == FahrzeugMarke.BMW);
		fahrzeuge.Where(e => e.Marke == FahrzeugMarke.BMW).Count();

		//Average, Sum, Min, MinBy, Max, MaxBy
		fahrzeuge.Average(e => e.MaxV); //208.416666666666

		fahrzeuge.Min(e => e.MaxV); //Die Zahl
		fahrzeuge.MinBy(e => e.MaxV); //Das Fahrzeug mit der Zahl

		//Skip & Take
		int page = 1;
		fahrzeuge.Skip(page * 10).Take(10);

		//Was sind die 3 schnellsten Fahrzeuge?
		fahrzeuge
			.OrderByDescending(e => e.MaxV)
			.Take(3);

		//Select
		//Transformiert eine Liste
		//Nimmt eine "Form" als Parameter, wendet diese Form auf jedes Listenelement an

		//1. Fall (80%): Einzelnes Feld entnehmen
		fahrzeuge.Select(e => e.Marke);
		fahrzeuge.Select(Marken);

		FahrzeugMarke Marken(Fahrzeug fzg) => fzg.Marke;

		List<FahrzeugMarke> marken = [];
		foreach (Fahrzeug fzg in fahrzeuge)
			marken.Add(fzg.Marke);

		//2. Fall (20%): Liste transformieren

		//Liste mit 0.1er Schritten von 0 bis 10
		Enumerable.Range(0, 100).Select(e => e / 10.0);

		//Liste von Strings (Usereingaben) sollen alle zu Ints konvertiert werden
		new string[] { "1", "5", "18", "4", "37" }.Select(int.Parse);

		//Gesamte Liste casten
		Enumerable.Range(0, 10).Select(e => (double) e);

		//Alle Dateinamen aus einer Liste von Pfaden entnehmen
		List<string> files = [];
		string[] paths = Directory.GetFiles("C:\\Windows");
		foreach (string path in paths)
			files.Add(Path.GetFileName(path));

		Directory.GetFiles("C:\\Windows").Select(e => Path.GetFileName(e)).ToList();
		List<string> linqPaths = Directory.GetFiles("C:\\Windows").Select(Path.GetFileName).ToList();

		Console.WriteLine(files.SequenceEqual(linqPaths));

		//Liste von 10 Fahrzeugen erzeugen
		Enumerable.Range(0, 10).Select(_ => new Fahrzeug(0, FahrzeugMarke.Audi));

		//SelectMany
		//Select, führt aber auch eine Glättung durch
		List<List<int>> list = [[1, 2, 3], [4, 5, 6], [7, 8, 9]];
		list.SelectMany(e => e);

		//GroupBy
		//Gruppierung
		//Legt Gruppen an anhand eines Kriteriums; jedes Element wird in seine Gruppe platziert
		fahrzeuge.GroupBy(e => e.Marke);

		fahrzeuge.GroupBy(e => e.Marke).ToDictionary(e => e.Key, e => e.ToList()); //ToDictionary benötigt 2 Lambda-Expressions

		///////////////////////////////////////////////////////////

		//Erweiterungsmethoden
		int zahl = 123;
		Console.WriteLine(zahl.Quersumme());

		//Vom Compiler generiert
		ExtensionMethods.Quersumme(zahl);

		Enumerable.Where(fahrzeuge, e => e.Marke == FahrzeugMarke.BMW);

		//Eigene Linq Methode
		fahrzeuge.Shuffle();
	}
}

public static class ExtensionMethods
{
	public static int Quersumme(this int x)
	{
		//int summe = 0;
		//string zahlAlsString = x.ToString();
		//for (int i = 0; i < zahlAlsString.Length; i++)
		//{
		//	summe += (int) char.GetNumericValue(zahlAlsString[i]);
		//}
		//return summe;

		return (int) x.ToString().Sum(char.GetNumericValue);
	}

	//public static IEnumerable<T> Flatten<T>(this IEnumerable<T> x)
	//{
	//	return x.SelectMany(e => e);
	//}

	public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> list)
	{
		return list.OrderBy(e => Random.Shared.Next());
	}
}

[DebuggerDisplay("MaxV: {MaxV}, Marke: {Marke}")]
public class Fahrzeug
{
	public int MaxV { get; set; }

	public FahrzeugMarke Marke { get; set; }

	public Fahrzeug(int maxV, FahrzeugMarke marke)
	{
		MaxV = maxV;
		Marke = marke;
	}
}

public enum FahrzeugMarke { Audi, BMW, VW }