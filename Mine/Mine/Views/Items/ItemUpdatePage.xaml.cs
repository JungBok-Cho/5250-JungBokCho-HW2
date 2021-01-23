using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Mine.ViewModels;
using Mine.Models;

namespace Mine.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class ItemUpdatePage : ContentPage
    {
        public ItemModel Item { get; set; }

        public ItemUpdatePage()
        {
            InitializeComponent();

            Item = new ItemModel
            {
                Text = "Item name",
                Description = "This is an item description."
            };

            BindingContext = this;
        }

        /// <summary>
        /// Contructor that takes a ViewModel
        /// </summary>
        /// <param name="viewModel"></param>
        public ItemUpdatePage(ItemReadViewModel viewModel)
        {
            InitializeComponent();
            Item = viewModel.Item;

            BindingContext = this;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "UpdateItem", Item);
            await Navigation.PopModalAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        void Value_OnStepperValueChanged(object sender, ValueChangedEventArgs e) 
        {
            ValueValue.Text = String.Format("{0}", e.NewValue);
        }
    }
}