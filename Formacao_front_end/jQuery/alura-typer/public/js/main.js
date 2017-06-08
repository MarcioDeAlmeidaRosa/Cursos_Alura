var tempoInicial = parseInt($(".tempo-digitacao").text());
var frasePrincipal = $(".frase");
var campo = $(".campo-digitacao");

//função que só será executada quando 
//todo o documento estiver carregado
// $(document).ready();
//podemos usar de forma resumida
$(function() {
    atualizaTamanhoFrase();
    inicializaCronometro();
    inicializaContadores();
    $("#reiniciar").click(reiniciaJogo);
    InicializaMarcador();
});

function atualizaTamanhoFrase() {
    var frase = frasePrincipal.text();
    var totalPalavras = frase.split(" ");
    var tamanho = totalPalavras.length;
    // recuperando todos os li filhos de informacoes
    // recuperando o primeiro e alterando o texto
    // usando ternário para colocar plural na descrição do contador
    // $(".informacoes").children("li").first().text(tamanho + " palavra" + ((tamanho > 1) ? "s" : ""));
    $('.total-palavras').text(tamanho);
    $('.plural-total-palavras').text((tamanho > 1) ? "s" : "")
}

function inicializaTotais() {
    var palavras = campo.val().trim().split(/\S+/);

    $(".contador-caracteres").text(campo.val().length);
    $(".contador-palavras").text(palavras.length - 1);

    if (campo.val().trim().length > 1)
        $(".s-contador-caracteres").show();
    else
        $(".s-contador-caracteres").hide();

    if ((palavras.length - 1) > 1)
        $(".s-contador-palavras").show();
    else
        $(".s-contador-palavras").hide();
}

function inicializaContadores() {
    //campo.on("click", function() {
    campo.on("input", inicializaTotais);
}

function inicializaCronometro() {
    var tempoRestante = tempoInicial;
    // campo.on("focus", function() {
    //função one similar a on -> porém só vai escutar o evento 1 única vez
    //já a on, toda vez que ele se repetir, será executado
    campo.one("focus", function() {
        var id = setInterval(function() {
            tempoRestante--;
            $(".tempo-digitacao").text(tempoRestante);
            if (tempoRestante === 0) {
                finalizaJogo(id);
            }
            //1000 milesegundos = 1 segundo
        }, 1000);
    });
}

function reiniciaJogo() {
    campo.attr("disabled", false);
    campo.val("");
    inicializaTotais();
    $(".tempo-digitacao").text(tempoInicial);
    inicializaCronometro();
    campo.toggleClass("campo-desativado");
    campo.removeClass("digitacao-incorreta");
    campo.removeClass("digitacao-correta");
}

function InicializaMarcador() {
    campo.on("input", function() {
        var digitado = $(this).val();
        var textoComparador = frasePrincipal.text().substr(0, digitado.length);
        var correto = (digitado === textoComparador);
        campo.toggleClass("digitacao-correta", correto);
        campo.toggleClass("digitacao-incorreta", !correto);
    });
}

function finalizaJogo(id) {
    campo.attr("disabled", true);
    campo.toggleClass("campo-desativado");
    //para a execução do setInterval
    clearInterval(id);
    InserePlacar();
}

function InserePlacar() {
    var corpoTabela = $(".placar").find("tbody");
    var linha = "<tr><td>Marcus</td><td>1</td></tr>";
    // corpoTabela.append(linha); //--> adiciona no final
    corpoTabela.prepend(linha); //--> adiciona no final
}