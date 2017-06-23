class Arquivo {
    constructor(nome, tamanho, tipo) {
        if (!nome)
            throw new Error('Nome não informado');

        if (!tamanho)
            throw new Error('Tamanho não informado');

        if (!tipo)
            throw new Error('Tipo não informado');

        this._nomeArquivo = nome;
        this._tamanhoArquivo = tamanho;
        this._tipoArquivo = tipo;
    }

    get nome() {
        return ''.concat(this._nomeArquivo);
    }

    get tamanho() {
        return ''.concat(this._tamanhoArquivo);
    }

    get tipo() {
        return ''.concat(this._tipoArquivo);
    }
}