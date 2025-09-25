using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace Serialisierung;

internal class Program
{
	static void Main(string[] args)
	{
		string folderPath = "Output";

		string filePath = Path.Combine(folderPath, "Fahrzeuge.xml");

		if (!Directory.Exists(folderPath))
			Directory.CreateDirectory(folderPath);

		List<Fahrzeug> fahrzeuge =
		[
			new PKW(251, FahrzeugMarke.BMW),
			new Fahrzeug(274, FahrzeugMarke.BMW),
			new Fahrzeug(146, FahrzeugMarke.BMW),
			new Fahrzeug(208, FahrzeugMarke.Audi),
			new Fahrzeug(189, FahrzeugMarke.Audi),
			new Fahrzeug(133, FahrzeugMarke.VW),
			new Fahrzeug(253, FahrzeugMarke.VW),
			new Fahrzeug(304, FahrzeugMarke.BMW),
			new Fahrzeug(151, FahrzeugMarke.VW),
			new Fahrzeug(250, FahrzeugMarke.VW),
			new Fahrzeug(217, FahrzeugMarke.Audi),
			new Fahrzeug(125, FahrzeugMarke.Audi)
		];

		//1. Serialisieren/Deserialisieren
		//2. Attribute
		//3. Xml per Hand

		//1. Serialisieren/Deserialisieren
		XmlSerializer xml = new XmlSerializer(fahrzeuge.GetType());
		using (StreamWriter sw = new StreamWriter(filePath))
		{
			xml.Serialize(sw, fahrzeuge);
		}

		using (StreamReader sr = new StreamReader(filePath))
		{
			List<Fahrzeug> fzg = (List<Fahrzeug>) xml.Deserialize(sr);
		}

		//2. Attribute
		//XmlIgnore
		//XmlAttribute: Definiert, dass das Feld als Attribut geschrieben werden soll
		//XmlInclude: Vererbung

		//3. Xml per Hand
		XmlDocument doc = new XmlDocument();
		doc.Load(filePath);
		foreach (XmlNode node in doc.DocumentElement)
		{
			int maxV = int.Parse(node.Attributes["MaxV"].InnerText);
			FahrzeugMarke marke = Enum.Parse<FahrzeugMarke>(node.Attributes["Marke"].InnerText);
			Console.WriteLine($"MaxV: {maxV}, Marke: {marke}");
		}
	}

	public static void SystemJson()
	{
		//string folderPath = "Output";

		//string filePath = Path.Combine(folderPath, "Fahrzeuge.json");

		//if (!Directory.Exists(folderPath))
		//	Directory.CreateDirectory(folderPath);

		//List<Fahrzeug> fahrzeuge =
		//[
		//	new PKW(251, FahrzeugMarke.BMW),
		//	new Fahrzeug(274, FahrzeugMarke.BMW),
		//	new Fahrzeug(146, FahrzeugMarke.BMW),
		//	new Fahrzeug(208, FahrzeugMarke.Audi),
		//	new Fahrzeug(189, FahrzeugMarke.Audi),
		//	new Fahrzeug(133, FahrzeugMarke.VW),
		//	new Fahrzeug(253, FahrzeugMarke.VW),
		//	new Fahrzeug(304, FahrzeugMarke.BMW),
		//	new Fahrzeug(151, FahrzeugMarke.VW),
		//	new Fahrzeug(250, FahrzeugMarke.VW),
		//	new Fahrzeug(217, FahrzeugMarke.Audi),
		//	new Fahrzeug(125, FahrzeugMarke.Audi)
		//];

		////1. Serialisieren/Deserialisieren
		////2. Options/Settings
		////3. Attribute
		////4. Json per Hand

		////1. Serialisieren/Deserialisieren
		////string json = JsonSerializer.Serialize(fahrzeuge);
		////File.WriteAllText(filePath, json);

		////string readJson = File.ReadAllText(filePath);
		////Fahrzeug[] readFzg = JsonSerializer.Deserialize<Fahrzeug[]>(readJson);

		////2. Options/Settings
		//JsonSerializerOptions options = new JsonSerializerOptions(); //WICHTIG: Beim Serialisieren/Deserialisieren müssen die Options mitgegeben werden
		//options.WriteIndented = true;

		//string json2 = JsonSerializer.Serialize(fahrzeuge, options);
		//File.WriteAllText(filePath, json2);

		//string readJson2 = File.ReadAllText(filePath);
		//Fahrzeug[] readFzg2 = JsonSerializer.Deserialize<Fahrzeug[]>(readJson2, options);

		////3. Attribute
		////JsonIgnore: Ignoriert das Feld
		////JsonPropertyName/JsonPropertyOrder: Benennt ein Feld um, schreibt die Felder in einer anderen Reihenfolge
		////JsonExtensionData: Fängt Felder, welche in der Datenklasse keine entsprechenden Properties haben
		////JsonDerivedType: Vererbung

		////4. Json per Hand
		//JsonDocument doc = JsonDocument.Parse(json2);
		//foreach (JsonElement element in doc.RootElement.EnumerateArray())
		//{
		//	int maxV = element.GetProperty("Maximalgeschwindigkeit").GetInt32();
		//	FahrzeugMarke marke = (FahrzeugMarke) element.GetProperty("Marke").GetInt32();
		//	Console.WriteLine($"MaxV: {maxV}, Marke: {marke}");
		//}
	}

