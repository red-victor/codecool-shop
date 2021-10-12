// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

import { cartManager } from "./cart.js";

$(document).ready(() => {
    $(".add-to-cart").each(function () {
        $(this).on("click", function (event) {
            event.preventDefault();
            cartManager.addProductHandler(this);
        })
    })
    /*$("#cart-link").on("click", function () {
        $.ajax({
            url: '/Product/Checkout',
            type: 'POST',
            done: submissionSucceeded,
            fail: submissionFailed,
            data: localStorage.getItem("cart")
        });
    })*/
    $("#cart-link").on("click", function () {
        console.log("clicked");
        var cart = JSON.parse(localStorage.getItem("cart"));

        console.log(cart);


        $.ajax({
            type: "POST",
            url: "/Product/Checkout",
            data: JSON.stringify(cart),
            contentType: "application/json; charset=utf-8",
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

async function apiPost(url, payload) {
    try {
        let response = await fetch(url, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({
                payload: payload,
            }),
        });
        console.log(JSON.stringify(payload))
        return response.json();
    } catch (error) {
        console.error(error)
    }
}

function submissionSucceeded() {
    console.log("data sent")
}

function submissionFailed() {
    console.log("data NOT sent")
}