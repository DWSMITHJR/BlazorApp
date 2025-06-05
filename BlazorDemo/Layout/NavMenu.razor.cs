using Microsoft.AspNetCore.Components;

namespace BlazorDemo.Layout
{
    public partial class NavMenu
    {
        private bool collapseNavMenu = true;
        private string currentPage = "";

        private string? NavMenuCssClass => collapseNavMenu ? "collapse" : null;

        private void ToggleNavMenu()
        {
            collapseNavMenu = !collapseNavMenu;
        }

        private void SetCurrentPage(string? page = null)
        {
            currentPage = page ?? string.Empty;
        }

        private void NavigateToHome()
        {
            SetCurrentPage("");
        }

        private void NavigateToCounter()
        {
            SetCurrentPage("counter");
        }

        private void NavigateToWeather()
        {
            SetCurrentPage("weather");
        }
    }
}
