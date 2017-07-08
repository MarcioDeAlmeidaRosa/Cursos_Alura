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

        this._listaNegociacoes = new Bind(new ListaNegociacoes(),
            new NegociacoesView($('#negociacoesView')), 'adiciona', 'esvazia', 'ordena', 'inverteOrdem');

        this._mensagem = new Bind(new Mensagem(),
            new MensagemView($('#mensagemView')), 'texto');

        //Variável para controle de alteração crescente e decrescente
        this._ordemAtual = '';

        //let self = this;
        // this._listaNegociacoes = new Proxy(new ListaNegociacoes(), {
        //     get(target, prop, receiver) {
        //         // get: function(target, prop, receiver) {
        //         if ((['adiciona', 'esvazia'].includes(prop)) && (typeof(target[prop]) == typeof(Function))) {
        //             return function() {
        //                 console.log(`interceptado o metodo ${prop}`);
        //                 Reflect.apply(target[prop], target, arguments);
        //                 self._negociacoesView.update(target);
        //             }
        //         }
        //         console.log(`interceptado a propriedade ${prop}`);
        //         return Reflect.get(target, prop, receiver);
        //     }
        // });

        // this._listaNegociacoes = new ListaNegociacoes(model =>
        //     //metodo responsável por fazer o "binding" do template HTML da tabela
        //     //para cima do objeto DOM passado como parâmetro para o construtor da classe
        //     //PS: o escopo da arrow function é léxico, então o this não muda
        //     //é assumido o this do momento da criação e não altera
        //     this._negociacoesView.update(model)
        // );

        //cria propriedade (_negociacoesView) que recebe a instância de (NegociacoesView) e
        //passamos para ela o elemento do DOM que ela vai atribuir seu valor
        //faz a primeira renderização da lista
        // this._negociacoesView = new NegociacoesView($('#negociacoesView'));

        // this._listaNegociacoes = ProxyFactory.create(new ListaNegociacoes(), ['adiciona', 'esvazia'],
        //     (model) => this._negociacoesView.update(model));


        // this._negociacoesView.update(this._listaNegociacoes);

        // this._mensagemView = new MensagemView($('#mensagemView'));

        // this._mensagem = ProxyFactory.create(new Mensagem(), ['texto'],
        //     (model) => this._mensagemView.update(model));


        // this._mensagemView.update(this._mensagem);
    }

    adiciona(event) {
        event.preventDefault();
        this._listaNegociacoes.adiciona(this._criaNegociacao());
        console.log(this._listaNegociacoes.Negociacoes);

        console.log('atribuindo texto');
        this._mensagem.texto = "Negociação adicionada com sucesso!";
        // this._mensagemView.update(this._mensagem);

        this._limpaFormulario();
    }

    apaga() {
        this._listaNegociacoes.esvazia();
        this._mensagem.texto = "Negociações excluídas com sucesso!";
        // this._mensagemView.update(this._mensagem);
    }

    importaNegociacoes() {
        let service = new NegociacaoService();

        //PYRAMID OF DOOM  -> PIRÂMIDE DO DESTINO
        //CALLBACK HELL    -> INFERNO DE CALLBACK
        // service.obterNegociacoesSemana((error, negociacoes) => {
        //     if (error) {
        //         this._mensagem.texto = error;
        //         return;
        //     }
        //     negociacoes.forEach(negociacao => this._listaNegociacoes.adiciona(negociacao));

        //     service.obterNegociacoesSemanaAnterior((error, negociacoes) => {
        //         if (error) {
        //             this._mensagem.texto = error;
        //             return;
        //         }
        //         negociacoes.forEach(negociacao => this._listaNegociacoes.adiciona(negociacao));

        //         service.obterNegociacoesSemanaRetrasada((error, negociacoes) => {
        //             if (error) {
        //                 this._mensagem.texto = error;
        //                 return;
        //             }
        //             negociacoes.forEach(negociacao => this._listaNegociacoes.adiciona(negociacao));
        //             this._mensagem.texto = "Negociações importada com sucesso.";
        //         });
        //     });
        // });

        //USANDO PROMISE - NIVEL 1
        // service.obterNegociacoesSemana().then(negociacoes => {
        //     negociacoes.forEach(negociacao => this._listaNegociacoes.adiciona(negociacao));
        //     this._mensagem.texto = "Negociações da semana obtida com sucesso.";
        // }).catch(error => this._mensagem.texto = error);

        // service.obterNegociacoesSemanaAnterior().then(negociacoes => {
        //     negociacoes.forEach(negociacao => this._listaNegociacoes.adiciona(negociacao));
        //     this._mensagem.texto = "Negociações da semana anterior obtida com sucesso.";
        // }).catch(error => this._mensagem.texto = error);

        // service.obterNegociacoesSemanaRetrasada().then(negociacoes => {
        //     negociacoes.forEach(negociacao => this._listaNegociacoes.adiciona(negociacao));
        //     this._mensagem.texto = "Negociações da semana retrasada obtida com sucesso.";
        // }).catch(error => this._mensagem.texto = error);

        //USANDO PROMISE - NIVEL 2
        // Promise.all([service.obterNegociacoesSemana(),
        //         service.obterNegociacoesSemanaAnterior(),
        //         service.obterNegociacoesSemanaRetrasada()
        //     ])
        //     .then(negociacoes => {
        //         console.log(negociacoes);
        //         negociacoes.reduce((arrayAchatado, array) => arrayAchatado.concat(array), [])
        //             .forEach(negociacao => this._listaNegociacoes.adiciona(negociacao));
        //         this._mensagem.texto = "Negociações da semana anterior obtida com sucesso.";
        //     }).catch(error => this._mensagem.texto = error);

        service.obterNegociacoes()
            .then(negociacoes => {
                negociacoes.forEach(negociacao => this._listaNegociacoes.adiciona(negociacao));
                this._mensagem.texto = "Negociações da semana anterior obtida com sucesso.";
            }).catch(error => this._mensagem.texto = error);
    }

    ordena(coluna){
        if (this._ordemAtual === coluna){
            this._listaNegociacoes.inverteOrdem();
        }
        else{
            this._listaNegociacoes.ordena((a, b) => a[coluna] - b[coluna]); 
        }
        this._ordemAtual = coluna;
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