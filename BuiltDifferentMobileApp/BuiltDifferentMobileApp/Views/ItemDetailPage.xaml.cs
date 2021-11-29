using BuiltDifferentMobileApp.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace BuiltDifferentMobileApp.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}