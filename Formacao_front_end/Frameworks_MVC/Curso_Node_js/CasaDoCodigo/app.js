//express -->biblioteca js contruida sobre o módulo http do 
//node.js que lida de uma forma mais simples para
//lhe dar com as requisições
//linha abaixo cria a referência para a biblioteca express
var express = require('express');
//Criação do objeto express em memória para ser utilizado
var app = express();

//set -> usado para definir variáveis que pode ser usada 
//por todo o escopo do sistema
//configurando a engeni como ejs
app.set('view engine', 'ejs');

//configurando a rota --> o metodo e conteúdo que iremos atender
app.get('/produtos', function(req,res){
    //usando ejs
    res.render("produtos/lista");
    //res.send('<html><head><meta charset="UTF-8"></head><body><h1>Listando os produtos da loja...</h1></body><html>');
});
//configurando a porta que o servidor que acabamos de criar irá escutar
app.listen(3000, function(){
    console.log('Serviço da casa do código esta rodando!');
});