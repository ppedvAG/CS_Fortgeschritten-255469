namespace Events;

internal class Component
{
	public event EventHandler Start;

	public event EventHandler End;

	public event EventHandler<ProgressEventArgs> Progress;

	/// <summary>
	/// Diese Methode soll eine längerandauernde Arbeit simulieren
	/// </summary>
	public void DoWork()
	{
		Start?.Invoke(this, EventArgs.Empty);

		for (int i = 0; i < 10; i++)
		{
			Progress?.Invoke(this, new ProgressEventArgs(i));
			Thread.Sleep(100);
		}

		End?.Invoke(this, EventArgs.Empty);
	}
}

public class ProgressEventArgs(int progress) : EventArgs
{
	public int Progress { get; } = progress;
}