class NegociacaoService {
    //IMPLEMENTAÇÃO DO PADRÃO PROMISE
    obterNegociacoesSemana() {
        return new Promise((resolve, reject) => {
            let xhr = new XMLHttpRequest();
            xhr.open('GET', 'negociacoes/semana');
            xhr.onreadystatechange = () => {
                if (xhr.readyState == 4) {
                    if (xhr.status == 200) {
                        resolve(JSON.parse(xhr.responseText).map(objeto => new Negociacao(new Date(objeto.data), objeto.quantidade, objeto.valor)));
                    } else {
                        console.log(xhr.responseText);
                        reject('Não foi possível obter as negociações da semana.');
                    }
                }
            };
            xhr.send();
        });
    }

    obterNegociacoesSemanaAnterior() {
        return new Promise((resolve, reject) => {
            let xhr = new XMLHttpRequest();
            xhr.open('GET', 'negociacoes/anterior');
            xhr.onreadystatechange = () => {
                if (xhr.readyState == 4) {
                    if (xhr.status == 200) {
                        resolve(JSON.parse(xhr.responseText).map(objeto => new Negociacao(new Date(objeto.data), objeto.quantidade, objeto.valor)));
                    } else {
                        console.log(xhr.responseText);
                        reject('Não foi possível obter as negociações da semana anterior.');
                    }
                }
            };
            xhr.send();
        });
    }

    obterNegociacoesSemanaRetrasada() {
        return new Promise((resolve, reject) => {
            let xhr = new XMLHttpRequest();
            xhr.open('GET', 'negociacoes/retrasada');
            xhr.onreadystatechange = () => {
                if (xhr.readyState == 4) {
                    if (xhr.status == 200) {
                        resolve(JSON.parse(xhr.responseText).map(objeto => new Negociacao(new Date(objeto.data), objeto.quantidade, objeto.valor)));
                    } else {
                        console.log(xhr.responseText);
                        reject('Não foi possível obter as negociações da semana retrasada.');
                    }
                }
            };
            xhr.send();
        });
    }

    //USANDO PADRÃO CALLBACK
    // obterNegociacoesSemana(cb) {
    //     //objeto para fazer chamadas ao server
    //     let xhr = new XMLHttpRequest();
    //     //Preparando página para abrir a requisição
    //     xhr.open('GET', 'negociacoes/semana');
    //     /* 
    //         0: requisição ainda não iniciada.
    //         1: conexão com o servidor estabelecida.
    //         2: requisição recebida.
    //         3: processando requisição.
    //         4: requisição concluída e a resposta esta pronta.
    //     */
    //     //configurações
    //     //...onreadystatechange -> ser executada automaticamente cada vez que há uma alteração no estado da requisição.
    //     xhr.onreadystatechange = () => {
    //         //4: requisição concluída e a resposta esta pronta
    //         if (xhr.readyState == 4) {
    //             if (xhr.status == 200) {
    //                 console.log('Obtendo as negociações do servidor');
    //                 console.log(xhr.responseText);
    //                 let negociacoes = JSON.parse(xhr.responseText);
    //                 console.log(negociacoes);

    //                 cb(null, negociacoes
    //                     .map(objeto => new Negociacao(new Date(objeto.data), objeto.quantidade, objeto.valor)));
    //             } else {
    //                 console.log(xhr.responseText);
    //                 cb('Não foi possível obter as negociações da semana.', null);
    //             }
    //         }
    //     };
    //     //executando a requisição
    //     xhr.send();
    // }

    // obterNegociacoesSemanaAnterior(cb) {
    //     //objeto para fazer chamadas ao server
    //     let xhr = new XMLHttpRequest();
    //     //Preparando página para abrir a requisição
    //     xhr.open('GET', 'negociacoes/anterior');
    //     /* 
    //         0: requisição ainda não iniciada.
    //         1: conexão com o servidor estabelecida.
    //         2: requisição recebida.
    //         3: processando requisição.
    //         4: requisição concluída e a resposta esta pronta.
    //     */
    //     //configurações
    //     //...onreadystatechange -> ser executada automaticamente cada vez que há uma alteração no estado da requisição.
    //     xhr.onreadystatechange = () => {
    //         //4: requisição concluída e a resposta esta pronta
    //         if (xhr.readyState == 4) {
    //             if (xhr.status == 200) {
    //                 console.log('Obtendo as negociações do servidor');
    //                 console.log(xhr.responseText);
    //                 let negociacoes = JSON.parse(xhr.responseText);
    //                 console.log(negociacoes);

    //                 cb(null, negociacoes
    //                     .map(objeto => new Negociacao(new Date(objeto.data), objeto.quantidade, objeto.valor)));
    //             } else {
    //                 console.log(xhr.responseText);
    //                 cb('Não foi possível obter as negociações da semana anterior.', null);
    //             }
    //         }
    //     };
    //     //executando a requisição
    //     xhr.send();
    // }

    // obterNegociacoesSemanaRetrasada(cb) {
    //     //objeto para fazer chamadas ao server
    //     let xhr = new XMLHttpRequest();
    //     //Preparando página para abrir a requisição
    //     xhr.open('GET', 'negociacoes/retrasada');
    //     /* 
    //         0: requisição ainda não iniciada.
    //         1: conexão com o servidor estabelecida.
    //         2: requisição recebida.
    //         3: processando requisição.
    //         4: requisição concluída e a resposta esta pronta.
    //     */
    //     //configurações
    //     //...onreadystatechange -> ser executada automaticamente cada vez que há uma alteração no estado da requisição.
    //     xhr.onreadystatechange = () => {
    //         //4: requisição concluída e a resposta esta pronta
    //         if (xhr.readyState == 4) {
    //             if (xhr.status == 200) {
    //                 console.log('Obtendo as negociações do servidor');
    //                 console.log(xhr.responseText);
    //                 let negociacoes = JSON.parse(xhr.responseText);
    //                 console.log(negociacoes);

    //                 cb(null, negociacoes
    //                     .map(objeto => new Negociacao(new Date(objeto.data), objeto.quantidade, objeto.valor)));
    //             } else {
    //                 console.log(xhr.responseText);
    //                 cb('Não foi possível obter as negociações da semana retrasada.', null);
    //             }
    //         }
    //     };
    //     //executando a requisição
    //     xhr.send();
    // }
}