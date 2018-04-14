var menu;

$(() => {
    menu = $('#menu');

    listAllIngredients(renderRecipes);

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

                let recipe = $('<div>');

                let header = $(`<header>`);
                header.append($(`<h3>${item.Name}</h3>`))
                header.append($(`<span class='price'>${formatCurrency(item.Price)}</header>`));
                recipe.append(header);

                let ul = $('<ul>');

                for (let j = 0; j < item.Ingredients.length; j++) {
                    let ingredient = item.Ingredients[j];
                    let li = $('<li>');
                    li.html(ingredient.Name);
                    ul.append(li);
                }

                recipe.append(ul);

                let footer = $('<footer>');
                let button = $('<button type="button">ADICIONAR AO PEDIDO</button>');
                button.click(() => addToOrder(item));
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
        label.append($(`<input type="checkbox" value="${allIngredients[i].Id}" data-price="${allIngredients[i].Price}">`))
        label.append($(`<span class='price'>${formatCurrency(allIngredients[i].Price)}</header>`));
        label.append(allIngredients[i].Name);
        li.append(label);
        customUl.append(li);
    }

    let customTotalPrice = 0;

    function updateCustomPrice() {
        customPrice.html(formatCurrency(customTotalPrice));

        if (customTotalPrice > 0) { customButton.removeAttr('disabled'); }
        else { customButton.attr('disabled', 'disabled'); }
    }

    updateCustomPrice();

    customButton.click(() => {
        let ingredients = [];

        customUl.find('input').each((index, element) => {
            let e = $(element);
            if (e.is(':checked')) {
                ingredients.push({ Id: +e.val() });
            }
        });

        customUl.find('input').prop('checked', false);

        addToOrder({
            Name: 'Personalizado',
            Ingredients: ingredients,
        });

        customTotalPrice = 0;
        updateCustomPrice();
    });

    custom.find('input[type="checkbox"]').change(event => {
        let price = +$(event.target).data('price');

        if (!event.target.checked) { price *= -1; }

        customTotalPrice += price;

        updateCustomPrice();
    });

    return custom;
}

function addToOrder(item) {
    console.dir(item);
}
