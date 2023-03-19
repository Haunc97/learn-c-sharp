using LearnCSharp.Examples;

ArgExpression.Example_1();
ArgExpression.Example_2();

var weatherService = new WeatherService();
await WeatherService.CheckTaskStatus();
var weatherAtBangkok = await weatherService.GetWeatherTask("Bangkok");
var weatherAtViengChan = await weatherService.GetWeatherValueTask("ViengChan");
Console.WriteLine(weatherAtBangkok.AvgTempratureF);
Console.WriteLine(weatherAtViengChan.AvgTempratureF);