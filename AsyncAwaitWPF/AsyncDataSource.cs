namespace AsyncAwaitWPF;

public class AsyncDataSource
{
	public async IAsyncEnumerable<int> Generate()
	{
		while (true)
		{
			await Task.Delay(Random.Shared.Next(200, 2000));
			yield return Random.Shared.Next();
		}
	}
}
