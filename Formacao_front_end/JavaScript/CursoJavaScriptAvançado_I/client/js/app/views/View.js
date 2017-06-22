class View{
    constructor(elemento){
        this._elemento = elemento;
    }

    //Quando o método não for implementado nos fihos
    //será executado o método da classe pai, e nesse caso
    //lançará uma exception
    //PS: antes o método era definido com _template, por conta da herença
    //essa convenção não pode ser mais usada, por isso foi removido o _ da frente
    template(model){
        throw new Error("O metodo template deve ser implementado...");
    }

    //metodo responsável por fazer o "binding" do template HTML da (tabela ou outro elemento)
    //para cima do objeto DOM passado como parâmetro para o construtor da classe
    update(modelo){
        //Comando responsável ppor pegar o elemento do DOM
        //passado no construtor da classe, e atribuir
        //em seu HTML um "template" referente a (tabela ou outro elemento passado)
        this._elemento.innerHTML = this.template(modelo);
    }
}