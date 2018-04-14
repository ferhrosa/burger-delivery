var urlApi = 'http://localhost:50000/';

var api = {

    ingredients: {
        list: () => $.get(`${urlApi}ingredients`),
    },

    recipes: {
        list: () => $.get(`${urlApi}recipes`),
    },

    orders: {
        save: item => $.post(`${urlApi}orders`, JSON.stringify(item)),
    },

}
