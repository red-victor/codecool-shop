// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

import { cartManager, updateHeader } from "./cart.js";

$(document).ready(() => {
    updateHeader();

    $(".add-to-cart").each(function () {
        $(this).on("click", function (event) {
            event.preventDefault();
            cartManager.addProductHandler(this);
        })
    });

    $("#cart-link").on("click", function () {
        console.log("clicked");
        var cart = localStorage.getItem("cart");

        $.ajax({
            type: "POST",
            url: "/Product/CheckoutJSON",
            data: { "cart": cart},
            dataType: "json",
            success: function (response) {
                if (response != null) {
                    alert("Name : " + response.Name + ", Designation : " + response.Designation + ", Location :" + response.Location);
                } else {
                    alert("Something went wrong");
                }
            },
            failure: function (response) {
                alert(response.responseText);
            },
            error: function (response) {
                alert(response.responseText);
            }
        });
    })
})
