class NegociacoesView extends View {
    //metodo responsável por retornar uma string com o template
    //da tabela que vai ser injetada dentro do elemento DOM que foi
    //passado como parâmetro no construtor da classe.
    //OBS: ao usar o metodo map, ele devolve um array, se deixa somente desta forma
    //o HTML será quebrado, pois irá conter a , que é o separador do array
    //para contornar esse problema, usamos o recurso join, passando uma string vazia
    //assim não será retornado , ao terminar o metodo map
    //deixando nosso HTML da forma que queremos
    template(modelo) {
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
                            modelo.Negociacoes.map(neg =>`
                                <tr>
                                    <td>${DateHelper.dateToText(neg.data)}</td>
                                    <td>${neg.quantidade}</td>
                                    <td>${neg.valor}</td>
                                    <td>${neg.volume}</td>
                                </tr>
                            `).join('')}
                    </tbody>

                    <tfoot>
                        <td colspan="3"></td>
                        <td>${
                            //IIFE --> Immediately-Invoked Function Expression
                            // (function() {
                            //         let total = 0;
                            //         modelo.Negociacoes.forEach(neg => {total += neg.volume; });
                            //         return total;

                            // })()
                            
                            // modelo.Negociacoes.reduce( (total, n) => total + n.volume, 0.0)

                            modelo.Negociacoes.VolumeTotal

                        }</td>
                    </tfoot>
                </table>
        `;
    }
}