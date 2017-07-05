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
}