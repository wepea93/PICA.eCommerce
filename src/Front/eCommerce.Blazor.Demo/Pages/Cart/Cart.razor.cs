using Blazored.LocalStorage;
using Blazored.Toast.Services;
using eCommerce.Blazor.Demo.Pages.Shared;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace eCommerce.Blazor.Demo.Pages.Cart
{
    public partial class Cart : ComponentBase
    {
        [CascadingParameter]
        public MainLayout mainLayout { get; set; }
        [Parameter]
        public int? Category { get; set; }
        [Inject]
        public IDialogService _Dialog { get; set; }
        [Inject]
        public IToastService _toastService { get; set; }
        [Inject]
        public ILocalStorageService _localStorage { get; set; }

        public string MyString2 { get; set; }
        protected override void OnInitialized()
        {
            mainLayout.Title = "Shopping cart";

            if (Category.HasValue)
                _toastService.ShowSuccess($"parameter:{Category}");
        }
    }
}
