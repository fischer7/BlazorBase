using MudBlazor;
using System.Threading.Tasks;

namespace Client.Infrastructure.Managers.Preferences
{
    public interface IClientPreferenceManager : IManager //: IPreferenceManager
    {
        Task<MudTheme> GetCurrentThemeAsync();

        Task<bool> ToggleDarkModeAsync();
    }
}