using System.Net.NetworkInformation;
using System.Reflection;

namespace Reflection;

internal class Program
{
	public int Zahl { get; set; }

	private int GeheimeZahl = 3;

	static void Main(string[] args)
	{
		//Reflection
		//Über Type-Objekte indirekt mit objects interagieren
		//-> Meta Informationen über das Objekt herausfinden

		//Type
		Program p = new Program();

		Type pt = p.GetType();

		Type to = typeof(Program);

		//Informationen finden
		to.GetMethods(); //MethodInfo[]

		to.GetProperties(); //PropertyInfo[]

		to.GetEvents(); //EventInfo[]

		//...

		//////////////////////////////////////
		
		//Objekte erstellen
		object o = Activator.CreateInstance(pt);

		pt.GetProperty("Zahl").SetValue(o, 123); //Mit Objekt indirekt interagieren

		Console.WriteLine(pt.GetProperty("Zahl").GetValue(o));

		Console.WriteLine(pt.GetField("GeheimeZahl", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(o));

		//////////////////////////////////////

		//Assembly
		//Code Block (Projekt)
		Assembly a = Assembly.GetExecutingAssembly(); //Das derzeitige Projekt

		string path = @"C:\Users\lk3\source\repos\CSharp_Fortgeschritten_2025_09_24\Events\bin\Debug\net9.0\Events.dll";

		Assembly e = Assembly.LoadFrom(path);

		Type compType = e.GetType("Events.Component");

		object comp = Activator.CreateInstance(compType);

		EventHandler start = new EventHandler((sender, args) => Console.WriteLine("Prozess gestartet"));
		compType.GetEvent("Start").AddEventHandler(comp, start);

		compType.GetMethod("DoWork").Invoke(comp, null);
	}
}
