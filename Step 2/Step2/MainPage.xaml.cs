using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Step2.Services;
using Xamarin.Forms;

namespace Step2
{
    public partial class MainPage : ContentPage
    {
        int count = 0;
        public int Count
        {
            get { return count; }
            set
            {
                if (count != value)
                {
                    count = value;
                    OnPropertyChanged();
                }
            }
        }

        IWatch watch;

        public MainPage()
        {
            InitializeComponent();

            BindingContext = this;

            watch = DependencyService.Get<IWatch>();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            watch.Activate();
            watch.ValueUpdated += Watch_ValueUpdated;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            watch.Deactivate();
            watch.ValueUpdated -= Watch_ValueUpdated;
        }

        void Watch_ValueUpdated(int value)
        {
            Count = value;
        }

        void Dec_Clicked(Object sender, EventArgs e)
        {
            --Count;
            SyncToWatch();
        }

        void Inc_Clicked(Object sender, EventArgs e)
        {
            ++Count;
            SyncToWatch();
        }

        void SyncToWatch()
        {
            watch.SendValue(count);
        }
    }
}
