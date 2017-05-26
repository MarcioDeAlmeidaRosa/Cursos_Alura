//Recupera a instância de app
var app = require('./config/express');



//configurando a rota --> o metodo e conteúdo que iremos atender
app.get('/produtos', function(req, res) {
    //usando ejs
    res.render("produtos/lista");
    //res.send('<html><head><meta charset="UTF-8"></head><body><h1>Listando os produtos da loja...</h1></body><html>');
});
//configurando a porta que o servidor que acabamos de criar irá escutar
app.listen(3000, function() {
    console.log('Serviço da casa do código esta rodando!');
});