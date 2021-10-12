export let cartManager = {
    addProductHandler(doc) {
        var product = {
            "name": $(doc).data('name'),
            "price" : parseFloat($(doc).data('price')),
            "quantity": 1
        }

        var cart = getCart();
        addToCart(cart, product);
        updateTotal();
        updateHeader();

        /*console.log(cart);
        console.log(localStorage.getItem("total"));*/
    }
}

function addToCart(cart, product) {
    var inCart = false;

    cart.forEach(item => {
        if (item.name == product.name) {
            updateQuantity(cart, product);
            inCart = true;
        }
    });

    if (!inCart)
        addItem(cart, product)
}

function addItem(cart, product) {
    cart.push(product);
    setCart(cart);
}

function updateQuantity(cart, product) {
    cart.forEach(item => {
        if (item.name == product.name) {
            item.quantity++;
        }
    });

    setCart(cart);
}

function updateTotal() {
    var price = 0;
    var cart = getCart();
    cart.forEach(item => {
        price += item.price * item.quantity;
    })

    localStorage.setItem("total", price);
}

function getCart() {
    var cartValue = localStorage.getItem("cart");
    return JSON.parse(cartValue);
}

function setCart(cart) {
    var jsonStr = JSON.stringify(cart);
    localStorage.setItem("cart", jsonStr);
}

function updateHeader() {
    var itemCount = 0;
    var cart = getCart();
    cart.forEach(item => itemCount += item.quantity);

    var header = document.getElementById("cart-header");
    header.innerHTML = "Cart: " + itemCount;
}