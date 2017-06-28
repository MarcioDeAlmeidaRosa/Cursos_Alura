class ListaNegociacoes {

    // constructor(callbackAtualizacao) {`
    constructor() {
        this._negociacoes = [];
        // this._callbackAtualizacao = callbackAtualizacao;
    }

    adiciona(negociacao) {
        this._negociacoes.push(negociacao);
        //Usando a API Reflection para tornar a classe chamadora como o this da execução
        //Reflect.apply(this._callbackAtualizacao, this._contexto, [this]);
        // this._callbackAtualizacao(this);
    }

    get getNegociacoes() {
        //usando programação defensiva
        return [].concat(this._negociacoes);
    }

    esvazia() {
        this._negociacoes = [];
        //Usando a API Reflection para tornar a classe chamadora como o this da execução
        ///Reflect.apply(this._callbackAtualizacao, this._contexto, [this]);
        // this._callbackAtualizacao(this)
    }
}