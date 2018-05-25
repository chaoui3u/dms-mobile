﻿using MeteoMobile.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MeteoMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UsersPageDetail : ContentPage
    {
        //public ObservableCollection<string> Items { get; set; }
        UsersViewModel vm = new UsersViewModel();
        public UsersPageDetail()
        {
            InitializeComponent();

            //         Items = new ObservableCollection<string>
            //         {
            //             "Item 1",
            //             "Item 2",
            //             "Item 3",
            //             "Item 4",
            //             "Item 5"
            //         };
 
            vm.GetUsersCommand.Execute(null);
     
        }

        protected override async void OnAppearing()
        {
            await PutTaskDelay(3000);
            if(vm.Users == null)
            await PutTaskDelay(1000);
            MyListView.ItemsSource = vm.Users;
        }
        async Task PutTaskDelay(int delay)
        {
            await Task.Delay(delay);
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            await DisplayAlert("Item Tapped", "An item was tapped.", "OK");

            //Deselect Item
            ((ListView)sender).SelectedItem = null;

 
        }


    }
}
