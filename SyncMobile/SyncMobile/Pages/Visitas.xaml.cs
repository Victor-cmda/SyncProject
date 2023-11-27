using System;
using SyncMobile.Context;
using SyncMobile.Models;

namespace SyncMobile.Pages
{
    public partial class Visitas : ContentPage
    {
        public Visitas()
        {
            InitializeComponent();
        }

        private async void OnEnviarClicked(object sender, EventArgs e)
        {
            var dataVisita = DataVisitaPicker.Date;
            var localizacao = LocalizacaoEntry.Text;

            var novaVisita = new Visita
            {
                Data = dataVisita,
                Localizacao = localizacao
            };

            try
            {
                using (var dbContext = new SyncContext())
                {
                    dbContext.Visitas.Add(novaVisita);
                    await dbContext.SaveChangesAsync();

                    await DisplayAlert("Alert", "Cadastrado com Sucesso!", "OK");
                    await Navigation.PushAsync(new VisitasPage());
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Alert", ex.ToString(), "OK");
            }
        }
    }
}
