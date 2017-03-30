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
using DummyClock.UIController;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace DummyClock
{
    
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private Timer RmiterTimetableTimer;
        public MainPage()
        {
            this.InitializeComponent();
            RmiterTimetableTimer = new Timer(RmitTimetableScheduleCallback, null, 0, (int)TimeSpan.FromHours(1).TotalMilliseconds);
        }

        public async void RmitTimetableScheduleCallback(object state)
        {
            MainPageUIController mainPageController = new MainPageUIController();
            var uniTimetableListContent = await mainPageController.GetUniTimetables(); 

            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
                () =>
                {
                    TimetableList.ItemsSource = uniTimetableListContent;
                });
        }


    }
}
