namespace Step3;

using Services;

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

    WatchService watch;

    public MainPage()
	{
		InitializeComponent();

		BindingContext = this;

        watch = new WatchService();
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

    void Watch_ValueUpdated(int value)
    {
        Count = value;
    }

    void SyncToWatch()
    {
        watch.SendValue(count);
    }
}

