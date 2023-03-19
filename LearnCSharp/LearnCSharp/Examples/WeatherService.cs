using LearnCSharp.Model;
using System.Collections.Concurrent;

namespace LearnCSharp.Examples
{
    /// <summary>
    /// An example about using ValueTask.
    /// </summary>
    public class WeatherService
    {
        /*
         * Benefits of ValueTask over Task are results of it being a struct type.
         * structs are stored on the stack rather than the heap, and they are automatically cleaned up when they go out of scope.
         * As a result, ValueTask significantly reduces the memory pressure on the garbage collector.
         * Moreover, in a scenario where the hot path in our code executes synchronously, it is better to use ValueTask instead of Task.
         * The hot path is a section of our code that executes frequently.
         */

        private readonly ConcurrentDictionary<string, Weather> _cache;

        public WeatherService()
        {
            _cache = new();
        }

        private static async Task<Weather> Get(string city)
        {
            await Task.Delay(10);
            var weather = new Weather
            {
                City = city,
                Date = DateTime.Now,
                AvgTempratureF = new Random().Next(5, 70)
            };

            return weather;
        }

        private static void LogTaskStatus(TaskStatus status)
        {
            Console.WriteLine($"Task Status: {Enum.GetName(typeof(TaskStatus), status)}");
        }

        public static async Task CheckTaskStatus()
        {
            var task = Get("Stockholm");
            LogTaskStatus(task.Status);
            await task;
            LogTaskStatus(task.Status);
        }

        public async Task<Weather> GetWeatherTask(string city)
        {
            if (!_cache.ContainsKey(city))
            {
                var weather = await Get(city);
                _cache.TryAdd(city, weather);
            }
            return _cache[city];
        }

        public async ValueTask<Weather> GetWeatherValueTask(string city)
        {
            if (!_cache.ContainsKey(city))
            {
                var weather = await Get(city);
                _cache.TryAdd(city, weather);
            }
            return _cache[city];
        }
    }
}
