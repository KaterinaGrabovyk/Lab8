using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Lab8.DB;
using Microsoft.EntityFrameworkCore;
using System.ServiceModel.Channels;
namespace Lab8
{

    public sealed partial class MainPage : Page
    {
        PlantDBContext db;
        public MainPage()
        {
            this.InitializeComponent();
            db = new PlantDBContext();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            db.Plants.Load();
        }
        private void button3_Click(object sender, RoutedEventArgs e)
        {

            listView.ItemsSource = db.Plants.Local.ToList();
        }
        private void button5_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxPK.Text != "" && textBoxUZ.Text != "" && textBoxPC.Text != "" && textBoxWN.Text != "" && textBoxDN.Text != "")
            {
                db.Add(new Plant
                {
                    PlantKind = textBoxPK.Text,
                    UmoviZrost = textBoxUZ.Text,
                    PeriodCvit = textBoxPC.Text,
                    WaterNeeds = textBoxWN.Text,
                    DobrivaNeeds = textBoxDN.Text
                });
                db.SaveChanges();
                listView.ItemsSource = db.Plants.Local.ToList();
                textBoxPK.Text = "";
                textBoxUZ.Text = "";
                textBoxPC.Text = "";
                textBoxWN.Text = "";
                textBoxDN.Text = "";

            }
            else
            {
                ContentDialog cont = new ContentDialog
                {
                    Title = "Порожньо",
                    Content = "Ви не ввели всю потрібну інформацію.",
                    CloseButtonText = "Ok"
                };
                cont.ShowAsync();
            }
        }
        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var line = listView.SelectedItem as Plant;
            if (line != null)
            {
                var entity = db.Find<Plant>(line.Id);
                if (entity != null)
                {
                    textBoxPK.Text = entity.PlantKind;
                    textBoxUZ.Text = entity.UmoviZrost;
                    textBoxPC.Text = entity.PeriodCvit;
                    textBoxWN.Text = entity.WaterNeeds;
                    textBoxDN.Text = entity.DobrivaNeeds;

                }
            }
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            var entity = db.Find<Plant>((listView.SelectedItem as Plant).Id);
            if (entity != null)
            {
                db.Remove(entity);
                db.SaveChanges();
                listView.ItemsSource = db.Plants.Local.ToList();
                textBoxPK.Text = "";
                textBoxUZ.Text = "";
                textBoxPC.Text = "";
                textBoxWN.Text = "";
                textBoxDN.Text = "";
            }

        }
        
    }
}
