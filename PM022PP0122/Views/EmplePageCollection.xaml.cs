﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PM022PP0122.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EmplePageCollection : ContentPage
    {
        public EmplePageCollection()
        {
            InitializeComponent();
        }

        private void ListEmple_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}