// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

import { cartManager } from "./cart.js";

$(document).ready(() => {

    sessionStorage.setItem("total", 0);
    sessionStorage.setItem("cart", "[]");

    $(".add-to-cart").each(function () {
        $(this).on("click", function (event) {
            event.preventDefault();
            cartManager.addProductHandler(this);
        })
    })

})