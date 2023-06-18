using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using Newtonsoft.Json.Linq;
using RestaurantDesk.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Formats.Asn1;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Windows.Controls;
using System.Windows.Input;
using Wpf.Ui.Common.Interfaces;
using Menu = RestaurantDesk.Models.Menu;

namespace RestaurantDesk.ViewModels
{
    public partial class SettingsViewModel : ObservableObject, INavigationAware
    {
        private bool _isInitialized = false;
        private string baseAddress;

        [ObservableProperty]
        private string _appVersion = String.Empty;

        [ObservableProperty]
        private string fileName;

        [ObservableProperty]
        private bool isProgress;

        [ObservableProperty]
        private Wpf.Ui.Appearance.ThemeType _currentTheme = Wpf.Ui.Appearance.ThemeType.Unknown;

        public void OnNavigatedTo()
        {
            if (!_isInitialized)
            {
                InitializeViewModel();
                baseAddress = $"{ConfigurationManager.AppSettings["ApiBaseAddress"]}";
            }
        }

        public void OnNavigatedFrom()
        {
        }

        private void InitializeViewModel()
        {
            CurrentTheme = Wpf.Ui.Appearance.Theme.GetAppTheme();
            AppVersion = $"RestaurantDesk - {GetAssemblyVersion()}";

            _isInitialized = true;
        }

        private string GetAssemblyVersion()
        {
            return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? String.Empty;
        }

        [RelayCommand]
        private void Onupload()
        {
            Microsoft.Win32.OpenFileDialog openFileDlg = new Microsoft.Win32.OpenFileDialog();
            openFileDlg.DefaultExt = "csv";
            // Launch OpenFileDialog by calling ShowDialog method
            Nullable<bool> result = openFileDlg.ShowDialog();
            if (result == true)
            {
                this.FileName = openFileDlg.FileName;
                this.IsProgress = true;

                var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    MissingFieldFound = null,
                    HeaderValidated = null,

                };



                using (var reader = new StreamReader(this.FileName))
                using (var csv = new CsvReader(reader, configuration))
                {
                    //csv.Context.TypeConverterCache.AddConverter<bool>(new MyBooleanConverter());
                    var records = csv.GetRecords<Menu>().ToList();

                    using (HttpClient httpClient = new HttpClient())
                    {
                        httpClient.BaseAddress = new Uri(baseAddress);
                        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                        // var encodedContent = new JsonContent(records);

                        records.ForEach(x => x.MenuId = 0);
                        HttpResponseMessage response = httpClient.PostAsJsonAsync("api/MenuBulkInsert", records).Result;
                        if (response.IsSuccessStatusCode)
                        {
                           
                        }
                        else
                        {

                        }
                    }
                }
                this.IsProgress = false;

            }
        }
    }

    public class MyBooleanConverter : DefaultTypeConverter
    {

        public override object? ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
        {
            if (text == null)
            {
                return string.Empty;
            }
            var boolValue = bool.Parse(text);
            return boolValue;
            // return base.ConvertFromString(text, row, memberMapData);
        }

        public override string? ConvertToString(object? value, IWriterRow row, MemberMapData memberMapData)
        {
            return base.ConvertToString(value, row, memberMapData);
        }
    }
}
