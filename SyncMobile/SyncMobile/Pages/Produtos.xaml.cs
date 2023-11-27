using System.Net.Http;
using System.Text.Json;
using System.Text;
using SyncMobile.Context;
using SyncMobile.Models;

namespace SyncMobile.Pages
{
    public partial class Produtos : ContentPage
    {
        private readonly HttpClient _httpClient;

        public Produtos()
        {
            InitializeComponent();
            _httpClient = new HttpClient();
        }

        private async void OnEnviarClicked(object sender, EventArgs e)
        {
            var nomeProduto = ProdutoEntry.Text;
            var valorProduto = double.Parse(ValorEntry.Text);
            var informacaoTecnica = InformacaoTecnicaEditor.Text;
            var imageSource = imagemCapturada.Source;
            var streamImageSource = (StreamImageSource)imageSource;


            byte[] byteArray = null;
            if (streamImageSource != null)
            {
                using (var stream = await streamImageSource.Stream(CancellationToken.None))
                {
                    if (stream != null)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await stream.CopyToAsync(memoryStream);
                            byteArray = memoryStream.ToArray();
                        }
                    }
                }
            }


            var novoProduto = new Produto
            {
                NomeProduto = nomeProduto,
                ValorProduto = valorProduto,
                InformacaoTecnica = informacaoTecnica,
                Foto = byteArray
            };

            try
            {
                using (var dbContext = new SyncContext())
                {
                    dbContext.Produtos.Add(novoProduto);
                    await dbContext.SaveChangesAsync();

                    await DisplayAlert("Alert", "Cadastrado com Sucesso!", "OK");
                    await Navigation.PushAsync(new ProdutosPage());
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Alert", ex.ToString(), "OK");
            }
        }


        private async void AbrirCamera_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (!MediaPicker.IsCaptureSupported)
                {
                    await DisplayAlert("Erro", "Câmera não suportada neste dispositivo.", "OK");
                    return;
                }

                var photo = await MediaPicker.CapturePhotoAsync();
                if (photo == null)
                {
                    return;
                }

                await using var stream = await photo.OpenReadAsync();
                var memoryStream = new MemoryStream();
                await stream.CopyToAsync(memoryStream);

                memoryStream.Seek(0, SeekOrigin.Begin);

                imagemCapturada.Source = ImageSource.FromStream(() => new MemoryStream(memoryStream.ToArray()));
            }
            catch (Exception ex)
            {
                await DisplayAlert("Alert", ex.ToString(), "OK");
            }
        }

    }
}
