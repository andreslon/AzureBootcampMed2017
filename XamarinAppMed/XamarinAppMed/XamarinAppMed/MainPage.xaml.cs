using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamarinAppMed
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BtnEnviarDatos.Clicked += BtnEnviarDatos_Clicked;
        }

        async private void BtnEnviarDatos_Clicked(object sender, EventArgs e)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    string bodyRequest = JsonConvert.SerializeObject(new Person { name = xamarino.Text });
                    var result=await client.PostAsync("https://functionappmed.azurewebsites.net/api/HttpPOST-Xamarinos?code=tWpZfiwucBBrqO9gaGiaZaa8avxecIn9Z3snaEGHDquBD7XB/owxEg=="
                        , new StringContent(bodyRequest, Encoding.UTF8, "application/json"));
                    await DisplayAlert("Completado", "Registro enviado correctamente.", "Aceptar");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "Ha ocurrido un error intentando consumir el servicio de Azure.", "Aceptar");
            }
        }
    }
    public class Person
    {
        public string name { get; set; }
    }
}
