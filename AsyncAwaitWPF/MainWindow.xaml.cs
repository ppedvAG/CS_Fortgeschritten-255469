using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;

namespace AsyncAwaitWPF;

public partial class MainWindow : Window
{
	public MainWindow()
	{
		InitializeComponent();
	}

	private void Button_Click(object sender, RoutedEventArgs e)
	{
		for (int i = 0; i < 100; i++)
		{
			Thread.Sleep(25);
			Output.Text += i + "\n";
			Scroll.ScrollToEnd();
		}
	}

	private void Button_Click_TaskRun(object sender, RoutedEventArgs e)
	{
		Task.Run(() =>
		{
			for (int i = 0; i < 100; i++)
			{
				Thread.Sleep(25);
				Dispatcher.Invoke(() => //Dispatcher: Legt Code auf den Main Thread
				{
					Output.Text += i + "\n";
					Scroll.ScrollToEnd();
				});
			}
		});
	}

	private async void Button_Click_Async(object sender, RoutedEventArgs e)
	{
		for (int i = 0; i < 100; i++)
		{
			await Task.Delay(25);
			Output.Text += i + "\n";
			Scroll.ScrollToEnd();
		}
	}

	private async void Request(object sender, RoutedEventArgs e)
	{
		string url = "http://www.gutenberg.org/files/54700/54700-0.txt";

		using HttpClient client = new HttpClient();

		//Aufgabe starten
		Task<HttpResponseMessage> response = client.GetAsync(url);

		//Zwischenschritte
		Output.Text = "Text wird geladen...";
		ReqButton.IsEnabled = false;

		//Warten
		HttpResponseMessage msg = await response;

		if (msg.IsSuccessStatusCode)
		{
			//Aufgabe starten
			Task<string> str = msg.Content.ReadAsStringAsync();

			//Zwischenschritte
			Output.Text = "Text wird angezeigt...";

			//Warten
			string text = await str;

			Output.Text = "";

			//Text schreiben asynchron
			int length = text.Length;
			int teile = 50;
			IEnumerable<string> chunks = text.Chunk(length / teile).Select(e => new string(e));
			foreach (string chunk in chunks)
			{
				Output.Text += chunk;
				await Task.Delay(25);
			}

			//Output.Text = text;
		}

		ReqButton.IsEnabled = true;
	}

	private async void Button_Click_AsyncDataSource(object sender, RoutedEventArgs e)
	{
		AsyncDataSource ads = new();
		await foreach (int item in ads.Generate())
		{
			Output.Text += item + "\n";
		}
	}
}