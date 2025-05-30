using System;
using System.Collections.Generic;
using System.Linq;
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

namespace Main.Views
{
    /// <summary>
    /// Interaction logic for StoreView.xaml
    /// </summary>
    public partial class StoreView : Page
    {
        public StoreView()
        {
            InitializeComponent();
        }

        private void AddToStoreBtn_OnClick(object sender, RoutedEventArgs e)
        {

            //lägga till från main inventory
            throw new NotImplementedException();
        }

        private void RemoveFromStoreBtn_OnClick(object sender, RoutedEventArgs e)
        {

            //Ta bort från affärens inventory
            throw new NotImplementedException();
        }

        private void ShowStoreInventoryBtn_OnClick(object sender, RoutedEventArgs e)
        {
            //en refresh på den valda affärens inventory
            throw new NotImplementedException();
        }

        private void StoreInventory_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            throw new NotImplementedException();
            //I den makerade Stores inventory, vad som ska tas bort från affären.
        }

        private void FullInventory_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //i Main Inventory - vad som ska läggas till i affären
            throw new NotImplementedException();
        }
    }
}
