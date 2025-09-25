namespace Events;

internal class User
{
	static void Main(string[] args)
	{
		Component comp = new Component();
		comp.Start += Comp_Start;
		comp.End += Comp_End;
		comp.Progress += Comp_Progress;
		comp.DoWork();
	}

	private static void Comp_Progress(object? sender, ProgressEventArgs e) => Console.WriteLine($"Fortschritt: {e.Progress}");

	private static void Comp_End(object? sender, EventArgs e) => Console.WriteLine("Prozess beendet");

	private static void Comp_Start(object? sender, EventArgs e) => Console.WriteLine("Prozess gestartet");
}
