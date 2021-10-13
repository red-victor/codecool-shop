const cartBtn = document.querySelector(".cart-btn");
const closeCartBtn = document.querySelector(".close-cart");
const clearCartBtn = document.querySelector(".clear-cart")
const cartDOM = document.querySelector(".cart");
const cartOverlay = document.querySelector(".cart-overlay");
const cartItems = document.querySelector(".cart-items");
const cartTotal = document.querySelector(".cart-total");
const cartContent = document.querySelector(".cart-content");

class LocalStorage {
    static getCart() {
        return localStorage.getItem("cart") ? JSON.parse(localStorage.getItem("cart")) : [];
    }

    static saveCart(cart) {
        localStorage.setItem("cart", JSON.stringify(cart));
    }

    static getProduct(id) {
        let cart = JSON.parse(localStorage.getItem("cart"));
        return cart.find(product => product.id == id);
    }

    static productIsInCart(product) {
        let isInCart = false;
        let cart = this.getCart();
        cart.forEach(item => {
            if (item.id == product.id)
                isInCart = true;
        })
        return isInCart;
    }

    static addToCart(product) {
        var cart = this.getCart();
        var inCart = false;

        cart.forEach(item => {
            if (item.name == product.name) {
                this.updateQuantity(cart, product);
                inCart = true;
            }
        });

        if (!inCart)
            this.addItem(cart, product)
    }

    static addItem(cart, product) {
        cart.push(product);
        this.saveCart(cart);
    }

    static updateQuantity(cart, product) {
        cart.forEach(item => {
            if (item.name == product.name) {
                item.quantity++;
            }
        });
        this.saveCart(cart);
    }

    static decreaseQuantity(cart, product) {
        cart.forEach(item => {
            if (item.name == product.name) {
                item.quantity--;
            }
        });
        this.saveCart(cart);
    }
}


class UI {
    setUpAddToCartButtons() {
        document.addEventListener('keydown', this.onEscPress.bind(this), false);
        const buttons = [...document.querySelectorAll(".add-to-cart")];
        buttons.forEach(button => {
            button.addEventListener("click", () => this.addProductHandler(event.target))
        });
    }

    onEscPress(event) {
        console.log(cartOverlay);
        if (event.key == "Escape" && cartOverlay.classList.contains("transparentBcg"))
            this.hideCart();
    }

    setupApp() {
        this.setUpAddToCartButtons();
        this.cartLogic();
        this.setCartValues();
        this.populateCart();
        cartBtn.addEventListener("click", this.showCart);
        closeCartBtn.addEventListener("click", this.hideCart);
    }

    cartLogic() {
        clearCartBtn.addEventListener("click", () => this.clearCart());

        cartOverlay.addEventListener("click", (event) => {
            if (event.target.classList.contains("transparentBcg"))
                this.hideCart();
        })

        cartContent.addEventListener("click", event => {
            if (event.target.classList.contains("remove-item")) {
                let itemToRemove = event.target;
                let id = itemToRemove.dataset.id;
                this.removeItem(id);
            } else if (event.target.classList.contains("fa-chevron-up")) {
                var cart = LocalStorage.getCart();
                let id = event.target.dataset.id;
                let item = LocalStorage.getProduct(id);
                LocalStorage.updateQuantity(cart, item)
            } else if (event.target.classList.contains("fa-chevron-down")) {
                var cart = LocalStorage.getCart();
                let id = event.target.dataset.id;
                let item = LocalStorage.getProduct(id);
                if (item.quantity == 1) 
                    this.removeItem(id);
                else {
                    LocalStorage.decreaseQuantity(cart, item);
                }                
            }

            this.setCartValues();
            this.populateCart();
        })
    }

    clearCart() {
        console.log("pls fix clearing the cart");
        localStorage.removeItem("cart");
        this.hideCart();
        this.populateCart();
        this.setCartValues();
    }

    removeItem(id) {
        var cart = LocalStorage.getCart();
        cart = cart.filter(item => item.id != id)
        LocalStorage.saveCart(cart);
    }

    addProductHandler(doc) {
        var product = {
            "id": $(doc).data("id"),
            "name": $(doc).data('name'),
            "price": parseFloat($(doc).data('price')),
            "quantity": 1
        }

        LocalStorage.addToCart(product);
        this.populateCart();
        this.setCartValues();
        this.showCart();
    }

    setCartValues() {
        var cart = LocalStorage.getCart();
        let priceTotal = 0;
        let itemsTotal = 0;

        cart.map(item => {
            priceTotal += item.price * item.quantity;
            itemsTotal += item.quantity;
        });

        cartTotal.innerText = parseFloat(priceTotal.toFixed(2));
        cartItems.innerText = itemsTotal;
    }

    displayCartItem(item) {
        const div = document.createElement("div");
        div.classList.add("cart-item");
        div.innerHTML = `
            <img src="../../img/${item.name}.jpg" alt="product" />
            <div>
                <h4>${item.name}</h4>
                <h5>$${item.price}</h5>
                <span class="remove-item" data-id=${item.id}>remove</span>
            </div>
            <div>
                <i class="fas fa-chevron-up" data-id=${item.id}></i>
                <p class="item-amount">${item.quantity}</p>
                <i class="fas fa-chevron-down" data-id=${item.id}></i>
            </div>
        `;
        cartContent.appendChild(div);
    }

    showCart() {
        cartOverlay.classList.add("transparentBcg");
        cartDOM.classList.add("showCart");
    }

    hideCart() {
        cartOverlay.classList.remove("transparentBcg");
        cartDOM.classList.remove("showCart");
    }

    populateCart() {
        cartContent.innerHTML = "";
        var cart = LocalStorage.getCart();
        cart.forEach(item => this.displayCartItem(item))
    }
}



document.addEventListener("DOMContentLoaded", () => {
    const ui = new UI();
    ui.setupApp();
});


/*export function updateHeader() {
    var itemCount = 0;
    var cart = getCart();
    cart.forEach(item => itemCount += item.quantity);

    var header = document.getElementById("cart-header");
    header.innerHTML = "Cart: " + itemCount;
}*/