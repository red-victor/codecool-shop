export let dataHandler = {
    checkoutCart: async function (cart) {
        return await apiPost("/api/Cart", cart, "/Product/Cart");
    },
    saveOrder: async function (cart) {
        return await apiPost("/api/saveOrder", cart, "/Product/OrderDetails");
    }
}

async function apiPost(url, payload, redirectURL) {
    $.ajax({
        type: "POST",
        url: url,
        data: { "payload": payload },
        dataType: "json",
        success: function (response) {
            if (response.success) {
                window.location.replace(redirectURL);
            }
        },
        error: function (response) {
            alert("error!");
        }
    });
}