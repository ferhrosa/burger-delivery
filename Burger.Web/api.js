var urlApi = 'http://localhost:50000/';

var api = {

    ingredients: {
        list: () => $.get(`${urlApi}ingredients`),
    },

    recipes: {
        list: () => $.get(`${urlApi}recipes`),
    },

    orders: {
        save: items => $.post(`${urlApi}orders`, JSON.stringify(items)),
    },

}
