// criação de classe em ES6
class Negociacao {
    //Quando definimos uma classe com contrutor,
    //só podemos chama-la usando o perador new
    constructor(data, quantidade, valor) {
        //quando usamos o operador new,
        //vai tornar o this correspondente
        //ao objeto criado
        this._data = new Date(data.getTime());
        this._quantidade = quantidade;
        this._valor = valor;
        // this.volume = (this.quantidade * this.valor);
        //this --> é uma variável implicita
        //que sempre aponta para a instância
        //que esta executando a operação
        //naquele momento
        //--------------------------------------------
        //Recuero para congelar a instância do objeto
        //porém não funciona quando a propriedade do
        //objeto é um objeto
        Object.freeze(this);
    }

    get volume() {
        return this._quantidade * this._valor;
    }

    get data() {
        return new Date(this._data.getTime());
    }

    get quantidade() {
        return this._quantidade;
    }

    get valor() {
        return this._valor;
    }
}