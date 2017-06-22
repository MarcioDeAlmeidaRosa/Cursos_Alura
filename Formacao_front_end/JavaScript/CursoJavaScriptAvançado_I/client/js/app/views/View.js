class View{
    constructor(elemento){
        this._elemento = elemento;
    }

    //metodo responsável por fazer o "binding" do template HTML da (tabela ou outro elemento)
    //para cima do objeto DOM passado como parâmetro para o construtor da classe
    update(modelo){
        //Comando responsável ppor pegar o elemento do DOM
        //passado no construtor da classe, e atribuir
        //em seu HTML um "template" referente a (tabela ou outro elemento passado)
        this._elemento.innerHTML = this._template(modelo);
    }
}