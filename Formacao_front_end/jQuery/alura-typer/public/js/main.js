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
    AplicarRemover();
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
    AplicarRemover();
}

function InserePlacar() {
    var corpoTabela = $(".placar").find("tbody");
    var totalPalavras = $(".contador-palavras").text();
    var usuario = "Marcio de Almeida Rosa";
    var botaoRemover = '<i class="small material-icons deletar">delete</i>';
    var linha = "<tr><td>" + usuario + "</td><td>" + totalPalavras + "</td><td>" + botaoRemover + "</td></tr>";

    // corpoTabela.append(CriaLinha(usuario)); //--> adiciona no final
    corpoTabela.prepend(CriaLinha(usuario, totalPalavras)); //--> adiciona no final
}

function AplicarRemover() {
    $(".deletar").click(function() {
        $(this).closest("tr").remove();
    });
}

function CriaLinha(usuario, totalPalavras) {
    var linha = $("<tr>")
    var colunaUsuario = $("<td>").text(usuario);
    var colunaPalavras = $("<td>").text(totalPalavras);
    var colunaRemover = $("<td>");
    var link = $("<i>").addClass("small").addClass("material-icons").addClass("deletar").text("delete");

    colunaRemover.append(link);

    linha.append(colunaUsuario);
    linha.append(colunaPalavras);
    linha.append(colunaRemover);

    return linha;
}