//Importa biblioteca para trabalhar com web
var http = require('http');
//configurando porta
var porta = 3000;
//configurando domínio
var ip = "localhost";
//Criando o objeto que representa o servidor
var servidor = http.createServer(
    //O node é programado em cima de funções callbacks
    function(req, res){
        if (req.url == "/produtos"){
            res.end('<html><head><meta charset="UTF-8"></head><body>Listando os produtos da loja...</body><html>');
        }else{
            res.end('<html><head><meta charset="UTF-8"></head><body>Home da casa do código</body><html>');
        }
    }
);
//configurando escuta no endereço e porta da variável
servidor.listen(porta, ip);
console.log('servidor rodando');