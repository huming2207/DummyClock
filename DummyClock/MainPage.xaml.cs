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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace DummyClock
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            var RmiterTimetableTimer = new Timer(RmitTimetableScheduleCallback, null, 5000, (int)TimeSpan.FromHours(1).TotalMilliseconds);
        }

        public async void RmitTimetableScheduleCallback(object state)
        {
            // Initialize login object then login
            var casLogin = new CasLogin();
            var casResult = await casLogin.RunCasLogin(Settings.RmitID, Settings.RmitPassword);

            // Initialize portal stuff
            var portal = new MyRmitPortal(casResult.CasCookieContainer);
            var timetableResult = await portal.GetCurrentClassTimetable();

            // Get the time. In this array, Monday is 0, Sunday is 6.
            int dayOfWeek = (int)((DateTime.Now.DayOfWeek) + 6) % 7; // Shift the day of week
            var timetableListContent = new List<UIBindings.TimetableBindings>();

            // Set to 
            foreach (var timetableForToday in timetableResult.WeeklyTimetable[0].DailyTimetable)
            {
                var tableContent = new UIBindings.TimetableBindings()
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

            await Window.Current.CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
                () =>
                {
                    TimetableList.ItemsSource = timetableListContent;
                });
        }
    }
}
