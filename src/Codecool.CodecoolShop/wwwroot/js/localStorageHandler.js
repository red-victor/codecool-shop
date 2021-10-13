export class LocalStorage {
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