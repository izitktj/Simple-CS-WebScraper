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

    static /* (string html, string dayHourClimate, string temperature, string cityState) */ CityInfo GetWeather(string city)
    {
    	string url = "https://www.google.com/search?q=clima+em+" + city;

    	var httpClient = new HttpClient();
    	var html = httpClient.GetStringAsync(url).Result;

    	var htmlDocument = new HtmlDocument();
    	htmlDocument.LoadHtml(html);

		CityInfo Info;

		try
		{
			var temperatureElement = htmlDocument.DocumentNode.SelectSingleNode("//div[@class='BNeawe iBp4i AP7Wnd']");
    		Info.temperature = temperatureElement.InnerText;

			var dayHourClimateElement = htmlDocument.DocumentNode.SelectSingleNode("//div[@class='BNeawe tAd8D AP7Wnd']");
			Info.dayHourClimate = dayHourClimateElement.InnerText;

			var cityStateElement = htmlDocument.DocumentNode.SelectSingleNode("//span[@class='BNeawe tAd8D AP7Wnd']");
			Info.cityState = cityStateElement.InnerText;
		} catch (Exception e)
		{
			Console.WriteLine("Exception while searching, please report to developers.\nException: " + e);
			return Info.error = true;
		}

    	

    	return Info;
    }

	public struct CityInfo
	{
		string html, dayHourClimate, temperature, cityState;
		bool error = false;
	}
}