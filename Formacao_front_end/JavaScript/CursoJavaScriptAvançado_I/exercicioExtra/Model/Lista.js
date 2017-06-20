class Lista {

    constructor() {
        this._listaDeNomes1 = ["Marcio", "de", "Almeida", "Rosa"];
        this._listaDeNomes2 = ["Elaine", "Cristina", "de", "Carvalho", "Pereira", "Rosa"];
    }

    get impressaoLista() {
        let union = this._listaDeNomes1.
        concat(this._listaDeNomes2);

        union.forEach(item => console.log(item));
    }
}