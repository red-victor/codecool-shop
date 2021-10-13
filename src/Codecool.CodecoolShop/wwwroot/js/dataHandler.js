export let dataHandler = {
    checkoutCart: async function (cart) {
        return await apiPost("/api/checkout", cart);
    }
}

//async function apiPost(url, payload) {
//    try {
//        let response = await fetch(url, {
//            method: "POST",
//            headers: { "Content-Type": "json" },
//            body: { "payload": payload },
//        });
//        return response.json();
//    } catch (error) {
//        console.error(error)
//    }
//}

async function apiPost(url, payload) {
    $.ajax({
        type: "POST",
        url: url,
        data: { "payload": payload },
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
}