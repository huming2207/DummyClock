using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using RmiterCoreUwp;
using RmiterCoreUwp.MyRmit;
using Windows.ApplicationModel.Core;
using System.Globalization;
using DummyClock.UIController;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace DummyClock
{
    
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private Timer TimetableTimer;
        private Timer ClockTimer;
        private Timer PtvTimer;
        public MainPage()
        {
            this.InitializeComponent();
            TimetableTimer = new Timer(TimetableScheduleCallback, null, 0, (int)TimeSpan.FromHours(1).TotalMilliseconds);
            ClockTimer = new Timer(ClockUpdateCallback, null, 0, (int)TimeSpan.FromSeconds(1).TotalMilliseconds);
            PtvTimer = new Timer(PtvTimetableCallback, null, 0, (int)TimeSpan.FromMinutes(1).TotalMilliseconds);
        }

        public async void PtvTimetableCallback(object state)
        {
            var mainPageController = new MainPageUIController();
            var ptvTimetableBindingList = await mainPageController.GetPtvInfo();

            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Low,
                () =>
                {
                    PtvList.ItemsSource = ptvTimetableBindingList;
                });
        }

        public async void ClockUpdateCallback(object state)
        {
            var chineseCalendar = new ChineseLunisolarCalendar();
            var currentDateTime = DateTime.Now;
            string currentHour = string.Empty;
            string currentMinute = string.Empty;

            // Current 24h-formatted time
            string currentTime = currentDateTime.ToString("HH:mm");

            // Current date information
            string currentDate = currentDateTime.ToString("dddd, MMMM dd, yyyy");

            // Current Chinese lunar calendar information
            string currentLunarCalendar = "农历 " +
                chineseCalendar.GetMonth(currentDateTime).ToString() + "月" +
                chineseCalendar.GetDayOfMonth(currentDateTime).ToString() + "日";

            // Push to UI
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Low,
                ()=>
                {
                    MainTimeTextblock.Text = currentTime;
                    DateTextbox.Text = currentDate;
                    LunarCalendarTextbox.Text = currentLunarCalendar;
                });
        }

        public async void TimetableScheduleCallback(object state)
        {
            // This timer updates weather information and RMIT timetable from myRMIT.
            //
            // Currently weather section is working without any data binding. 
            // I've tried before by using GridView but somehow the UI didn't work as I expected.
            // So the following code is very messy. I'll try again later.

            var mainPageController = new MainPageUIController();
            var uniTimetableListContent = await mainPageController.GetUniTimetables();
            var weatherTextContent = await mainPageController.GetWeatherInfo();

            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
                () =>
                {
                    CurrentWeatherTempTextbox.Text = weatherTextContent.TodayTemp;
                    CurrentWeatherIconTextbox.Text = weatherTextContent.TodayEmoji;
                    CurrentWeatherDetailedTextbox.Text = weatherTextContent.TodayCondition;
                    TomorrowWeatherDetailedTextbox.Text = weatherTextContent.TomorrowCondition;
                    TomorrowWeatherIconTextbox.Text = weatherTextContent.TomorrowEmoji;
                    TomorrowWeatherTempTextbox.Text = weatherTextContent.TomorrowTemp;
                    TimetableList.ItemsSource = uniTimetableListContent;
                });
        }


    }
}
