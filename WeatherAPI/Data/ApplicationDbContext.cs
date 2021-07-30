using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MySqlConnector;
using WeatherAPI.Models;

namespace WeatherAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        private const string ConnStr = "server=127.0.0.1;port=3306;user=root;password=0000;database=db";

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            this.Database.EnsureCreated();
        }

        public static Weather RetrieveData(string request)
        {
            Weather response = new Weather();

            using (MySqlConnection con = new MySqlConnection(ConnStr))
            {
                con.Open();

                MySqlCommand select = new MySqlCommand($"select * from db.Weather where local = '{request}'", con);

                MySqlDataReader reader = select.ExecuteReader();

                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        response.Local = reader.GetString("Local");
                        response.Temp.Min = reader.GetDouble("Temp_min");
                        response.Temp.Max = reader.GetDouble("temp_max");
                        response.Temp.Now = reader.GetDouble("temp_now");

                        return response;
                    }
                }
                return null;
            }
        }

        public static void StoreData(Weather weather)
        {
            string createTable = @"CREATE TABLE IF NOT EXISTS Weather(
                                Local VARCHAR(25),
                                Temp_min DOUBLE(100,2),
                                Temp_max DOUBLE(100,2),
                                Temp_now DOUBLE(100,2),
                                time_created DATETIME DEFAULT NOW());";

            string createEvent = @"CREATE EVENT IF NOT EXISTS CacheHandler
                                ON SCHEDULE EVERY 2 MINUTE
                                ON COMPLETION PRESERVE
                                DO
                                DELETE FROM db.Weather Where time_created < (NOW() - interval 20 minute);";

            // Handle double with comma separation
            System.Globalization.NumberFormatInfo nfi = new System.Globalization.NumberFormatInfo();
            nfi.NumberDecimalSeparator = ".";

            string data = $"insert into Weather (Local, Temp_min, Temp_max, Temp_now) " +
                $"values ('{weather.Local}', {weather.Temp.Min.ToString(nfi)}, {weather.Temp.Max.ToString(nfi)}, {weather.Temp.Now.ToString(nfi)})";


            using (MySqlConnection con = new MySqlConnection(ConnStr))
            {
                con.Open();

                new MySqlCommand("SET SQL_SAFE_UPDATES = 0;");
                MySqlCommand table = new MySqlCommand(createTable, con);
                MySqlCommand cache = new MySqlCommand(createEvent, con);
                MySqlCommand write = new MySqlCommand(data, con);

                table.ExecuteNonQuery();
                cache.ExecuteNonQuery();
                write.ExecuteNonQuery();

                con.Close();
            }

        }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseMySql("server=127.0.0.1;port=3306;user=root;password=0000;database=db", new MySqlServerVersion("8.0.26"))
                .UseLoggerFactory(LoggerFactory.Create(b => b
                    .AddConsole()
                    .AddFilter(level => level >= LogLevel.Information)))
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
        }
    }
}
