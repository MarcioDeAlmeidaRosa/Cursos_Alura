//Recupera a instância de app
var app = require('./config/express');
//Apendando o arquivo de rota
var rotasProdutos = require('./app/routes/produtos')(app);
//configurando a porta que o servidor que acabamos de criar irá escutar
app.listen(3000, function() {
    console.log('Serviço da casa do código esta rodando!');
});