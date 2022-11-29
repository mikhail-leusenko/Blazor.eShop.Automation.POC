// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

namespace eShop.Web.CustomerPortal.Controls
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "D:\Repos\Blazor_eshop\eShop.Web.Modules\eShop.Web.CustomerPortal\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Repos\Blazor_eshop\eShop.Web.Modules\eShop.Web.CustomerPortal\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "D:\Repos\Blazor_eshop\eShop.Web.Modules\eShop.Web.CustomerPortal\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "D:\Repos\Blazor_eshop\eShop.Web.Modules\eShop.Web.CustomerPortal\_Imports.razor"
using eShop.CoreBusiness.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "D:\Repos\Blazor_eshop\eShop.Web.Modules\eShop.Web.CustomerPortal\_Imports.razor"
using eShop.UseCases.SearchProductScreen;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "D:\Repos\Blazor_eshop\eShop.Web.Modules\eShop.Web.CustomerPortal\_Imports.razor"
using eShop.UseCases.ViewProductScreen;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "D:\Repos\Blazor_eshop\eShop.Web.Modules\eShop.Web.CustomerPortal\_Imports.razor"
using eShop.UseCases.ViewProductScreen.Interfaces;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "D:\Repos\Blazor_eshop\eShop.Web.Modules\eShop.Web.CustomerPortal\_Imports.razor"
using eShop.UseCases.PluginInterfaces.StateStore;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "D:\Repos\Blazor_eshop\eShop.Web.Modules\eShop.Web.CustomerPortal\_Imports.razor"
using eShop.UseCases.ShoppingCartScreen;

#line default
#line hidden
#nullable disable
#nullable restore
#line 11 "D:\Repos\Blazor_eshop\eShop.Web.Modules\eShop.Web.CustomerPortal\_Imports.razor"
using eShop.UseCases.ShoppingCartScreen.Interfaces;

#line default
#line hidden
#nullable disable
#nullable restore
#line 12 "D:\Repos\Blazor_eshop\eShop.Web.Modules\eShop.Web.CustomerPortal\_Imports.razor"
using eShop.UseCases.OrderConfirmationScreen;

#line default
#line hidden
#nullable disable
#nullable restore
#line 14 "D:\Repos\Blazor_eshop\eShop.Web.Modules\eShop.Web.CustomerPortal\_Imports.razor"
using eShop.Web.Common.Controls;

#line default
#line hidden
#nullable disable
#nullable restore
#line 15 "D:\Repos\Blazor_eshop\eShop.Web.Modules\eShop.Web.CustomerPortal\_Imports.razor"
using eShop.Web.CustomerPortal.Controls;

#line default
#line hidden
#nullable disable
#nullable restore
#line 16 "D:\Repos\Blazor_eshop\eShop.Web.Modules\eShop.Web.CustomerPortal\_Imports.razor"
using eShop.Web.CustomerPortal.ViewModels;

#line default
#line hidden
#nullable disable
    public partial class OrderSummaryComponent : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 46 "D:\Repos\Blazor_eshop\eShop.Web.Modules\eShop.Web.CustomerPortal\Controls\OrderSummaryComponent.razor"
       
    int itemsCount = 0;
    double itemsTotalPrice = 0;

    [Parameter]
    public Order Order { get; set; }
    [Parameter]
    public bool HidePlaceOrderButton { get; set; } = false;

    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        if (Order != null)
        {
            itemsCount = Order.LineItems.Count;
            itemsTotalPrice = 0;
            Order.LineItems.ForEach(x => itemsTotalPrice += x.Price * x.Quantity);
        }
    }

    private void PlaceOrder()
    {
        navigationManager.NavigateTo("/placeorder");
    }

#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private NavigationManager navigationManager { get; set; }
    }
}
#pragma warning restore 1591