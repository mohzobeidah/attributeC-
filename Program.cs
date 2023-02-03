using System;

namespace ReportingReflection
{
    class Program
    {
        static void Main()
        {
            new CSV2Genertor<Book>(BookData.Books, "Book1s").Generator();
            new CSV2Genertor<Weather>(WeatherData.Weather, "Weather1").Generator();
        }
    }
}
