class NegociacaoService {
    obterNegociacoesSemana(cb) {
        //objeto para fazer chamadas ao server
        let xhr = new XMLHttpRequest();
        //Preparando página para abrir a requisição
        xhr.open('GET', 'negociacoes/semana');

        /* 
            0: requisição ainda não iniciada.
            1: conexão com o servidor estabelecida.
            2: requisição recebida.
            3: processando requisição.
            4: requisição concluída e a resposta esta pronta.
        */
        //configurações
        //...onreadystatechange -> ser executada automaticamente cada vez que há uma alteração no estado da requisição.
        xhr.onreadystatechange = () => {
            //4: requisição concluída e a resposta esta pronta
            if (xhr.readyState == 4) {
                if (xhr.status == 200) {
                    console.log('Obtendo as negociações do servidor');
                    console.log(xhr.responseText);
                    let negociacoes = JSON.parse(xhr.responseText);
                    console.log(negociacoes);

                    cb(null, negociacoes
                        .map(objeto => new Negociacao(new Date(objeto.data), objeto.quantidade, objeto.valor)));
                } else {
                    console.log(xhr.responseText);
                    cb('Não foi possível obter as negociações.', null);
                }
            }
        };
        //executando a requisição
        xhr.send();
    }
}