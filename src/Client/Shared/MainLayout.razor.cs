using Client.Infrastructure.Settings;
using MudBlazor;
using System;
using System.Threading.Tasks;

namespace Client.Shared
{
    public partial class MainLayout 
    {
        private MudTheme _currentTheme;
        private bool _rightToLeft = false;
        private async Task RightToLeftToggle(bool value)
        {
            _rightToLeft = value;
            await Task.CompletedTask;
        }

        protected override async Task OnInitializedAsync()
        {
            _currentTheme = BlazorHeroTheme.DefaultTheme;
            _currentTheme = await _clientPreferenceManager.GetCurrentThemeAsync();
            _rightToLeft = await _clientPreferenceManager.IsRTL();
        }

        private async Task DarkMode()
        {
            bool isDarkMode = await _clientPreferenceManager.ToggleDarkModeAsync();
            _currentTheme = isDarkMode
                ? BlazorHeroTheme.DefaultTheme
                : BlazorHeroTheme.DarkTheme;
        }

    }
}
