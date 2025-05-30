using CommonModels.Models;
using CommonModels.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StoreManagerUI.Views
{
    /// <summary>
    /// Interaction logic for InventoryView.xaml
    /// </summary>
    public partial class InventoryView : INotifyPropertyChanged
    {

        private readonly BookRepository _bookRepository;
        private readonly StoreRepository _storeRepository;
        private readonly InventoryRepository _inventoryRepository;
        private readonly AuthorRepository _authorRepository;



        public ObservableCollection<AuthorModel> AuthorList { get; set; } = new();
        private AuthorModel _authorSelected;

        public AuthorModel AuthorSelected
        {
            get { return _authorSelected; }
            set
            {
                if (_authorSelected != value)
                {
                    _authorSelected = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<BookModel> AuthorBookList { get; set; } = new();




        private string _insertISBN;

        public string InsertISBN
        {
            get { return _insertISBN; }
            set
            {
                if (_insertISBN != value)
                {
                    _insertISBN = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _insertTitle;

        public string InsertTitle
        {
            get { return _insertTitle; }
            set
            {
                if (_insertTitle != value)
                {
                    _insertTitle = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _insertLanguage;

        public string InsertLanguage
        {
            get { return _insertLanguage; }
            set
            {
                if (_insertLanguage != value)
                {
                    _insertLanguage = value;
                    OnPropertyChanged();
                }
            }
        }

        private double _insertPrice;

        public double InsertPrice
        {
            get { return _insertPrice; }
            set
            {
                if (_insertPrice != value)
                {
                    _insertPrice = value;
                    OnPropertyChanged();
                }
            }
        }

        private DateOnly _insertReleaseDate;

        public DateOnly InsertReleaseDate
        {
            get { return _insertReleaseDate; }
            set
            {
                if (_insertReleaseDate != value)
                {
                    _insertReleaseDate = value;
                    OnPropertyChanged();
                }
            }
        }






        public InventoryView()
        {
            InitializeComponent();

            DataContext = this;

            _bookRepository = new BookRepository();
            _storeRepository = new StoreRepository();
            _inventoryRepository = new InventoryRepository();
            _authorRepository = new AuthorRepository();

            var allAuthors = _authorRepository?.GetAllAuthors();
            
            foreach (var author in allAuthors)
            {
                AuthorList.Add(author);
            }
            
        }

     

        private void AuthorListInView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AuthorSelected is null)
            {
                return;
            }
        }

        private void ShowAuthorBtn_OnClick(object sender, RoutedEventArgs e)
        {
            if (AuthorSelected is null)
            {
                return;
            }

            if (AuthorSelected != null)
            {
                AuthorBookList.Clear();

                int authorIdFromView = AuthorSelected.Id;
               

                var AuthorListToShow = _bookRepository.GetBookByAuthor(authorIdFromView);

                foreach (var book in AuthorListToShow)
                {
                    AuthorBookList.Add(book);
                }



            }
            ICollectionView view = CollectionViewSource.GetDefaultView(AuthorBookList);
                view.Refresh();

        }
        private void SaveBtn_OnClick(object sender, RoutedEventArgs e)
        {

           

            if (IsbnBox.Text is null || TitleBox.Text is null || PriceBox.Text is null || LanguageBox.Text is null || ReleaseDateBox.Text is null)
            {
                return;
            }

            if (IsbnBox.Text.Length == 13 )
            {
                InsertISBN = IsbnBox.Text;
            }
            else
            {
                return;
            }
            
            var bookBoxController = double.TryParse(PriceBox.Text, out double newBookPrice);
            
            var dateController = DateOnly.TryParse(ReleaseDateBox.Text, out var dateOut);

            InsertTitle = TitleBox.Text;
            InsertPrice = newBookPrice;
            InsertLanguage = LanguageBox.Text;
            InsertReleaseDate = dateOut;

            var addToAuthor = AuthorSelected.Id;//markerade Author ifrån listan.

            
            if (AuthorBookList.Any(b => b.Isbn13 == InsertISBN))
            {
                MessageBox.Show("This ISBN already exists");
            }

            var newBook = new BookModel()
            {
                Isbn13 = InsertISBN,
                Title = InsertTitle,
                Price = newBookPrice,
                Language = LanguageBox.Text,
                ReleaseDate = dateOut,
              

            };
         
           AuthorBookList.Add(_bookRepository.AddNewBook(newBook, addToAuthor));

           ICollectionView view = CollectionViewSource.GetDefaultView(AuthorBookList);
           view.Refresh();





        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        
    }
}
