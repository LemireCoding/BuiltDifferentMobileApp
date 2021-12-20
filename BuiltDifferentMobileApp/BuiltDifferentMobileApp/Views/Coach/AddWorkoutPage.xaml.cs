﻿using BuiltDifferentMobileApp.ViewModels;
using BuiltDifferentMobileApp.ViewModels.Coach;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BuiltDifferentMobileApp.Views.Coach
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddWorkoutPage : ContentPage
    {
        public AddWorkoutPage()
        {
            InitializeComponent();
            BindingContext = new AddWorkoutViewModel();
        }
    }
}