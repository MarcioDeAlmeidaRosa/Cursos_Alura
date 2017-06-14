// criação de classe em ES6
class Negociacao {
    //Quando definimos uma classe com contrutor,
    //só podemos chama-la usando o perador new
    constructor(data, quantidade, valor) {
        //quando usamos o operador new,
        //vai tornar o this correspondente
        //ao objeto criado
        this._data = data;
        this._quantidade = quantidade;
        this._valor = valor;
        // this.volume = (this.quantidade * this.valor);
        //this --> é uma variável implicita
        //que sempre aponta para a instância
        //que esta executando a operação
        //naquele momento
    }

    get volume() {
        return this._quantidade * this._valor;
    }

    get data() {
        return this._data;
    }

    get quantidade() {
        return this._quantidade;
    }

    get valor() {
        return this._valor;
    }
}