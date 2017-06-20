function Pessoa(nome, sobrenome) {
    this.nome = nome;
    this.sobrenome = sobrenome;
}

// método de instância
Pessoa.prototype.obterNomeCompleto = function() {
    return this.nome + ' ' + this.sobrenome;
};

// declarando equivalente a método estático

Pessoa.metodoStaticoQualquer = function() {

    console.log('Método estático chamado');

};