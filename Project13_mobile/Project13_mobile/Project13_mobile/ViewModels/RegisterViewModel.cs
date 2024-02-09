using Project13_mobile.APIs;
using Project13_mobile.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace Project13_mobile.ViewModels
{
    class RegisterViewModel : INotifyPropertyChanged
    {
        public Command RegisterCommand { get; }
        public Command BackCommand { get; }

        APIservice aPIservice;
        public Register register { get; set; }

        public string result;


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


        public RegisterViewModel()
        {
            register = new Register();

            aPIservice = new APIservice();

            RegisterCommand = new Command(async () =>
            {


                var response = await aPIservice.Register(register);

                if (response)
                {
                    await Application.Current.MainPage.DisplayAlert("Register", "Register success!!!", "OK");
                    Application.Current.MainPage = new View.LoginPage();
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Register", "faillll!!!", "OK");
                }

            });




            BackCommand = new Command(async () => {


                await Application.Current.MainPage.Navigation.PopAsync();



            });

        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
    
    }

