function formatCurrency(number) {
    return `R$ ${number.toFixed(2).replace('.', ',')}`;
}