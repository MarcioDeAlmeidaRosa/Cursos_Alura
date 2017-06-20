class ListaNegociacoes {
    constructor() {
        this._negociacoes = [];
    }

    adiciona(negociacao) {
        this._negociacoes.push(negociacao);
    }

    get getNegociacoes() {
        //usando programação defensiva
        return [].concat(this._negociacoes);
    }
}