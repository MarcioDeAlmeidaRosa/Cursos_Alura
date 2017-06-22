let numeros = [1, 2, 3, 4]; // Somatório = 1 + 2 + 3 + 4 = 10


//Um exemplo de função que nos retorne o somatório de um array de números poderia ser assim:
console.log(somatorio(numeros));
function somatorio(array) {
    var resultado = 0;
    for(let i = 0; i < array.length; i++){
        resultado = array[i] + resultado;
    }

    return resultado;
}

//A mesma coisa usando forEach:
console.log(somatorio(numeros));
function somatorio(array) {
    let resultado = 0;
    array.forEach(item => resultado+= item);
    return resultado;
}

//Produtorio usando reduce:
console.log(produtorio(numeros));
function produtorio (array) {
    let resultado = numeros.reduce(function(total, num) { return total * num; }, 1);
    return resultado;
}

//O total se inicia com o valor 1, definido pelo segundo parâmetro da função reduce.
//É feita a primeira iteração, pegando o primeiro valor do array (1) :
//Na segunda iteração, com o segundo valor do array (2):
//Na terceira iteração, com o segundo valor do array (3):
//Na segunda iteração, com o segundo valor do array (4):
//E no final ele devolve para nós o valor 24 , que é o valor esperado do produtório!