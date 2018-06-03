﻿using MeteoMobile.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MeteoMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePageMaster : ContentPage
    {
        public ListView ListView;

        public HomePageMaster()
        {
            InitializeComponent();

            BindingContext = new HomePageMasterViewModel();
            ListView = MenuItemsListView;
            if (DateTime.Now >= Settings.AccessTokenExpirationDate)
                Application.Current.MainPage = new LoginPage();
            ocpImage.Source = ImageSource.FromResource("MeteoMobile.Images.ocp_logo.png");
        }

        class HomePageMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<HomePageMenuItem> MenuItems { get; set; }
            
            public HomePageMasterViewModel()
            {
                MenuItems = new ObservableCollection<HomePageMenuItem>(new[]
                {
                    new HomePageMenuItem { Id = 0, Title = "Acceuil", TargetType=typeof(HomePageDetail) },
                    new HomePageMenuItem { Id = 1, Title = "Profile", TargetType=typeof(ProfilePageDetail) },
                    new HomePageMenuItem { Id = 2, Title = "Utilisateurs" , TargetType=typeof(UsersPageDetail)},
                    new HomePageMenuItem { Id = 3, Title = "Graphes" , TargetType=typeof(StatisticsTabbedPageDetail)},
                    new HomePageMenuItem { Id = 4, Title = "Se déconnecter" , TargetType=typeof(LogoutPage)},
                });
                if(Constants.MyUser.Role != "Administrateur")
                     MenuItems.RemoveAt(2);
            }
            
            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}