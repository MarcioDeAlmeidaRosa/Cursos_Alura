console.log("olá mundo");
var frase = jQuery(".frase").text();
console.log(frase);

var totalPalavras = frase.split(" ");
var tamanho = totalPalavras.length;
console.log(totalPalavras.length);
// recuperando todos os li filhos de informacoes
// recuperando o primeiro e alterando o texto
// usando ternário para colocar plural na descrição do contador
$(".informacoes").children("li").first().text(tamanho + " palavra" + ((tamanho > 1) ? "s" : ""));

//$(".campo-digitacao").on("click", function() {
$(".campo-digitacao").on("input", function() {
    var palavras = $(this).val().trim().split(/\S+/);

    $(".contador-caracteres").text($(this).val().length);
    $(".contador-palavras").text(palavras.length - 1);

    if ($(this).val().trim().length > 1)
        $(".s-contador-caracteres").show();
    else
        $(".s-contador-caracteres").hide();

    if ((palavras.length - 1) > 1)
        $(".s-contador-palavras").show();
    else
        $(".s-contador-palavras").hide();

});