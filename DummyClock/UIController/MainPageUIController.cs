using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RmiterCoreUwp;
using RmiterCoreUwp.MyRmit;
using DummyClock.UIBindings;
using YahooWeatherParser.Uwp;
using YahooWeatherParser.Shared;

namespace DummyClock.UIController
{
    public class MainPageUIController
    {
        public async Task<List<WeatherControlBindings>> GetWeatherInfo()
        {
            var ywasync = new YahooWeatherControl(true);
            var result = await ywasync.DoQuery("Melbourne", "Victoria");

            var forecastStatus = result.Results.Channel.Item.Forecast;
            var currentStatus = result.Results.Channel.Item.Condition;

            string currentTemp = currentStatus.Temperature.ToString();
            string tomorrowForecast = forecastStatus[1].High.ToString() + "/" + forecastStatus[1].Low.ToString() + "℃";

            return null;
        }

        public async Task<List<RmitTimetableBindings>> GetUniTimetables()
        {
            // Initialize login object then login
            var casLogin = new CasLogin();
            var casResult = await casLogin.RunCasLogin(Settings.RmitID, Settings.RmitPassword);

            // Initialize portal stuff
            var portal = new MyRmitPortal(casResult.CasCookieContainer);
            var timetableResult = await portal.GetCurrentClassTimetable();

            // Get the time. In this array, Monday is 0, Sunday is 6.
            int dayOfWeek = (int)((DateTime.Now.DayOfWeek) + 6) % 7; // Shift the day of week
            var timetableListContent = new List<RmitTimetableBindings>();

            // Set to 
            foreach (var timetableForToday in timetableResult.WeeklyTimetable[0].DailyTimetable)
            {
                var tableContent = new RmitTimetableBindings()
                {
                    TitleString = string.Format("{0} - {1}", timetableForToday.Title, timetableForToday.ActivityType),
                    DetailedString = string.Format("{0}{1}, from {2} to {3}",
                        timetableForToday.Subject,
                        timetableForToday.CatalogNumber,
                        timetableForToday.StartDisplayable,
                        timetableForToday.EndDisplayable)
                };

                timetableListContent.Add(tableContent);
            }

            return timetableListContent;
        }

        private string GetWeatherEmoji(ConditionCode conditionCode)
        {
            string emoji = string.Empty;

            switch(conditionCode)
            {
                case ConditionCode.Sunny:
                case ConditionCode.FairAtDay:
                    {
                        emoji = "☀️";
                        break;
                    }

                case ConditionCode.PartlyCloudy:
                case ConditionCode.PartlyCloudyAtDay:
                    {
                        emoji = "🌤️";
                        break;
                    }

                case ConditionCode.MostlyCloudyAtDay:
                case ConditionCode.MostlyCloudyAtNight:
                case ConditionCode.Cloudy:
                    {
                        emoji = "☁️";
                        break;
                    }

                case ConditionCode.ClearAtNight:
                    {
                        emoji = "🌙";
                        break;
                    }

                case ConditionCode.Tornado:
                case ConditionCode.TropicalStorm:
                case ConditionCode.Hurricane:
                    {
                        emoji = "🌀";
                        break;
                    }

                case ConditionCode.Thundershowers:
                case ConditionCode.Thunderstorms:
                case ConditionCode.IsolatedThundershowers:
                case ConditionCode.IsolatedThunderstorms:
                case ConditionCode.ScatteredThunderstorms:
                case ConditionCode.ScatteredThunderstormsConditionTwo:
                case ConditionCode.SevereThunderstorms:
                    {
                        emoji = "⛈️";
                        break;
                    }

                case ConditionCode.MixedRainAndHail:
                case ConditionCode.MixedRainAndSleet:
                case ConditionCode.MixedRainAndSnow:
                case ConditionCode.Sleet:
                case ConditionCode.MixedSnowAndSleet:
                case ConditionCode.HeavySnow:
                case ConditionCode.SnowFlurries:
                case ConditionCode.SnowShowers:
                case ConditionCode.ScatteredSnowShowers:
                case ConditionCode.HeavySnowConditionTwo:
                case ConditionCode.BlowingSnow:
                    {
                        emoji = "🌨️";
                        break;
                    }

                case ConditionCode.Snow:
                case ConditionCode.Cold:
                case ConditionCode.LightSnowShowers:
                case ConditionCode.Hail:
                    {
                        emoji = "❄️";
                        break; 
                    }

                case ConditionCode.Hot:
                    {
                        emoji = "♨️";
                        break;
                    }

                case ConditionCode.Haze:
                case ConditionCode.Foggy:
                case ConditionCode.Dust:
                    {
                        emoji = "🌫️";
                        break;
                    }

                case ConditionCode.Windy:
                case ConditionCode.Blustery:
                    {
                        emoji = "💨";
                        break;
                    }

                case ConditionCode.Drizzle:
                case ConditionCode.FreezingDrizzle:
                case ConditionCode.FreezingRain:
                    {
                        emoji = "🌧️";
                        break;
                    }

                default:
                    {
                        emoji = "⁉️";
                        break;
                    }
            }

            return emoji;
        }
    }
}
