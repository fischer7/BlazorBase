using MudBlazor;

namespace Client.Infrastructure.Managers;
public class Manager : IManager
{
    public ISnackbar _snackBar;
    public Manager(ISnackbar snackbar) => _snackBar = snackbar;
}
