namespace Step3;

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

    public MainPage()
	{
		InitializeComponent();

		BindingContext = this;
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
        //watch.SendValue(count);
    }
}

