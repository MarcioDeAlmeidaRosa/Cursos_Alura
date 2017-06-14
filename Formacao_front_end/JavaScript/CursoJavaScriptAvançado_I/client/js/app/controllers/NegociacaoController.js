class NegociacaoController {
    constructor() {
        //Quando atribuímos a função ou "método da classe document"
        //a referência que é atribuída perde a referência this
        //pois somente a função é "copiada",
        //para corrigir isso, temos o auxílio do metodo
        //bind, que define a referência chamadora, seu "this",
        //no caso document, assim quando jogamos a referência
        //para a variável $, ao chamar a função
        //seu this continuará sendo document, 
        //se comportando normalmente,
        //algo similar é feito com jQuery
        let $ = document.querySelector.bind(document);

        //Estratégia adotada de trazer a busca
        //no DOM dos campos para o contrutor da classe
        //para melhorar a performance,
        //pois é custoso criar e ficar a cada adição
        //tendo que buscar os campos no DOM.
        this._inputDate = $("#data");
        this._inputQuantidade = $("#quantidade");
        this._inputValor = $("#valor");
    }

    adiciona(event) {
            event.preventDefault();
            // alert("chamei ação n controller");

            console.log(typeof(this._inputDate.value));

            console.log(this._inputDate.value);
            console.log("//1º - Forma - usando aplit");
            console.log(new Date(this._inputDate.value.split("-")));
            console.log("//2º - Forma - usando replace com regex");
            console.log(new Date(this._inputDate.value.replace(/-/g, ',')));
            console.log("//3º - Forma - usando split recuperando as casas");
            let ano = parseInt(this._inputDate.value.split("-")[0]);
            let mes = parseInt(this._inputDate.value.split("-")[1]) - 1;
            let dia = parseInt(this._inputDate.value.split("-")[2]);
            console.log(new Date(ano, mes, dia));
            console.log("//4º - Forma - usando spread operator");
            console.log(
                new Date(
                    this._inputDate.value
                    .split("-")
                    .map(function(item, indice) {
                        if (indice == 1)
                            return item - 1;
                        return item;
                    })
                ));
            console.log("//5º - Forma - usando spread operator - e calculo de módulo");
            console.log(
                new Date(
                    this._inputDate.value
                    .split("-")
                    .map(function(item, indice) {
                        return item - (item % 2);
                    })
                ));
            console.log("//6º - Forma - usando arrow arrow functions - e calculo de módulo");
            console.log(
                new Date(
                    this._inputDate.value
                    .split("-")
                    .map((item, indice) => {
                        return item - (item % 2);
                    })
                ));
            console.log("//7º - Forma - usando arrow arrow functions simplificada por só ter uma instrução - e calculo de módulo");
            console.log(
                new Date(
                    this._inputDate.value
                    .split("-")
                    .map((item, indice) => item - (item % 2));

                    console.log(this._inputQuantidade.value); console.log(this._inputValor.value);

                    let negocicacao = new Negociacao(
                        new Date(this._inputDate.value.split("-")),
                        this._inputQuantidade.value,
                        this._inputValor.value
                    );

                    //adicionar uma negociação em uma lista

                    console.log(negocicacao);
                }
            }