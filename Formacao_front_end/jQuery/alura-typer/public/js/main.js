console.log("olá mundo");
var frase = jQuery(".frase").text();
console.log(frase);

var totalPalavras = frase.split(" ");
var tamanho = totalPalavras.length;
console.log(totalPalavras.length);
$(".informacoes").children("li").first().text(tamanho + " palavra" + ((tamanho > 1) ? "s" : ""));