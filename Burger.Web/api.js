var urlApi = 'http://localhost:50000/';

var api = {

    ingredients: {
        list: () => $.get(`${urlApi}ingredients`),
    },

    recipes: {
        list: () => $.get(`${urlApi}recipes`),
    },

    orders: {
        listLtest: () => $.get(`${urlApi}orders/latest`),
        calculateCustom: item => $.post(`${urlApi}orders/calculate-custom`, item),
        save: order => $.post(`${urlApi}orders`, order),
    },

}
