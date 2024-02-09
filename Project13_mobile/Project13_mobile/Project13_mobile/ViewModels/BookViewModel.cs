using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Project13_mobile.Models;
using Xamarin.Forms;
using Xamarin.Essentials;
using Project13_mobile.APIs;
using Project13_mobile.View;
using System.Threading.Tasks;

namespace Project13_mobile.ViewModels
{
    class BookViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Command SelectCommand { get; }
        public Book selectBook { get; set; }

        public Command DeleteCommand { get; }
        public Command EditBookCommand { get; }
        public Command EditCommand { get; }
        public Command AddCommand { get; }
        public Command BackCommand { get; }
        public Command NextPageCommand { get; }
        public ObservableCollection<Book> book
        {
            get
            {
                return mybook;
            }
            set
            {
                mybook = value;
                var args = new PropertyChangedEventArgs(nameof(book));
                PropertyChanged?.Invoke(this, args);
            }


        }
        ObservableCollection<Book> mybook;

        APIservice aPIservice;

        public BookViewModel()
        {
            aPIservice = new APIservice();
            book = new ObservableCollection<Book>();
            GetBook();



            NextPageCommand = new Command(async () =>
            {

                await Application.Current.MainPage.Navigation.PushAsync(new View.AddBookPage());
            });

            SelectCommand = new Command(async () =>
            {
                var sendVar = new { selectBook = selectBook, BackCommand = BackCommand , DeleteCommand = DeleteCommand , EditCommand = EditCommand };
                var BookDetail = new View.BookDetail
                {
                    BindingContext = sendVar
                };
                await Application.Current.MainPage.Navigation.PushModalAsync(BookDetail);
            });

            BackCommand = new Command(async () =>
            {
                await Application.Current.MainPage.Navigation.PopModalAsync();
            });

            DeleteCommand = new Command(async () =>  
            
            {
                if (selectBook != null)
                {
                    // ทำการเรียกใช้เมธอดที่เราสร้างไว้เพื่อลบหนังสือ
                    bool isDeleted = await aPIservice.DeleteBook(selectBook.Book_Id);



                    if (isDeleted)
                    {
                        await Application.Current.MainPage.DisplayAlert("BOOK", "book Deleted", "OK");
                        Application.Current.MainPage = new TabbedPageBook() ;
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("BOOK", "Error  Deleted Try Again", "OK");
                    }
                }
            });

            EditCommand = new Command(async () => {
                var sendVar = new { selectBook = selectBook, BackCommand = BackCommand, EditBookCommand= EditBookCommand };
                var editBookPage = new View.EditBook(sendVar);

                await Application.Current.MainPage.Navigation.PushModalAsync(editBookPage);
            });

            EditBookCommand = new Command(async () =>
            {
                if (selectBook != null)
                {
                    // ทำการเรียกใช้ method สำหรับการแก้ไขหนังสือ
                    bool isEdited = await aPIservice.EditBook(selectBook.Book_Id,selectBook);

                    if (isEdited)
                    {
                        await Application.Current.MainPage.DisplayAlert("BOOK", "Book Edited", "OK");
                        Application.Current.MainPage = new TabbedPageBook();
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("BOOK", "Error Editing Book. Try Again", "OK");
                    }
                }
            });

        }


        async void GetBook()
        {
            book = await aPIservice.GetBook();
        }
        


    }
}