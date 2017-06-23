class Secretaria extends Funcionario {
    //só precisamos usar o super passando o parâmetro 
    //de entrada da classe pai quando o nosso construtor 
    //da classe filha tem um número diferente de parâmetro
    //PS: o super deve ser a primeira linha a ser executada
    //pois enquanto não criamos a instância da classe pai
    //a classe filha ainda não tem o this.
    constructor(nome, funcionario){
        super(nome);
        this._funcionario = funcionario;
    }

    atenderTelefone() {
        console.log(`${this._nome} atendendo telefone` );
    }

    get funcionario(){
        return this._funcionario;
    }
}