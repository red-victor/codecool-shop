import { UI } from "./UIManager.js";
import { LocalStorage } from "./localStorageHandler.js";

const ui = new UI();
changeTotal();
/*var check = false;*/

function changeVal(el) {
    var qt = parseFloat(el.parent().children(".qt").html());
    var price = parseFloat(el.parent().children(".price").html());
    var eq = Math.round(price * qt * 100) / 100;

    el.parent().children(".full-price").html(eq + "$");

    changeTotal();
}

function changeTotal() {

    var price = 0;

    $(".full-price").each(function (index) {
        price += parseFloat($(".full-price").eq(index).html());
    });

    price = Math.round(price * 100) / 100;
    var tax = Math.round(price * 0.05 * 100) / 100
    var shipping = parseFloat($(".shipping span").html());
    var fullPrice = Math.round((price + tax + shipping) * 100) / 100;

    if (price == 0) {
        fullPrice = 0;
    }

    $(".subtotal span").html(price);
    $(".tax span").html(tax);
    $(".total span").html(fullPrice);
}

$(document).ready(function () {

    $(".remove").click(function () {
        var el = $(this);
        var elId = $(el).data("id");
        console.log(elId);
        ui.removeItem(elId);
        ui.setCartValues();
        ui.populateCart();
        el.parent().parent().addClass("removed");
        window.setTimeout(
            function () {
                el.parent().parent().slideUp('fast', function () {
                    el.parent().parent().remove();

                    if ($(".product").length == 0) {
                        $("#cart").html("<h1>No products!</h1>");
                        $("#site-footer").hide();
                    }

                    /*if ($(".product").length == 0) {
                        if (check) {
                            $("#payment-form").show();
                        } else {
                            $("#cart").html("<h1>No products!</h1>");
                            changeTotal();
                        }
                    }*/
                });
            }, 200);
    });

    $(".qt-plus").click(function () {
        $(this).parent().children(".qt").html(parseInt($(this).parent().children(".qt").html()) + 1);

        $(this).parent().children(".full-price").addClass("added");

        var el = $(this);
        var elId = $(el).data("id");
        var cart = LocalStorage.getCart();
        var item = LocalStorage.getProduct(elId);
        LocalStorage.updateQuantity(cart, item);
        ui.setCartValues();
        ui.populateCart();
        window.setTimeout(function () { el.parent().children(".full-price").removeClass("added"); changeVal(el); }, 150);
    });

    $(".qt-minus").click(function () {
        var el = $(this);
        var child = $(this).parent().children(".qt");

        if (parseInt(child.html()) > 1) {
            child.html(parseInt(child.html()) - 1);

            var elId = $(el).data("id");
            var cart = LocalStorage.getCart();
            var item = LocalStorage.getProduct(elId);
            LocalStorage.decreaseQuantity(cart, item);
            ui.setCartValues();
            ui.populateCart();
        }

        $(this).parent().children(".full-price").addClass("minused");

        window.setTimeout(function () { el.parent().children(".full-price").removeClass("minused"); changeVal(el); }, 150);
    });

    window.setTimeout(function () { $(".is-open").removeClass("is-open") }, 1200);

    /*$(".checkout-btn").click(function () {
        check = true;
        $(".remove").click();
    });*/
});