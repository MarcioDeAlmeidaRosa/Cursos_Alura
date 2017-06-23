class ListaNegociacoes {

    constructor(callbackAtualizacao) {
        this._negociacoes = [];
        this._callbackAtualizacao = callbackAtualizacao;
    }

    adiciona(negociacao) {
        this._negociacoes.push(negociacao);
        this._callbackAtualizacao(this);
    }

    get getNegociacoes() {
        //usando programação defensiva
        return [].concat(this._negociacoes);
    }

    esvazia(){
        this._negociacoes = [];
        this._callbackAtualizacao(this)
    }
}