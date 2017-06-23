class NegociacaoController {
    constructor() {
        //Quando atribuímos a função ou "método da classe document"
        //a referência que é atribuída perde a referência this
        //pois somente a função é "copiada",
        //para corrigir isso, temos o auxílio do metodo
        //bind, que define a referência chamadora, seu "this",
        //no caso document, assim quando jogamos a referência
        //para a variável $, ao chamar a função
        //seu this continuará sendo document, 
        //se comportando normalmente,
        //algo similar é feito com jQuery
        let $ = document.querySelector.bind(document);

        //Estratégia adotada de trazer a busca
        //no DOM dos campos para o contrutor da classe
        //para melhorar a performance,
        //pois é custoso criar e ficar a cada adição
        //tendo que buscar os campos no DOM.
        this._inputDate = $("#data");
        this._inputQuantidade = $("#quantidade");
        this._inputValor = $("#valor");
        this._listaNegociacoes = new ListaNegociacoes();

        //cria propriedade (_negociacoesView) que recebe a instância de (NegociacoesView) e
        //passamos para ela o elemento do DOM que ela vai atribuir seu valor
        this._negociacoesView = new NegociacoesView($('#negociacoesView'));

        //metodo responsável por fazer o "binding" do template HTML da tabela
        //para cima do objeto DOM passado como parâmetro para o construtor da classe
        this._negociacoesView.update(this._listaNegociacoes);

        this._mensagem = new Mensagem();
        this._mensagemView = new MensagemView($('#mensagemView'));
        this._mensagemView.update(this._mensagem);
    }

    adiciona(event) {
        event.preventDefault();
        this._listaNegociacoes.adiciona(this._criaNegociacao());
        console.log(this._listaNegociacoes.getNegociacoes);
        //metodo responsável por fazer o "binding" do template HTML da tabela
        //para cima do objeto DOM passado como parâmetro para o construtor da classe
        this._negociacoesView.update(this._listaNegociacoes);
        
        this._mensagem.texto = "Negociação adicionada com sucesso!";
        this._mensagemView.update(this._mensagem);

        this._limpaFormulario();
    }

    apaga(){
        this._listaNegociacoes.esvazia();
        this._negociacoesView.update(this._listaNegociacoes);
        
        this._mensagem.texto = "Negociações excluídas com sucesso!";
        this._mensagemView.update(this._mensagem);
    }

    _criaNegociacao() {
        return new Negociacao(
            DateHelper.textToDate(this._inputDate.value),
            this._inputQuantidade.value,
            this._inputValor.value
        );
    }

    _limpaFormulario() {
        this._inputDate.value = '';
        this._inputQuantidade.value = '1';
        this._inputValor.value = '0.0';

        this._inputDate.focus();
    }
}