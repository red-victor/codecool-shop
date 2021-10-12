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
        var cart = localStorage.getItem("cart");

        $.ajax({
            type: "POST",
            url: "/Product/CheckoutJSON",
            data: { "cart": cart},
            dataType: "json",
            success: function (response) {
                if (response.success) {
                    window.location.replace("/Product/Checkout");
                }
            },
            error: function (response) {
                alert("error!"); 
            }
        });
    })
})
