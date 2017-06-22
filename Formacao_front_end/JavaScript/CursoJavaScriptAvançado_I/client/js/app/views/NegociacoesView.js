class NegociacoesView{

    constructor(elemento){
        this._elemento = elemento;
    }

    update(){
        //Comando responsável ppor pegar o elemento do DOM
        //passado no construtor da classe, e atribuir
        //em seu HTML um "template" referente a tabela 
        this._elemento.innerHTML = this._template();
    }

    _template(){
        //com templateString conseguimos quebrar linhas sem fazer concatenação da coluna
        return `
                <table class="table table-hover table-bordered">
                    <thead>
                        <tr>
                            <th>DATA</th>
                            <th>QUANTIDADE</th>
                            <th>VALOR</th>
                            <th>VOLUME</th>
                        </tr>
                    </thead>

                    <tbody>
                    </tbody>

                    <tfoot>
                    </tfoot>
                </table>
        `;
    }
}