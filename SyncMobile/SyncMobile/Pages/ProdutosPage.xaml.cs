namespace SyncMobile.Pages;

public partial class ProdutosPage : ContentPage
{
	private ProdutoService _produtoService = new ProdutoService();

    public ProdutosPage()
    {
        InitializeComponent();
        CarregarProdutos();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        CarregarProdutos();
    }

    private async void CarregarProdutos()
    {
        var produtos = await _produtoService.GetProdutosAsync();
        foreach (var produto in produtos)
        {
            produto.ImageSource = ByteToImageSource(produto.Foto);
        }
        ListViewProdutos.ItemsSource = produtos;
    }

    public static ImageSource ByteToImageSource(byte[] imageData)
    {
        return ImageSource.FromStream(() => new MemoryStream(imageData));
    }

    private async void OnAdicionarNovoProdutoClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Produtos());
    }

}
