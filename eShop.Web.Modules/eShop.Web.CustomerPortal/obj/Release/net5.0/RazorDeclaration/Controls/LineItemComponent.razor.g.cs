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
    public partial class LineItemComponent : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 26 "D:\Repos\Blazor_eshop\eShop.Web.Modules\eShop.Web.CustomerPortal\Controls\LineItemComponent.razor"
       
    [Parameter]
    public OrderLineItem LineItem { get; set; }
    [Parameter]
    public EventCallback<Order> OnDeleteProduct { get; set; }
    [Parameter]
    public EventCallback<Order> OnUpdateQuantity { get; set; }
    async void HandleQuantityChange(ChangeEventArgs e)
    {
        if (e.Value == null || string.IsNullOrWhiteSpace(e.Value.ToString())) return;
        int qty = 0;
        if (int.TryParse(e.Value.ToString(), out qty))
        {
            if (qty < 0)
            {
                return;
            }

            var order = await UpdateQuantityUseCase.Execute(LineItem.ProductId, qty);
            await OnUpdateQuantity.InvokeAsync(order);
        }
    }

    async void DeleteProduct(int productId)
    {
        var order = await DeleteProductUseCase.Execute(productId);
        await OnDeleteProduct.InvokeAsync(order);
    }


#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IUpdateQuantityUseCase UpdateQuantityUseCase { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IDeleteProductUseCase DeleteProductUseCase { get; set; }
    }
}
#pragma warning restore 1591