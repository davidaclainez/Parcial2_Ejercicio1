using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;

namespace Ejercicio1
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        public class restCountries
        {
            public string name { get; set; }
            public List<string> topLevelDomain { get; set; }
            public string alpha2Code { get; set; }
            public string alpha3Code { get; set; }
            public List<string> callingCodes { get; set; }
            public string capital { get; set; }
            public List<string> altSpellings { get; set; }
            public string region { get; set; }
        }

        private async void btnRefresh_Clicked(object sender, EventArgs e)
        {
            var name = cboRegion.Items[cboRegion.SelectedIndex];
            var customUri = String.Concat("http://api.countrylayer.com/v2/region/", name, "?access_key=a457f0e02955cbfb8e3d559157850e23");
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri(customUri);
            System.Console.WriteLine("---------------");
            System.Console.WriteLine(customUri);
            System.Console.WriteLine("---------------");
            //request.Headers.Add("Accpet", "application/json");
            request.Method = HttpMethod.Get;
            var client = new HttpClient();
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {

                string content = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<List<restCountries>>(content);
                listCountries.ItemsSource = resultado;
            }
        }

        private void listCountries_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var details = e.Item as restCountries;
            Page page = new Page1();
            page.BindingContext = details;
            Navigation.PushAsync(page);
        }
    }
}
