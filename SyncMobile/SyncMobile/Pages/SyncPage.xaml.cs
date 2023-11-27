using System.Text;
using System.Text.Json;
using SyncMobile.Models;

namespace SyncMobile.Pages
{
    public partial class SyncPage : ContentPage
    {
        private readonly HttpClient _httpClient;
        private readonly string apiUrl = "https://cd4d-2804-45e4-80f8-5800-cc4f-eb64-489e-41c5.ngrok-free.app/api/sync";
        private ProdutoService _produtoService = new ProdutoService();
        private VisitasService _visitasService = new VisitasService();

        public SyncPage()
        {
            InitializeComponent();
            _httpClient = new HttpClient();
        }

        private async void OnStartSyncClicked(object sender, EventArgs e)
        {
            StartSyncButton.IsEnabled = false;
            StatusLabel.Text = "Sincronizando...";
            SyncProgressBar.IsVisible = true;
            SyncProgressBar.Progress = 0.5;

            try
            {
                List<Visita> visitas = await BuscarVisitas();
                List<Produto> produtos = await BuscarProdutos();

                var syncData = new SyncData();

                syncData.Produtos = produtos;
                syncData.Visitas = visitas;

                var json = JsonSerializer.Serialize(syncData);

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    SyncProgressBar.Progress = 1.0;
                    StatusLabel.Text = "Sincronizado com sucesso!";
                }
                else
                {
                    var errorResponse = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Erro na API: {errorResponse}");
                    StatusLabel.Text = "Falha na sincronização.";
                }
            }
            catch (Exception ex)
            {
                StatusLabel.Text = $"Erro: {ex.Message}";
            }
            finally
            {
                StartSyncButton.IsEnabled = true;
                SyncProgressBar.IsVisible = false;
            }
        }

        private async Task<List<Visita>> BuscarVisitas()
        {
            
            return await _visitasService.GetVisitasAsync();
        }

        private async Task<List<Produto>> BuscarProdutos()
        {
            return await _produtoService.GetProdutosAsync();
        }
    }
}
