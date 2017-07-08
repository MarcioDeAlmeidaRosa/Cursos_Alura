class Mensagem {

    //valor default no parâmetro

    //O Edge não 13 não suporta parâmetros opcionais
    //então teremos que alterar a implementação do contrutor
    // constructor(texto = ''){
    constructor(texto) {
        this._texto = texto || ''; // se texto for undefined, vai passar ''
    }

    get texto() {
        return this._texto;
    }

    set texto(texto) {
        this._texto = texto;
    }
}