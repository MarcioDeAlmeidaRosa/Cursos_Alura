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



module.exports = app;

//Outra forma de devolver o módulo
//A diferença que toda ver que esse módulo for carregado
//será gerada novos objetos de express 
// module.exports = function() {
//     var express = require('express');
//     var app = express();
//     app.set('view engine', 'ejs');
//     return app;
// }