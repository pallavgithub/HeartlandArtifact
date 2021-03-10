using HeartlandArtifact.ViewModels;
using System;
using System.Collections.Generic;
using System.Timers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HeartlandArtifact.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EnterOtpPage : ContentPage
    {
        private List<Entry> _entries; 
        Timer T = new Timer();
        int Minutes = 2;
        int Seconds = 0;
        public EnterOtpPage()
        {
            InitializeComponent();
            _entries = new List<Entry>();
            _entries.Add(Entry_one);
            _entries.Add(Entry_two);
            _entries.Add(Entry_three);
            _entries.Add(Entry_four);
            T.Interval = 1000;
            T.Elapsed += T_Tick;
            T.Start();
        }
        void T_Tick(object sender, ElapsedEventArgs e)
        {
            if (Minutes != -1)
            {
                if (Seconds == 0)
                {
                    Seconds = 59;
                    Minutes--;
                }
                else
                {
                    Seconds--;
                }
                (BindingContext as EnterOtpPageViewModel).TimerText = $"{Minutes}:{Seconds.ToString("D2")}";
            }
            if (Minutes == -1)
            {
                T.Stop();
                (BindingContext as EnterOtpPageViewModel).TimerText = "0:00";
            }
        }

        private void RestartTimer()
        {
            T.Stop();
            Minutes = 2;
            Seconds = 0;
            (BindingContext as EnterOtpPageViewModel).TimerText = "2:00";
            T.Start();
        }

        private void Entry_one_TextChanged(object sender, TextChangedEventArgs e)
        {
            var text = (sender as Entry).Text;
            if (text.Length == 1 && !string.IsNullOrEmpty(text))
            {
                if (text != " ")
                    Entry_two.Focus();
            }
        }

        private void Entry_two_TextChanged(object sender, TextChangedEventArgs e)
        {
            var text = (sender as Entry).Text;
            if (text.Length == 1 && !string.IsNullOrEmpty(text))
            {
                if (text != " ")
                    Entry_three.Focus();
            }
            else
            {
                var previousEntry = GetPreviousEntry(sender as Entry);
                if (previousEntry != null)
                {
                    previousEntry.Focus();
                }
            }
        }

        private void Entry_three_TextChanged(object sender, TextChangedEventArgs e)
        {
            var text = (sender as Entry).Text;
            if (text.Length == 1 && !string.IsNullOrEmpty(text))
            {
                if (text != " ")
                    Entry_four.Focus();
            }
            else
            {
                var previousEntry = GetPreviousEntry(sender as Entry);
                if (previousEntry != null)
                {
                    previousEntry.Focus();
                }
            }
        }

        private void Entry_four_TextChanged(object sender, TextChangedEventArgs e)
        {
            var text = (sender as Entry).Text;
            if (string.IsNullOrEmpty(text))
            {
                var previousEntry = GetPreviousEntry(sender as Entry);
                if (previousEntry != null)
                {
                    previousEntry.Focus();
                }
            }
        }
        private void Entry_OnBackspace(object arg1, EventArgs arg2)
        {
            var previousEntry = GetPreviousEntry(arg1 as Entry);
            if (previousEntry != null)
            {
                previousEntry.Focus();
            }
        }
        public Entry GetPreviousEntry(Entry currentEntry)
        {
            var indexOfCurrentEntry = _entries.IndexOf(currentEntry);
            if (indexOfCurrentEntry == 0)
            {
                return null;
            }
            var previousEntry = _entries[indexOfCurrentEntry - 1];
            return previousEntry;
        }
        protected override bool OnBackButtonPressed() => true;

        private void ResendOtp_Tapped(object sender, EventArgs e)
        {
            RestartTimer();
            (BindingContext as EnterOtpPageViewModel).ResendOtp();
        }
    }
}