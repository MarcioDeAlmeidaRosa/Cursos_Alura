class HttpService {
    get(url) {
        return new Promise((resolve, reject) => {
            //objeto para fazer chamadas ao server
            let xhr = new XMLHttpRequest();
            //Preparando página para abrir a requisição
            xhr.open('GET', url);
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
                        resolve(JSON.parse(xhr.responseText));
                    } else {
                        reject(xhr.responseText);
                    }
                }
            };
            //executando a requisição
            xhr.send();
        });
    }

    post(url, dado){
        return new Promise((resolve, reject) => {
            //enviar o XMLHttpRequest
            //criando objeto para comunicação com o servidor
            let xhr = new XMLHttpRequest();
            //DOMString method,DOMString url,optional boolean async, optional DOMString user,optional DOMString password
            xhr.open('POST', url, true);
            //setando o cabeçalho
            xhr.setRequestHeader('Content-type', 'application/json');
            xhr.onreadystatechange = () => {
                if (xhr.readyState == 4) {
                    if (xhr.status == 200) {
                        resolve(JSON.parse(xhr.responseText));
                    } else {
                        reject(xhr.responseText);
                    }
                }
            };
            //chamando o cadastramento
            xhr.send(JSON.stringify(dado));
        });
    }
}