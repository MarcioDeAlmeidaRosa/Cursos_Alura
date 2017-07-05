class ListaNegociacoes {
    constructor() {
        this._negociacoes = [];
    }

    adiciona(negociacao) {
        this._negociacoes.push(negociacao);
    }

    get Negociacoes() {
        //usando programação defensiva
        return [].concat(this._negociacoes);
    }

    get VolumeTotal() {
        return this._negociacoes.reduce((total, n) => total + n.volume, 0.0)
    }
}