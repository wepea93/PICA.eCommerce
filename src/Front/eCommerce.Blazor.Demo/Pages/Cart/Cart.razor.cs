using Blazored.LocalStorage;
using Blazored.Toast.Services;
using eCommerce.Blazor.Demo.LocalStorage;
using eCommerce.Blazor.Demo.Pages.Shared;
using eCommerce.Blazor.Demo.SessionStorage;
using eCommerce.Commons.Objects.Responses.ShoppingCart;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace eCommerce.Blazor.Demo.Pages.Cart
{
    public partial class Cart : ComponentBase
    {
        #region component parameters and Inject
        [CascadingParameter]
        public MainLayout mainLayout { get; set; }
        [CascadingParameter]
        public Error Error { get; set; }
        [Inject]
        public ShoppingSessionStorage _ShoppingSessionStorage { get; set; }
        [Inject]
        public NavigationManager navigate { get; set; }

        #endregion

        #region component properties
        protected ShoppingCartResponse? CurrentProduct;
        protected bool? IsCheckAll
        {
            get
            {
                bool? result = false;
                if (mainLayout.cart != null && mainLayout.cart.Count(x => x.isCheck) == mainLayout.cart.Count)
                {
                    result = true;
                }
                else if (mainLayout.cart != null && mainLayout.cart.Any(x => x.isCheck))
                {
                    result = null;
                }
                return result;
            }
            set
            {
                if (mainLayout.cart != null && value.HasValue)
                {
                    foreach (var p in mainLayout.cart)
                    {
                        p.isCheck = value.Value;
                    }
                }
            }
        }

        public decimal TotalPriceIsCheck { get => mainLayout.cart.Any(x => x.isCheck) ? mainLayout.cart.Where(x => x.isCheck).Sum(x => x.TotalPrice) : 0; }
        protected bool isOpenRemoveCardForm;
        protected DialogOptions dialogOptions = new()
        {
            FullWidth = true,
            CloseOnEscapeKey = false,
            DisableBackdropClick = true,
            MaxWidth = MaxWidth.Small
        };

        #endregion

        #region component events
        protected override async Task OnInitializedAsync()
        {
            try
            {
               mainLayout.Title = "Shopping cart";
               await mainLayout.RefreshShoppingCart();
            }
            catch (Exception ex)
            {
                Error.ProcessError(ex);
            }
        }

        protected async Task onRemoveCard(ShoppingCartResponse e)
        {
            try
            {
                CurrentProduct = mainLayout.cart.FirstOrDefault(x => x.Id == e.Id);
                isOpenRemoveCardForm = true;
            }
            catch (Exception ex)
            {
                Error.ProcessError(ex);
            }
        }

        protected async Task ok()
        {
            try
            {
                await mainLayout.removeCard(CurrentProduct);
                isOpenRemoveCardForm = false;
            }
            catch (Exception ex)
            {
                Error.ProcessError(ex);
            }
        }
        protected async Task Cancel() => isOpenRemoveCardForm = false;

        protected string strCartItemsCount()
        {
            return $"{mainLayout.cart.Count} items";
        }
        private async void OnContinuePreOrder()
        {
            if (mainLayout.cart.Any(x => x.isCheck))
            {
                _ShoppingSessionStorage.SaveShoppingCart(mainLayout.cart.Where(x => x.isCheck).ToList());
                navigate.NavigateTo("/PreOrder");
            }
        }

        protected async Task onShopChanged()
        {
            try
            {
                mainLayout.SaveShoppingCartLocalStorage();
            }
            catch (Exception ex)
            {
                Error.ProcessError(ex);
            }
        }
        #endregion
    }
}
