using System.Collections;

namespace Generics;

internal class Program
{
	static void Main(string[] args)
	{
		//Generics
		//Platzhalter für Typen
		//Werden bei Instanzierung des Objektes/Aufruf der Methode durch einen konkreten Typen ersetzt
		List<int> ints = new List<int>(); //T = int
		ints.Add(1); //T wird durch int ausgetauscht

		DataStore<int> ds = [];
		ds[0] = 10; //Wegen Indexer möglich
		foreach (int i in ds) //Wegen IEnumerable möglich
		{

		}

		Test(0);
	}

	static void Test<T>(T obj)
	{
		switch (obj)
		{
			case int x: break;
		}

		//Keywords
		//default: Gibt den Standardwert hinter dem Typen zurück
		Console.WriteLine(default(T));

		//typeof
		Console.WriteLine(typeof(T));

		//nameof: Gibt den Namen des Typens hinter T als String zurück
		Console.WriteLine(nameof(T));
	}
}

public class DataStore<T> : IEnumerable<T>
{
	private T[] _data;

	public List<T> Data => _data.ToList();

	public void Add(T item, int index)
	{
		_data[index] = item;
	}

	public T GetValue(int index)
	{
		return _data[index];
	}

	public IEnumerator<T> GetEnumerator()
	{
		foreach (T x in _data)
		{
			yield return x;
		}
		//yield return: Gibt das derzeitige Element zurück
		//Muss in einer Methode verwendet werden, welche einen Enumerator zurückgibt
	}

	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

	public T this[int index]
	{
		get => _data[index];
		set => _data[index] = value;
	}
}
