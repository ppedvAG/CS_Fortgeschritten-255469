namespace Events;

internal class Events
{
	public event EventHandler TestEvent; //Entwicklerseite

	public event EventHandler<CustomEventArgs> CustomEvent;

	public event Action<string> StringEvent;

	private event Action accessorEvent;

	public event Action AccessorEvent
	{
		add
		{
			//Prüfe, ob noch kein Event angehängt ist
			if (accessorEvent == null)
				accessorEvent += value;
		}
		remove => accessorEvent -= value;
	}

	static void Main(string[] args) => new Events().Start();

	public void Start()
	{
		TestEvent += Events_TestEvent; //Anwenderseite
		TestEvent?.Invoke(this, EventArgs.Empty); //Entwicklerseite

		//////////////////////////////////////

		CustomEvent += Events_CustomEvent;
		CustomEvent?.Invoke(this, new CustomEventArgs(10)); //WICHTIG: Null Check

		//////////////////////////////////////

		StringEvent += Events_StringEvent;
		StringEvent?.Invoke("Hallo");
	}

	private void Events_TestEvent(object? sender, EventArgs e)
	{
		Console.WriteLine("TestEvent ausgeführt");
	}

	private void Events_CustomEvent(object? sender, CustomEventArgs e)
	{
		Console.WriteLine($"Zahl: {e.Zahl}");
	}

	private void Events_StringEvent(string obj)
	{
		Console.WriteLine(obj);
	}
}

public class CustomEventArgs(int zahl) : EventArgs
{
	public int Zahl { get; init; } = zahl;
}