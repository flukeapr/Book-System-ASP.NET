using Project13_mobile.APIs;
using Project13_mobile.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace Project13_mobile.ViewModels
{
    class LoginViewModel : INotifyPropertyChanged
    {
        public Login login { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        APIservice aPIService;

        public string result;
        public Command LoginCommand { get; }
        public Command RegisterCommand { get; }


        public string Result
        {
            get => result;

            set
            {
                result = value;
                var arg = new PropertyChangedEventArgs(nameof(Result));
                PropertyChanged?.Invoke(this, arg);
            }
        }

        public LoginViewModel()
        {
            login = new Login();

            aPIService = new APIservice();

            LoginCommand = new Command(async () =>
            {
                var response = await aPIService.Login(login);
                if (response)
                {
                    
                    await Application.Current.MainPage.Navigation.PushAsync(new View.TabbedPageBook());
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Login", "Fail Login Please TryAgain", "OK");
                }
            });

            RegisterCommand = new Command(async () =>
            {

                await Application.Current.MainPage.Navigation.PushAsync(new View.RegisterPage());
            });
        }
    }
}
