import { dataHandler } from "./dataHandler.js";
dataHandler.saveOrder(localStorage.getItem("cart"))
    .then(() => {
        localStorage.setItem("cart", []);
    });