using Blazored.Toast.Services;
using eCommerce.Blazor.Demo.Pages.Shared;
using eCommerce.Blazor.Demo.SessionStorage;
using eCommerce.Commons.Objects.Responses.ShoppingCart;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace eCommerce.Blazor.Demo.Pages.Order
{
    public partial class PreOrder : ComponentBase
    {
        #region component parameters and Inject
        [CascadingParameter]
        public MainLayout mainLayout { get; set; }
        [CascadingParameter]
        public Error Error { get; set; }
        [Parameter]
        public string Code { get; set; }
        [Inject]
        public ShoppingSessionStorage _ShoppingSessionStorage { get; set; }
        #endregion

        #region component properties
        private List<ShoppingCartResponse>? items { get; set; }
        public decimal TotalPriceIsCheck { get => items.Sum(x => x.TotalPrice); }
        public string textPrueba;
        public bool boolPrueba;
        protected bool isOpenAddressForm = false;
        protected DialogOptions dialogAddressOptions = new()
        {
            FullWidth = true,
            CloseOnEscapeKey = false,
            DisableBackdropClick = true,
            MaxWidth = MaxWidth.Large
        };
        #endregion

        #region component events
        protected override async Task OnInitializedAsync()
        {
            try
            {
                mainLayout.Title = "Order";
                items = (await _ShoppingSessionStorage.GetShoppingCart()).ToList();

            }
            catch (Exception ex)
            {
                Error.ProcessError(ex);
            }
        }

        protected async Task OnToggleAddressForm()
        {
            isOpenAddressForm = !isOpenAddressForm;
        }


        protected async Task onShopChanged()
        {
            try
            {
                _ShoppingSessionStorage.SaveShoppingCart(items);
            }
            catch (Exception ex)
            {
                Error.ProcessError(ex);
            }
        }
        #endregion
    }
}
