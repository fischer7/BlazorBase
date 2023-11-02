using Client.Extensions;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Threading.Tasks;

namespace Client.Shared
{
    public partial class MainBody
    {

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public EventCallback OnDarkModeToggle { get; set; }

        [Parameter]
        public EventCallback<bool> OnRightToLeftToggle { get; set; }


        private bool _drawerOpen = true;

        private string CurrentUserId { get; set; }
        private string FirstName { get; set; }
        private char FirstLetterOfName { get; set; }
        private bool _rightToLeft = false;


        public async Task ToggleDarkMode()
        {
            await OnDarkModeToggle.InvokeAsync();
        }

        protected override async Task OnInitializedAsync()
        {
            _rightToLeft = await _clientPreferenceManager.IsRTL();
            _snackBar.Add(string.Format("Welcome home {0}", FirstName), Severity.Success);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await LoadDataAsync();
            }
        }

        private async Task LoadDataAsync()
        {
            var state = await _stateProvider.GetAuthenticationStateAsync();
            var user = state.User;
            if (user == null) return;
            if (user.Identity?.IsAuthenticated == true)
            {
                if (string.IsNullOrEmpty(CurrentUserId))
                {
                    CurrentUserId = user.GetUserId();
                    FirstName = user.GetFirstName();
                    if (FirstName.Length > 0)
                    {
                        FirstLetterOfName = FirstName[0];
                    }

                }
            }
        }

        private void DrawerToggle()
        {
            _drawerOpen = !_drawerOpen;
        }

        private void Logout()
        {
            var parameters = new DialogParameters
            {
                {nameof(Dialogs.Logout.ContentText), $"Logout Confirmation"},
                {nameof(Dialogs.Logout.ButtonText), $"Logout"},
                {nameof(Dialogs.Logout.Color), Color.Error},
                {nameof(Dialogs.Logout.CurrentUserId), CurrentUserId}
            };

            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true };

            _dialogService.Show<Dialogs.Logout>("Logout", parameters, options);
        }

    }
}