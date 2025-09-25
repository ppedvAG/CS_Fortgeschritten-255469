namespace Multitasking;

internal class _02_TaskMitParameter
{
	static void Main(string[] args)
	{
		Task t = new Task(Run, 200); //Parameter wird bei dem Konstruktor direkt hinzugefügt
		t.Start();
	}

	static void Run(object? o)
	{

	}
}
