using Project13_mobile.APIs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace Project13_mobile.ViewModels
{
    class LogoutViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public Command LogoutCommand { get; }

        APIservice  aPIservice;

        public LogoutViewModel()
        {
            aPIservice = new APIservice();

            LogoutCommand = new Command(async () => {


                var response = await aPIservice.Logout();
                if (response)
                {
                    Application.Current.MainPage = new NavigationPage(new View.LoginPage());
                }



            });
        }
    }
}
