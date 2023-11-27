using System.Globalization;
using SyncMobile.Context;

namespace SyncMobile;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

        CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("pt-BR");
        CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("pt-BR");

        using (var dbContext = new SyncContext())
        {
            dbContext.EnsureDatabaseCreated();
        }

        MainPage = new AppShell();
	}
}

