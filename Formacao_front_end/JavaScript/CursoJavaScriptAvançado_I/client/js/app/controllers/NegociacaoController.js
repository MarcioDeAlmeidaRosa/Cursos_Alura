class NegociacaoController {
    //     constructor() {

    //     }

    adiciona(event) {
        event.preventDefault();
        // alert("chamei ação n controller");

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

        let inputDate = $("#data");
        let inputQuantidade = $("#quantidade");
        let inputValor = $("#valor");

        console.log(inputDate.value);
        console.log(inputQuantidade.value);
        console.log(inputValor.value);



    }
}