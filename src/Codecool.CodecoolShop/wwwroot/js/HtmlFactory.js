﻿export const htmlTemplates = {
    cartItem: 1
};

export function htmlFactory(template) {
    switch (template) {
        case htmlTemplates.cartItem:
            return cartItemBuilder;
        default:
            console.error("Undefined template: " + template);
            return () => {
                return "";
            };
    }
}

function cartItemBuilder(cartItem) {
    return `
        <div class="col-lg-3 col-lg-3" style="display: inline-block; max-width: 350px; height: 350px">
            <div class="card">
                <img src="../../img/${cartItem.name}.jpg" style="height: 50%; width: 50%; align-self: center; padding-top: 10px">

                <div class="card-body">
                    <h5 class="card-title text-center">
                        Product
                        @{ var num = Model.IndexOf(element) + 1;}
                        @num
                    </h5>
                    <h5 class="card-title">@element.Name</h5>
                    <p class="card-text">@element.Description.</p>
                    <p class="card-text">Category: <a asp-controller="Product" asp-action="GetByCategory" asp-route-id="@element.ProductCategory.Id"> @element.ProductCategory.Name</a></p>
                    <p class="card-text">Supplier: <a asp-controller="Product" asp-action="GetBySupplier" asp-route-id="@element.Supplier.Id"> @element.Supplier.Name</a></p>
                    <p class="card-text text-center"><strong>Price: @element.DefaultPrice.ToString("C2")</strong></p>
                    @*<a type="button" class="btn btn-primary" style="float: bottom" asp-controller="Product" asp-action="AddToCart" asp-route-id="@element.Id">Add To Cart</a>*@
                    <a href="" type="button" class="btn btn-primary add-to-cart" style="float: bottom" data-id="@element.Id" data-name="@element.Name" data-price="@element.DefaultPrice">Add To Cart</a>
                </div>
            </div>
        </div>
    `;
}