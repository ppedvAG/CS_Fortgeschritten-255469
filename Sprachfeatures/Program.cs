using System.Collections;

namespace Sprachfeatures;

unsafe internal class Program
{
	private int Zahl;

	public int Init { get; init; } = 1;

	public Program()
	{
		Init = 5;
	}

	unsafe static void Main(string[] args)
	{
		if (int.TryParse("1", out int x))
		{

		}
		Console.WriteLine(x);

		object o = 1;
		if (o.GetType() == typeof(int))
		{
			//Genauer Typvergleich
		}

		if (o is int i)
		{
			//Vererbungshiearchietypvergleich
			//if (o is IComparable) => true
			//if (o.GetType() == typeof(IComparable)) => false

			//int i = (int)o;
		}

		//if (o.GetType().GetInterface("IComparable") != null) { } //Weniger Performant als is


		long a = 81_294_713_285_432;
		Console.WriteLine(a);

		//class und struct
	
		//class
		//Referenztyp
		//Wenn ein Objekt einer Klasse auf eine Variable zugewiesen wird, wird eine Referenz in dieser Variable angelegt
		//Wenn zwei Objekte von Klassen miteinander verglichen werden, werden die Speicheradressen verglichen
		Program p = new Program(); //Wird im Arbeitsspeicher angelegt
		p.Zahl = 5;
		Program p2 = p; //Hier wird eine Referenz auf das Objekt unter p gelegt
		p2.Zahl = 10; //p und p2 haben den selben Wert

		Console.WriteLine(p.GetHashCode());
		Console.WriteLine(p2.GetHashCode());
		Console.WriteLine(p == p2);
		Console.WriteLine(p.GetHashCode() == p2.GetHashCode());

		//struct
		//Wertetyp
		//Wenn ein Objekt eines Structs auf eine Variable zugewiesen wird, wird eine Kopie in dieser Variable angelegt
		//Wenn zwei Objekte von Structs miteinander verglichen werden, werden die Inhalte verglichen
		int z = 5;
		int z2 = z; //Hier wird eine Kopie erzeugt
		z2 = 10; //z und z2 haben nicht den gleichen Wert

		Test(z); //Gilt auch bei Parametern

		//ref
		//Macht beliebige Variablen/Parameter/Rückgabewerte/... referenzierbar
		int r = 5;
		ref int r2 = ref r; //Referenz zw. den beiden Variablen
		r2 = 10; //Beide Variablen sind 10

		Test2(z: 5, x: 10);

		unsafe
		{
			int* n;
		}

		string tag;
		switch (DateTime.Now.DayOfWeek)
		{
			case DayOfWeek.Monday:
				tag = "Montag";
				break;
			case DayOfWeek.Tuesday:
				tag = "Dienstag";
				break;
			default:
				tag = "Anderer Tag";
				break;
		}

		string tag2 = DateTime.Now.DayOfWeek switch
		{
			DayOfWeek.Monday => "Montag",
			DayOfWeek.Tuesday => "Dienstag",
			_ => "Anderer Tag"
		};

		List<int> zahlen = null;
		if (zahlen == null)
			zahlen = new List<int>();

		zahlen = zahlen == null ? new List<int>() : zahlen;

		zahlen = zahlen ?? new List<int>();

		zahlen ??= new List<int>();

		zahlen ??= new(); //C# 9

		zahlen ??= []; //C# 12

		//String-Interpolation ($-String): Code in einen String einzubetten
		string output = "a: " + a + ", o: " + o + ", z: " + z;
		string output2 = $"a: {a * 2}, o: {o}, z: {ZahlAusgabe(z)}, {DateTime.Now.DayOfWeek switch
		{
			DayOfWeek.Monday => "Montag",
			DayOfWeek.Tuesday => "Dienstag",
			_ => "Anderer Tag"
		}}";
		Console.WriteLine(output);
		Console.WriteLine(output2);

		//Verbatim-String (@-String): String, welcher Escape-Sequenzen ignoriert
		string pfad = @"C:\Program Files\dotnet\shared\Microsoft.NETCore.App\9.0.5\System.Console.dll"; //Windows Pfade werden hier nicht als Escape-Sequenzen interpretiert
		string pfad2 = "C:\\Program Files\\dotnet\\shared\\Microsoft.NETCore.App\\9.0.5\\System.Console.dll";

		switch (DateTime.Now.DayOfWeek)
		{
			case >= DayOfWeek.Monday and <= DayOfWeek.Friday:
				break;
			case DayOfWeek.Saturday or DayOfWeek.Sunday:
				break;
		}

		Person person = new Person(0, "Max", "Mustermann");
		Console.WriteLine(person.ID);
		Console.WriteLine(person.Vorname);
		Console.WriteLine(person.Nachname);
		Console.WriteLine(person);

		int id;
		string vn;
		string nn;
		(id, vn, nn) = person;

		//Point punkt = new Point(1, 2, 3);
		//foreach (int k in punkt)
		//{

		//}
	}

	public static void Test(int x) => x *= 2;

	public static void Test2(int x = 0, int y = 0, int z = 0)
	{

	}

	public static string ZahlAusgabe(int x) => x switch
	{
		1 => "Eins",
		2 => "Zwei",
		_ => "Andere Zahl",
	};
}

public record Person(int ID, string Vorname, string Nachname)
{
	public void Test()
	{
		//...
	}
}

public class Point// : IEnumerable<int>
{
	public int X;

	public int Y;

	public int Z;

	public Point(int x, int y, int z)
	{
		X = x;
		Y = y;
		Z = z;
	}
}

public class Point2(int x, int y, int z)
{
	public int X = x;

	public int Y = y;

	public int Z = z;
}