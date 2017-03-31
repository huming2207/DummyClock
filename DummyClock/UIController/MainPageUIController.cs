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
using PtvCore.Timetable;
using System.Security.Cryptography;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace DummyClock.UIController
{
    public class MainPageUIController
    {
        public async Task<List<PtvTimetableBinding>> GetPtvInfo()
        {
            var client = new TimetableClient(
                Settings.PtvDeveloperID,
                Settings.PtvSecurityKey,
                (input, key) =>
                {
                    var provider = new HMACSHA1(key);
                    var hash = provider.ComputeHash(input);
                    return hash;
                });

            var searchResults = await client.SearchAsync(Settings.PtvStationName);
            var stopResults = (Stop)searchResults[0];

            var lineResults = await client.SearchLineByModeAsync(Settings.PtvLineName, TransportType.Train);
            var departResults = await client.GetBroadNextDeparturesAsync(TransportType.Train, stopResults.StopID, 3);

            var ptvBindingList = new List<PtvTimetableBinding>();

            // Use for loop here to limit the result amount to 3. 
            // In fact alothough I've set the amount limit, PTV API always returns more than 3.
            for(int i = 0; i <= 2; i++)
            {
                var departItem = departResults[i];
                ptvBindingList.Add(new PtvTimetableBinding()
                {
                    DetailedString = string.Format("to {0}", departItem.Platform.Direction.DirectionName),
                    TitleString = string.Format("{0} min ", ((int)((departItem.EstimatedTime.Value.ToLocalTime() - DateTime.Now).TotalMinutes)).ToString())
                });

               
            }

            if(ptvBindingList.Count == 0)
            {
                ptvBindingList.Add(new PtvTimetableBinding()
                {
                    DetailedString = string.Format(" {0}", "No data"),
                    TitleString = string.Format("{0}", "⁉️")
                });
            }

            return ptvBindingList;
        }

        public async Task<WeatherControlBindings> GetWeatherInfo()
        { 
            var yahooWeatherControl = new YahooWeatherControl(true);
            var yahooResult = await yahooWeatherControl.DoQuery("Melbourne", "Victoria");

            var forecastStatus = yahooResult.Results.Channel.Item.Forecast;
            var currentStatus = yahooResult.Results.Channel.Item.Condition;

            var weatherData = new WeatherControlBindings()
            {
                TodayTemp = currentStatus.Temperature.ToString() + "℃",
                TomorrowTemp = forecastStatus[1].Low.ToString() + "/" + forecastStatus[1].High.ToString() + "℃",
                TodayCondition = currentStatus.StatusText,
                TomorrowCondition = forecastStatus[1].StatusText,
                TodayEmoji = GetWeatherEmoji(currentStatus.Code),
                TomorrowEmoji = GetWeatherEmoji(forecastStatus[1].Code)
            };

            return weatherData;
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
            foreach (var timetableForToday in timetableResult.WeeklyTimetable[dayOfWeek].DailyTimetable)
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

            if(timetableListContent.Count == 0)
            {
                timetableListContent.Add(new RmitTimetableBindings()
                {
                    TitleString = "No class for today.",
                    DetailedString = "You've got a day off!"
                });
            }

            return timetableListContent;
        }

        private string GetWeatherEmoji(ConditionCode conditionCode)
        {
            // Grab the damn condition code and match 
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
                case ConditionCode.PartlyCloudyAtNight: // No "moon with cloud" emoji available lol
                case ConditionCode.Cloudy:
                    {
                        emoji = "☁️";
                        break;
                    }

                case ConditionCode.ClearAtNight:
                case ConditionCode.FairAtNight:
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
