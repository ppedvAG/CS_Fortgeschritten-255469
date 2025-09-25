using PrimeComponent;

List<int> i = [1, 2, 3, 4, 5];
ForEach(i, Funktion);
IEnumerable<int> x = ForEachReturn2(i, MalZwei);

Component comp = new();
comp.Prime += Comp_Prime;
comp.NotPrime += Comp_NotPrime;
comp.Prime100 += Comp_Prime100;
comp.DoWork();

void Comp_Prime(object? sender, PrimeEventArgs e)
{
	Console.WriteLine($"Primzahl: {e.Zahl}");
}

void Comp_NotPrime(object? sender, NotPrimeEventArgs e)
{
	Console.WriteLine($"Keine Primzahl: {e.Zahl}, Teiler: {e.Teiler}");
}

void Comp_Prime100(object? sender, PrimeEventArgs e)
{
	Console.WriteLine($"Hundertste Primzahl: {e.Zahl}");
}




void ForEach<T>(IEnumerable<T> values, Action<T> action)
{
	if (values == null || action == null)
		throw new ArgumentNullException();

	foreach (T item in values)
		action(item);
}

void Funktion(int x)
{
	Console.WriteLine($"Zahl: {x}");
}

IEnumerable<R> ForEachReturn<T, R>(IEnumerable<T> values, Func<T, R> func)
{
	if (values == null || func == null)
		throw new ArgumentNullException();

	List<R> list = new List<R>();
	foreach (T item in values)
	{
		R value = func(item);
		list.Add(value);
	}
	return list;
}

IEnumerable<R> ForEachReturn2<T, R>(IEnumerable<T> values, Func<T, R> func)
{
	if (values == null || func == null)
		throw new ArgumentNullException();

	foreach (T item in values)
	{
		yield return func(item);
	}
}

int MalZwei(int x)
{
	return x * 2;
}