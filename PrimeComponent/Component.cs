namespace PrimeComponent;

public class Component
{
	public event EventHandler<PrimeEventArgs> Prime;

	public event EventHandler<PrimeEventArgs> Prime100;

	public event EventHandler<NotPrimeEventArgs> NotPrime;

	public void DoWork()
	{
		int primeCounter = 1; //Prime100
		for (int i = 0; ; i++)
		{
			bool isPrime = CheckPrime(i, out int t);
			if (isPrime)
			{
				if (primeCounter % 100 == 0)
					Prime100?.Invoke(this, new PrimeEventArgs(i));
				else
					Prime?.Invoke(this, new PrimeEventArgs(i));
				primeCounter++;

				Thread.Sleep(100);
			}
			else
				NotPrime?.Invoke(this, new NotPrimeEventArgs(i, t));
		}
	}

	public bool CheckPrime(int num, out int teiler)
	{
		teiler = 0;

		//Prüfe, ob die Zahl (num) gerade ist
		if (num % 2 == 0)
		{
			//NotPrime?.Invoke(this, new NotPrimeEventArgs(num, 2));
			teiler = 2;
			return false; //Keine Primzahl
		}

		//Prüfe, ob die Zahl (num) durch eine ungerade Zahl von 3 bis zur Hälfte der gegebenen Zahl teilbar ist
		for (int i = 3; i <= num / 2; i += 2)
		{
			if (num % i == 0)
			{
				//NotPrime?.Invoke(this, new NotPrimeEventArgs(num, i));
				teiler = i;
				return false; //Keine Primzahl
			}
		}
		return true; //Wenn alle Prüfungen false ergeben, haben ist der Parameter "num" eine Primzahl
	}
}

public class PrimeEventArgs(int zahl) : EventArgs
{
	public int Zahl { get; } = zahl;
}

public class NotPrimeEventArgs(int zahl, int teiler) : EventArgs
{
	public int Zahl { get; } = zahl;

	public int Teiler { get; } = teiler;
}