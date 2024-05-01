using System;
using HtmlAgilityPack;

namespace Scrapper;
internal class Program
{
    static void Main(string[] args)
    {
    	string city = "sao+carlos";
    	bool ShowHtml = false;

		foreach(string arg in args)
		{
			Console.WriteLine("Verify args");

			if(arg == "-C" || arg == "--city")
			{
				Console.WriteLine("Please tell a city");
				city = Console.ReadLine();
			}

			if(arg == "--html")
				ShowHtml = true;
		}

		string html = GetWeather(city);

		if(ShowHtml == true)Console.WriteLine("Html: \n" + html);
    }

    static string GetWeather(string city)
    {
    	string url = "https://www.google.com/search?q=clima+em+" + city;

    	var httpClient = new HttpClient();
    	var html = httpClient.GetStringAsync(url).Result;

    	var htmlDocument = new HtmlDocument();
    	htmlDocument.LoadHtml(html);

    	var temperatureElement = htmlDocument.DocumentNode.SelectSingleNode("//div[@class='BNeawe iBp4i AP7Wnd']");
    	var temperature = temperatureElement.InnerText;

    	var dayHourClimateElement = htmlDocument.DocumentNode.SelectSingleNode("//div[@class='BNeawe tAd8D AP7Wnd']");
    	var dayHourClimate = dayHourClimateElement.InnerText;

    	Console.WriteLine(dayHourClimate + '\n');
    	Console.WriteLine(temperature);

    	return html;
    }
}