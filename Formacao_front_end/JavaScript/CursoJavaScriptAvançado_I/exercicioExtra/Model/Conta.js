class Conta{
    constructor(saldo = 0){
        this._saldo = saldo;
    }

    get saldo(){
        return this._saldo;
    }

    atualiza(taxa){
        throw new Error('Metodo atualiza deve ser implementado na classe filha');
    }
}