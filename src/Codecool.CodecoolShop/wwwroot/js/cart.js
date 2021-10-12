export let cartManager = {
    addProductHandler(doc) {
        var id = $(doc).data('id');
        var name = $(doc).data('name');
        console.log(id, name);
    }
}