	public static void NewtonsoftJson()
	{
		string folderPath = "Output";

		string filePath = Path.Combine(folderPath, "Fahrzeuge.json");

		if (!Directory.Exists(folderPath))
			Directory.CreateDirectory(folderPath);

		List<Fahrzeug> fahrzeuge =
		[
			new PKW(251, FahrzeugMarke.BMW),
			new Fahrzeug(274, FahrzeugMarke.BMW),
			new Fahrzeug(146, FahrzeugMarke.BMW),
			new Fahrzeug(208, FahrzeugMarke.Audi),
			new Fahrzeug(189, FahrzeugMarke.Audi),
			new Fahrzeug(133, FahrzeugMarke.VW),
			new Fahrzeug(253, FahrzeugMarke.VW),
			new Fahrzeug(304, FahrzeugMarke.BMW),
			new Fahrzeug(151, FahrzeugMarke.VW),
			new Fahrzeug(250, FahrzeugMarke.VW),
			new Fahrzeug(217, FahrzeugMarke.Audi),
			new Fahrzeug(125, FahrzeugMarke.Audi)
		];

		//1. Serialisieren/Deserialisieren
		//2. Options/Settings
		//3. Attribute
		//4. Json per Hand

		//1. Serialisieren/Deserialisieren
		string json = JsonConvert.SerializeObject(fahrzeuge);
		File.WriteAllText(filePath, json);

		string readJson = File.ReadAllText(filePath);
		Fahrzeug[] readFzg = JsonConvert.DeserializeObject<Fahrzeug[]>(readJson);

		//2. Options/Settings
		JsonSerializerSettings options = new(); //WICHTIG: Beim Serialisieren/Deserialisieren müssen die Options mitgegeben werden
		//options.Formatting = Formatting.Indented;
		options.TypeNameHandling = TypeNameHandling.Objects;

		string json2 = JsonConvert.SerializeObject(fahrzeuge, options);
		File.WriteAllText(filePath, json2);

		string readJson2 = File.ReadAllText(filePath);
		Fahrzeug[] readFzg2 = JsonConvert.DeserializeObject<Fahrzeug[]>(readJson2, options);

		//3. Attribute
		//JsonIgnore: Ignoriert das Feld
		//JsonProperty: Benennt ein Feld um, schreibt die Felder in einer anderen Reihenfolge, ...
		//JsonExtensionData: Fängt Felder, welche in der Datenklasse keine entsprechenden Properties haben
		//JsonDerivedType: Wird über die Settings gemacht (TypeNameHandling)

		//4. Json per Hand
		JToken doc = JToken.Parse(json2);
		foreach (JToken element in doc)
		{
			int maxV = element["Maximalgeschwindigkeit"].Value<int>();
			FahrzeugMarke marke = (FahrzeugMarke) element["Marke"].Value<int>();
			Console.WriteLine($"MaxV: {maxV}, Marke: {marke}");
		}
	}
}

[DebuggerDisplay("MaxV: {MaxV}, Marke: {Marke}")]

//[JsonDerivedType(typeof(Fahrzeug), "F")]
//[JsonDerivedType(typeof(PKW), "P")]

[XmlInclude(typeof(Fahrzeug))]
[XmlInclude(typeof(PKW))]
public class Fahrzeug
{
	//[JsonIgnore]
	//[JsonPropertyName("Maximalgeschwindigkeit")]
	[JsonProperty(PropertyName = "Maximalgeschwindigkeit")]
	[XmlAttribute]
	public int MaxV { get; set; }

	[XmlAttribute]
	public FahrzeugMarke Marke { get; set; }

	//[JsonExtensionData]
	//public Dictionary<string, object> ExtensionData { get; set; }

	public Fahrzeug(int maxV, FahrzeugMarke marke)
	{
		MaxV = maxV;
		Marke = marke;
	}

	public Fahrzeug() { }
}

public class PKW : Fahrzeug
{
	public PKW(int maxV, FahrzeugMarke marke) : base(maxV, marke)
	{
	}

	public PKW()
	{
		
	}
}

public enum FahrzeugMarke { Audi, BMW, VW }