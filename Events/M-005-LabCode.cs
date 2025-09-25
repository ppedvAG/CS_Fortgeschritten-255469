/// <summary>
/// Diese Methode kann f�r das dritte Event bearbeitet werden
/// </summary>
public bool CheckPrime(int num)
{
	//Pr�fe, ob die Zahl (num) gerade ist
	if (num % 2 == 0)
	{
		return false; //Keine Primzahl
	}

	//Pr�fe, ob die Zahl (num) durch eine ungerade Zahl von 3 bis zur H�lfte der gegebenen Zahl teilbar ist
	for (int i = 3; i <= num / 2; i += 2)
	{
		if (num % i == 0)
		{
			return false; //Keine Primzahl
		}
	}
	return true; //Wenn alle Pr�fungen false ergeben, haben ist der Parameter "num" eine Primzahl
}