using System;
using HtmlAgilityPack;

namespace Scrapper;
internal class Program
{
    static void Main(string[] args)
    {
    	string city = "new-york";
    	bool ShowHtml = false;

		foreach(string arg in args)
		{
			if(arg == "-C" || arg == "--city")
			{
				Console.WriteLine("Please tell a city");
				city = Console.ReadLine();
			}

			if(arg == "--html")
				ShowHtml = true;

			if(arg == "--help" || arg == "-help")
			{
				Console.WriteLine("A climate seacher program, comands: ");
				Console.WriteLine("	-C or --city: defines a city to search for ");
				Console.WriteLine("	--html: Prints entire page html ");
				return;
			}
		}

		var Weather = GetWeather(city);

		Console.WriteLine("City:\n" + Weather.cityState + "\n\nDay, Hour:\n" + Weather.dayHourClimate + "\n\nTemperature:\n" + Weather.temperature);

		if(ShowHtml == true)Console.WriteLine("Html: \n" + Weather.html);
    }

    static (string html, string dayHourClimate, string temperature, string cityState) GetWeather(string city)
    {
    	string url = "https://www.google.com/search?q=clima+em+" + city;

    	var httpClient = new HttpClient();
    	var html = httpClient.GetStringAsync(url).Result;

    	var htmlDocument = new HtmlDocument();
    	htmlDocument.LoadHtml(html);

    	var temperatureElement = htmlDocument.DocumentNode.SelectSingleNode("//div[@class='BNeawe iBp4i AP7Wnd']");
    	string temperature = temperatureElement.InnerText;

    	var dayHourClimateElement = htmlDocument.DocumentNode.SelectSingleNode("//div[@class='BNeawe tAd8D AP7Wnd']");
    	string dayHourClimate = dayHourClimateElement.InnerText;

    	var cityStateElement = htmlDocument.DocumentNode.SelectSingleNode("//span[@class='BNeawe tAd8D AP7Wnd']");
    	string cityState = cityStateElement.InnerText;

    	return (html, dayHourClimate, temperature, cityState);
    }
}