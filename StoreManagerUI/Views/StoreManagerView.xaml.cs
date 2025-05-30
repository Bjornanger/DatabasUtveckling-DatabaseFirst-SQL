using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using CommonModels.Models;
using CommonModels.Services;
using Labb2_DbFirst_Template.Entities;

namespace StoreManagerUI.Views
{
    /// <summary>
    /// Interaction logic for StoreManagerView.xaml
    /// </summary>
    public partial class StoreManagerView : INotifyPropertyChanged
    {

        private readonly BookRepository _bookRepository;
        private readonly StoreRepository _storeRepository;
        private readonly InventoryRepository _inventoryRepository;
        private readonly AuthorRepository _authorRepository;


        public ObservableCollection<StoreModel> StoreList { get; set; } = new();
        private StoreModel _storeSelected;
        public StoreModel StoreSelected
        {
            get { return _storeSelected; }
            set
            {
                if (_storeSelected != value)
                {
                    _storeSelected = value;
                    OnPropertyChanged();
                }
            }

        }



        public ObservableCollection<BookModel> BooksFromSelectedStore { get; set; } = new();
        private BookModel _bookSelected;
        public BookModel BookSelected //Denna är för att markera en bok från den valda affärens inventory, för att TA BORT.
        {
            get { return _bookSelected; }
            set
            {
                if (_bookSelected != value)
                {
                    _bookSelected = value;
                    OnPropertyChanged();
                }
            }

        }

        public event Action BooksFromSelectedStoreView;



        public ObservableCollection<BookModel> MainInventoryList { get; set; } = new();
        private BookModel _bookSelectedFromMainList;
        public BookModel BookSelectedFromMainList
        {
            get { return _bookSelectedFromMainList; }
            set
            {
                if (_bookSelectedFromMainList != value)
                {
                    _bookSelectedFromMainList = value;
                    OnPropertyChanged();
                }
            }

        }//Denna är för att lägga till ytterligare böcker från Mainlagret till vald Affär.ADDBtn
        public StoreManagerView()
        {
            InitializeComponent();

            DataContext = this;

            _bookRepository = new BookRepository();
            _storeRepository = new StoreRepository();
            _inventoryRepository = new InventoryRepository();
            _authorRepository = new AuthorRepository();

            var allStores = _storeRepository.GetAllStores();

            foreach (var store in allStores)
            {
                StoreList.Add(store);
            }


            var books = _bookRepository.GetAllBooks();

            foreach (var book in books)
            {
                MainInventoryList.Add(book);
            }

            BooksFromSelectedStoreView += StoreManagerView_BooksFromSelectedStoreView;
        }






        private void StoreManagerView_BooksFromSelectedStoreView()
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(BooksFromSelectedStore);
            view.Refresh();
        }


        private void
            AddToStoreBtn_OnClick(object sender, RoutedEventArgs e) //Gör så att de läggs till på varandra. check på Isbn
        {
          
            if ( BookSelectedFromMainList is null)
            {
                return;
            }

            var storeToChange = StoreSelected.Id;

            var bookFromMainList = BookSelectedFromMainList.Isbn13;

            var bookToAdd = _bookRepository.GetBookByIsbn(bookFromMainList);

            var bookToIncrease = BooksFromSelectedStore.FirstOrDefault(d => d.Isbn13 == bookToAdd.Isbn13);


            if (BooksFromSelectedStore.Any(b => b.Isbn13 == bookToAdd.Isbn13))
            {
                bookToIncrease.Amount++;
                _bookRepository.UpdateInventoryByStoreId(bookToIncrease, storeToChange);
                BooksFromSelectedStoreView();
            }
            else if (!BooksFromSelectedStore.Contains(BookSelectedFromMainList))
            {
                BookSelectedFromMainList.Amount = 1;
                BooksFromSelectedStore.Add(BookSelectedFromMainList);
                _bookRepository.UpdateInventoryByStoreId(bookToAdd, storeToChange);
                BooksFromSelectedStoreView();

            }


            ICollectionView view = CollectionViewSource.GetDefaultView(BooksFromSelectedStore);
            view.Refresh();



        }

        private void RemoveFromStoreBtn_OnClick(object sender, RoutedEventArgs e)
        {
            if (BookSelected is null)
            {
                return;
            }
            var storeToChange = StoreSelected.Id;

            var bookFromMainList = BookSelected.Isbn13;

            var bookToAdd = _bookRepository.GetBookByIsbn(bookFromMainList);

            var bookToDecrease = BooksFromSelectedStore.FirstOrDefault(d => d.Isbn13 == bookToAdd.Isbn13);


            if (BooksFromSelectedStore.Any(b => b.Isbn13 == bookToAdd.Isbn13))
            {
                bookToDecrease.Amount--;
                _bookRepository.UpdateInventoryByStoreId(bookToDecrease, storeToChange);
                BooksFromSelectedStoreView();
            }
            else if (BooksFromSelectedStore.Contains(BookSelectedFromMainList))
            {
                BookSelectedFromMainList.Amount = 0;
                BooksFromSelectedStore.Remove(BookSelectedFromMainList);
                _bookRepository.UpdateInventoryByStoreId(bookToDecrease, storeToChange);
                BooksFromSelectedStoreView();

            }
            
            ICollectionView view = CollectionViewSource.GetDefaultView(BooksFromSelectedStore);
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


        private void StoreListView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (StoreSelected is null)
            {
                return;
            }
        }

        private void ShowStoreInventoryBtn_OnClick(object sender, RoutedEventArgs e)
        {
            if (StoreSelected != null)
            {
                BooksFromSelectedStore.Clear();
                int selectedStoreFromList = StoreSelected.Id;

                var getInventoryFromStoreId = _inventoryRepository.GetInventoryByStoreId(selectedStoreFromList);

                if (getInventoryFromStoreId is null)
                {
                    return;
                }
                var getBooksFromStore = _bookRepository.GetAllBooks();

                var matchingBooks = getBooksFromStore.Where(book =>
                    getInventoryFromStoreId.Any(invent => book.Isbn13 == invent.Isbn13));

                foreach (var book in matchingBooks)
                {
                    BooksFromSelectedStore.Add(book);
                }
                ICollectionView view = CollectionViewSource.GetDefaultView(BooksFromSelectedStore);
                view.Refresh();
            }
        }

        private void FullInventory_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BookSelectedFromMainList is null)
            {
                return;
            }
        }


    }
}
