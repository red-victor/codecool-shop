export let dataHandler = {
    saveCart: async function (cart) {
        return await apiPost("/api/saveCart", cart, "/Product/Cart");
    },
    checkoutCart: async function (cart) {
        return await apiPost("/api/saveCart", cart, "/Product/Checkout");
    },
    saveOrder: async function (cart) {
        return await apiPost("/api/saveOrder", cart, "/Product/OrderDetails");
    }
}

async function apiPost(url, payload, redirectURL=null) {
    $.ajax({
        type: "POST",
        url: url,
        data: { "payload": payload },
        dataType: "json",
        success: function (response) {
            if (response.success) {
                if (redirectURL) {
                    window.location.replace(redirectURL);
                }
            }
        },
        error: function (response) {
            console.error("Error on post req");
        }
    });
}