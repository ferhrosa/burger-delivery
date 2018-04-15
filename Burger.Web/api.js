var urlApi = 'http://localhost:50000/';

var api = {

    ingredients: {
        list: () => $.get(`${urlApi}ingredients`),
    },

    recipes: {
        list: () => $.get(`${urlApi}recipes`),
    },

    orders: {
        calculateCustom: item => $.post(`${urlApi}orders/calculate-custom`, item),
        save: items => $.post(`${urlApi}orders`, items),
    },

}
