using Project13_mobile.APIs;
using Project13_mobile.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace Project13_mobile.ViewModels
{
    class AddBokViewModel : INotifyPropertyChanged
    {
        APIservice aPIservice;
        public Command AddCommand { get; }

        public string bookname;
        public string category;
        public string shorts;
        public string author;
        public string publisher;
        public decimal price;
        public string image;


        public string BookName
        {
            get => bookname;

            set
            {
                bookname = value;
                var arg = new PropertyChangedEventArgs(nameof(BookName));
                PropertyChanged?.Invoke(this, arg);
            }
        }
        public string Category
        {
            get => category;

            set
            {
                category = value;
                var arg = new PropertyChangedEventArgs(nameof(Category));
                PropertyChanged?.Invoke(this, arg);
            }
        }
        public string ShortDescription
        {
            get => shorts;

            set
            {
                shorts = value;
                var arg = new PropertyChangedEventArgs(nameof(ShortDescription));
                PropertyChanged?.Invoke(this, arg);
            }
        }
        public string Author
        {
            get => author;

            set
            {
                author = value;
                var arg = new PropertyChangedEventArgs(nameof(Author));
                PropertyChanged?.Invoke(this, arg);
            }
        }
        public string Publisher
        {
            get => publisher;

            set
            {
                publisher = value;
                var arg = new PropertyChangedEventArgs(nameof(Publisher));
                PropertyChanged?.Invoke(this, arg);
            }
        }
        public decimal Price
        {
            get => price;

            set
            {
                if (price != value)
                {
                    price = value;
                    var arg = new PropertyChangedEventArgs(nameof(Price));
                    PropertyChanged?.Invoke(this, arg);
                }
            }
        }
        public string Image
        {
            get => image;

            set
            {
                image = value;
                var arg = new PropertyChangedEventArgs(nameof(Image));
                PropertyChanged?.Invoke(this, arg);
            }
        }

        public AddBokViewModel()
        {
            aPIservice = new APIservice();

            AddCommand = new Command(async () =>
            {
                Book book = new Book();
                
                book.Book_Name = BookName;
                book.Category = Category;
                book.Short_Story = ShortDescription;
                book.Author = Author;
                book.Publisher = Publisher;
                book.Price = Price;
                book.Image = Image;

                var response = await aPIservice.AddBook(book);
                if (response)
                {
                    await Application.Current.MainPage.DisplayAlert("BOOK", "book added!!", "OK");
                    Application.Current.MainPage = new View.TabbedPageBook();
                }
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
