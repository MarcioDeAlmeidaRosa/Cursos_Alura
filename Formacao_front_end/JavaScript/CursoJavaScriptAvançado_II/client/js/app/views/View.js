class View {
    constructor(elemento) {
        this._elemento = elemento;
    }

    //Quando o método não for implementado nos fihos
    //será executado o método da classe pai, e nesse caso
    //lançará uma exception
    //PS: antes o método era definido com _template, por conta da herença
    //essa convenção não pode ser mais usada, por isso foi removido o _ da frente
    template(model) {
        throw new Error("O metodo template deve ser implementado...");
    }

    //metodo responsável por fazer o "binding" do template HTML da (tabela ou outro elemento)
    //para cima do objeto DOM passado como parâmetro para o construtor da classe
    update(modelo) {
        console.log(`executando update ${JSON.stringify(modelo)}`);
        //Comando responsável ppor pegar o elemento do DOM
        //passado no construtor da classe, e atribuir
        //em seu HTML um "template" referente a (tabela ou outro elemento passado)
        this._elemento.innerHTML = this.template(modelo);

        //POG para aplicar o controle de mouseouver no cabeçalho
        let $ = document.querySelector.bind(document);
        
        $('#cab_dat').onmouseover = ()=>{
            $('#cab_dat').classList.add('mouse-pointer');//.cursor = pointer; 
        };
        $('#cab_dat').onmouseout = ()=>{
            $('#cab_dat').classList.remove('mouse-pointer');//.cursor = pointer; 
        };
        
        $('#cab_qtd').onmouseover = ()=>{
            $('#cab_qtd').classList.add('mouse-pointer');//.cursor = pointer; 
        };
        $('#cab_qtd').onmouseout = ()=>{
            $('#cab_qtd').classList.remove('mouse-pointer');//.cursor = pointer; 
        };

        $('#cab_vlr').onmouseover = ()=>{
            $('#cab_vlr').classList.add('mouse-pointer');//.cursor = pointer; 
        };
        $('#cab_vlr').onmouseout = ()=>{
            $('#cab_vlr').classList.remove('mouse-pointer');//.cursor = pointer; 
        };

        $('#cab_vol').onmouseover = ()=>{
            $('#cab_vol').classList.add('mouse-pointer');//.cursor = pointer; 
        };
        $('#cab_vol').onmouseout = ()=>{
            $('#cab_vol').classList.remove('mouse-pointer');//.cursor = pointer; 
        };
    }
}