namespace SyncMobile.Pages;

public partial class VisitasPage : ContentPage
{
    private VisitasService _visitasService = new VisitasService();

    public VisitasPage()
    {
        InitializeComponent();
        CarregarVisitas();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        CarregarVisitas();
    }

    private async void CarregarVisitas()
    {
        var visistas = await _visitasService.GetVisitasAsync();
        ListViewVisitas.ItemsSource = visistas;
    }

    private async void OnAdicionarNovoProdutoClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Visitas());
    }

}
