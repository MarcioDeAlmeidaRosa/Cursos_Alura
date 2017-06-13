// criação de classe em ES6
class Negociacao {
    //Quando definimos uma classe com contrutor,
    //só podemos chama-la usando o perador new
    constructor() {
        //quando usamos o operador new,
        //vai tornar o this correspondente
        //ao objeto criado
        this.data = new Date();
        this.quantidade = 1;
        this.valor = 0;
        //this --> é uma variável implicita
        //que sempre aponta para a instância
        //que esta executando a operação
        //naquele momento
    }
}