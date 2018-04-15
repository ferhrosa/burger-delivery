var menu;
var currentOrder;
var currentOrderUl;
var currentOrderSave;
var currentOrderTotalPrice;
var allOrdersListElement;


$(() => {
    menu = $('#menu');
    currentOrder = $('#current-order');
    currentOrderUl = currentOrder.find('ul');
    currentOrderSave = currentOrder.find('button.main');
    currentOrderTotalPrice = currentOrder.find('header > .price');
    allOrdersListElement = $('#all-orders > .list');

    currentOrderSave.click(saveOrder);

    listAllIngredients(renderRecipes);
    updateLatestOrders();
});


var allIngredients;

function listAllIngredients(callback) {
    api.ingredients.list()
        .done(items => {
            allIngredients = items;
            if (callback) { callback(); }
        });
}

function renderRecipes() {
    api.recipes.list()
        .done(items => {
            menu.children().remove();

            menu.append(buildCustomOrderItem());

            for (let i = 0; i < items.length; i++) {
                let item = items[i];

                let orderItem = {
                    Name: item.Name,
                    Price: item.Price,
                    Ingredients: []
                };

                let recipe = $('<div>');

                let header = $(`<header class="title-price">`);
                header.append($(`<h3>${item.Name}</h3>`))
                header.append($(`<span class='price'>${formatCurrency(item.Price)}</header>`));
                recipe.append(header);

                let ul = $('<ul>');

                for (let j = 0; j < item.Ingredients.length; j++) {
                    let ingredient = item.Ingredients[j];
                    let li = $('<li>');
                    li.html(ingredient.Name);
                    ul.append(li);

                    orderItem.Ingredients.push({
                        Ingredient: {
                            Id: ingredient.Id,
                            Name: ingredient.Name,
                        },
                        Ammount: 1,
                    });
                }

                recipe.append(ul);

                let footer = $('<footer>');
                let button = $('<button type="button">ADICIONAR AO PEDIDO</button>');
                button.click(() => addToCurrentOrder(orderItem));
                footer.append(button);
                recipe.append(footer);

                menu.append(recipe);
            }
        });
}

function buildCustomOrderItem() {
    let custom = $($('#template-custom').html());
    let customUl = custom.find('ul');
    let customPrice = custom.find('header > .price');
    let customButton = custom.find('button');

    for (let i = 0; i < allIngredients.length; i++) {
        let li = $('<li>');
        let label = $(`<label>`);
        label.append($(`<input type="number" value="0" data-id="${allIngredients[i].Id}" data-price="${allIngredients[i].Price}">`))
        label.append($(`<span class='price'>${formatCurrency(allIngredients[i].Price)}</header>`));
        label.append(allIngredients[i].Name);
        li.append(label);
        customUl.append(li);
    }

    customButton.click(() => {
        let ingredients = [];

        customUl.find('input[type=number]').each((index, element) => {
            let input = $(element);
            let ammount = +input.val();
            let id = +input.data('id');
            let name;

            if (ammount) {
                ingredients.push({ Ingredient: { Id: id }, Ammount: ammount });
            }
        });

        let item = { Ingredients: ingredients };

        api.orders.calculateCustom(item)
            .done(calculatedItem => {
                if (confirm(`Confirma a inclusão de um item personalizado pelo preco de ${formatCurrency(calculatedItem.Price)}?`)) {
                    addToCurrentOrder(calculatedItem);

                    // Clear the inputed ammount only after confirming the inclusion of the item.
                    customUl.find('input[type=number]').val(0);
                }
            });
    });

    return custom;
}


var currentOrderList = [];
var currentOrderPrice = 0;

function addToCurrentOrder(item) {
    currentOrderList.push(item);
    currentOrderPrice += item.Price;

    currentOrderTotalPrice.html(formatCurrency(currentOrderPrice));

    currentOrderUl.append(buildOrderItem(item));

    currentOrderSave.removeAttr('disabled');
}


function saveOrder() {
    api.orders.save({ items: currentOrderList })
        .done(() => {
            currentOrderList.splice(0, currentOrderList.length);
            currentOrderPrice = 0;
            currentOrderTotalPrice.html('');
            currentOrderUl.children().remove();;

            updateLatestOrders();
        });
}


function updateLatestOrders() {
    api.orders.listLtest()
        .done(orders => {
            console.dir(orders);

            allOrdersListElement.children().remove();

            for (let i in orders) {
                let order = orders[i];

                let div = $('<div>');

                let header = $('<header class="title-price">');
                header.append($(`<h2>${order.Id} - ${order.FormattedEntryDate}</h2>`));
                header.append($(`<span class="price">${formatCurrency(order.Price)}</span>`));
                div.append(header);

                let ul = $('<ul class="order-item">');
                for (let j in order.Items) {
                    ul.append(buildOrderItem(order.Items[j]));
                }
                div.append(ul);

                allOrdersListElement.append(div);
            }
        });
}


function buildOrderItem(item) {
    let li = $('<li>');
    let header = $('<header class="title-price">');
    header.append($(`<h3>${item.Name}</h3>`));
    header.append($(`<span class="price">${formatCurrency(item.Price)}</h3>`));
    li.append(header);

    if (item.Sale) {
        li.append($(`<span>Promoção: ${item.SaleName}</span>`));
    }

    for (var i in item.Ingredients) {
        li.append($(`<span>${item.Ingredients[i].Ammount}x ${item.Ingredients[i].Ingredient.Name}</span>`));
    }

    return li;
}
