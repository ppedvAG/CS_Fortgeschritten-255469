namespace Delegates;

internal class Delegates
{
	public delegate void Vorstellung(string Name);

	static void Main(string[] args)
	{
		//Delegates
		//Eigener Typ, welcher Methodenzeiger speichert
		//Das Delegate hat einen Methodenaufbau; dieser muss von den Methoden die angehängt werden sollen repliziert werden
		Vorstellung v = new Vorstellung(VorstellungDE); //Methodenzeiger in der Klammer (selbst ohne Klammern)
		v("Max");

		v += VorstellungEN; //Weitere Funktion anhängen
		v += VorstellungEN; //Weitere Funktion anhängen
		v += VorstellungEN; //Weitere Funktion anhängen
		v += VorstellungEN; //Weitere Funktion anhängen
		v("Tim");

		v -= VorstellungDE; //Methode abnehmen
		v -= VorstellungDE; //Ab dem 2. mal passiert hier nichts
		v -= VorstellungDE;
		v("Udo");

		v -= VorstellungEN;
		v -= VorstellungEN;
		v -= VorstellungEN;
		v -= VorstellungEN; //Wenn alle Methoden heruntergenommen werden, ist das Delegate null (kein leeres Delegate)
		//v("Max");

		if (v != null)
			v("Max");

		v?.Invoke("Max"); //Null propagation: Führe den Code nach dem Fragezeichen nur aus, wenn die Variable nicht null ist

		foreach (Delegate dg in v.GetInvocationList())
		{
			Console.WriteLine(dg.Method.Name);
		}
	}

	static void VorstellungDE(string Name)
	{
		Console.WriteLine($"Hallo mein Name ist {Name}");
	}

	static void VorstellungEN(string Name)
	{
		Console.WriteLine($"Hello my name is {Name}");
	}
}
