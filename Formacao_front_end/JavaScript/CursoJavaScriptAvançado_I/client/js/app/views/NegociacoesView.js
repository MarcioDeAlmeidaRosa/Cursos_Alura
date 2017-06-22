class NegociacoesView{

    constructor(elemento){
        this._elemento = elemento;
    }

    //metodo responsável por fazer o "binding" do template HTML da tabela
    //para cima do objeto DOM passado como parâmetro para o construtor da classe
    update(modelo){
        //Comando responsável ppor pegar o elemento do DOM
        //passado no construtor da classe, e atribuir
        //em seu HTML um "template" referente a tabela 
        this._elemento.innerHTML = this._template(modelo);
    }

    //metodo responsável por retornar uma string com o template
    //da tabela que vai ser injetada dentro do elemento DOM que foi
    //passado como parâmetro no construtor da classe.
    //OBS: ao usar o metodo map, ele devolve um array, se deixa somente desta forma
    //o HTML será quebrado, pois irá conter a , que é o separador do array
    //para contornar esse problema, usamos o recurso join, passando uma string vazia
    //assim não será retornado , ao terminar o metodo map
    //deixando nosso HTML da forma que queremos
    _template(modelo){
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
                        ${
                            //usando template rendering
                            modelo.getNegociacoes.map(neg => {
                            console.log(neg);
                            return `
                                <tr>
                                <td>${DateHelper.dateToText(neg.data)}</td>
                                <td>${neg.quantidade}</td>
                                <td>${neg.valor}</td>
                                <td>${neg.volume}</td>
                                </tr>
                            `;
                        }).join('')}
                    </tbody>

                    <tfoot>
                    </tfoot>
                </table>
        `;
    }
}