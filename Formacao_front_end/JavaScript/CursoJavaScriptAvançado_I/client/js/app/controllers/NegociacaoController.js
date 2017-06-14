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
        console.log(this._inputQuantidade.value);
        console.log(this._inputValor.value);

        let negocicacao = new Negociacao(
            this._inputDate.value,
            this._inputQuantidade.value,
            this._inputValor.value
        );

        //adicionar uma negociação em uma lista

        console.log(negocicacao);
    }
}