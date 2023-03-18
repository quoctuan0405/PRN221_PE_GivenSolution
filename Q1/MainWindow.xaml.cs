using Microsoft.Win32;
using Q1.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;

namespace Q1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static List<Star> Stars = new List<Star>();
        PE_PRN_Fall22B1Context context = new PE_PRN_Fall22B1Context();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var InputStarName = starName.Text;
            var InputGender = (male.IsChecked==true ? true : false);
            var InputDateOfBirth = dob;
            var InputDescription = description.Text;

            var nationalityyy = (ComboBoxItem)nationality.SelectedItem;
            var InputNationality = nationalityyy.Content.ToString();
            Stars.Add(new Star()
            {
                FullName = InputStarName,
                Male = InputGender,
                Dob = DateTime.Parse(InputDateOfBirth.ToString()),
                Description = InputDescription,
                Nationality = InputNationality
            });
            Load();
        }
        public void Load()
        {
            lvStars.ItemsSource = null;
            lvStars.ItemsSource = Stars;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();

            if (open.ShowDialog() == true)
            {
                var stream = open.OpenFile();

                StreamReader reader = new StreamReader(stream);

                var line = reader.ReadToEnd();
                var list = JsonSerializer.Deserialize<List<Star>>(line);

                Stars.AddRange(list);
                Load();
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Stars.Count != 0)
                {
                    context.AddRange(Stars);
                    context.SaveChanges();
                    MessageBox.Show("Save successfully.");
                }
                else
                {
                    MessageBox.Show("Add fail.");
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Save fail.");
            }
        }
    }
}
