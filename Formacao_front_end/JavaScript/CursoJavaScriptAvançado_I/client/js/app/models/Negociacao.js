// criação de classe em ES6
class Negociacao {
    //Quando definimos uma classe com contrutor,
    //só podemos chama-la usando o perador new
    constructor(data, quantidade, valor) {
        //quando usamos o operador new,
        //vai tornar o this correspondente
        //ao objeto criado
        this.data = data;
        this.quantidade = quantidade;
        this.valor = valor;
        // this.volume = (this.quantidade * this.valor);
        //this --> é uma variável implicita
        //que sempre aponta para a instância
        //que esta executando a operação
        //naquele momento
    }

    obtemValume() {
        return this.quantidade * this.valor;
    }
}