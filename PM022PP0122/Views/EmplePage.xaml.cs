using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Media;
using PM022PP0122.Models;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PM022PP0122.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EmplePage : ContentPage
    {
        Plugin.Media.Abstractions.MediaFile FileFoto = null;
        public EmplePage()
        {
            InitializeComponent();
        }

        private Byte[] ConvertImageToByteArray()
        {
            if (FileFoto != null)
            {
                using (MemoryStream memory = new MemoryStream())
                {
                    Stream stream = FileFoto.GetStream();
                    stream.CopyTo(memory);
                    return memory.ToArray();
                }
            }
            return null;
        }
        private async void btnagregar_Clicked(object sender, EventArgs e)
        {
            if (FileFoto == null)
            {
                await DisplayAlert("Error", "No ha tomado una fotografia", "OK");
                return;
            }

            var emple = new Empleado
            {
                id = 0,
                nombre = txtnombre.Text,
                edad = txtedad.Text,
                //sexo = sexo.SelectedItem.ToString(),
                fechaingreso = fecha.Date,
                foto = ConvertImageToByteArray()
            };

            var result = await App.DBase.EmpleSave(emple);

            if (result > 0)
            {
                await DisplayAlert("Empleado Adicionado", "Aviso", "OK");
            }
            else
            {
                await DisplayAlert("ha ocurrido un error", "Aviso", "OK");
            }
        }
        private async void btneliminar_Clicked(object sender, EventArgs e)
        {
            var emple = new Empleado
            {
                id = Convert.ToInt32(txtid.Text),
                nombre = txtnombre.Text,
                edad = txtedad.Text,
                //sexo = sexo.SelectedItem.ToString(),
                fechaingreso = fecha.Date
            };

            var result = await App.DBase.EmpleDelete(emple);
        }
        private async void btnactualizar_Clicked(object sender, EventArgs e)
        {
            var emple = new Empleado
            {
                id = Convert.ToInt32(txtid.Text),
                nombre = txtnombre.Text,
                edad = txtedad.Text,
                //sexo = sexo.SelectedItem.ToString(),
                fechaingreso = fecha.Date
            };

            var result = await App.DBase.EmpleDelete(emple);
        }
        private async void btnfoto_Clicked(object sender, EventArgs e)
        {
            FileFoto = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "MisFotos",
                Name = "test.jpg",
                SaveToAlbum = true
            });

            await DisplayAlert("path directorio", FileFoto.Path, "OK");

            if (FileFoto != null)
            {
                Foto.Source = ImageSource.FromStream(() =>
                {
                    return FileFoto.GetStream();
                });
            }
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            var localizacion = await Geolocation.GetLocationAsync();
        }
    }
}