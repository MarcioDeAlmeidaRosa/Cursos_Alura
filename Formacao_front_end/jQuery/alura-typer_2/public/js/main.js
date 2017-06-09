var tempoInicial = $("#tempo-digitacao").text();
var campo = $(".campo-digitacao");

$(function() {
    atualizaTamanhoFrase();
    inicializaContadores();
    inicializaCronometro();
    inicializaMarcadores();
    $("#botao-reiniciar").click(reiniciaJogo);
});

function atualizaTamanhoFrase() {
    var frase = $(".frase").text();
    var numPalavras = frase.split(" ").length;
    var tamanhoFrase = $("#tamanho-frase");

    tamanhoFrase.text(numPalavras);
}

function inicializaContadores() {
    campo.on("input", function() {
        var conteudo = campo.val();

        var qtdPalavras = conteudo.split(/\S+/).length - 1;
        $("#contador-palavras").text(qtdPalavras);

        var qtdCaracteres = conteudo.length;
        $("#contador-caracteres").text(qtdCaracteres);
    });
}

function inicializaMarcadores() {
    var frase = $(".frase").text();
    campo.on("input", function() {
        var digitado = campo.val();
        var comparavel = frase.substr(0, digitado.length);

        if (digitado == comparavel) {
            campo.addClass("borda-verde");
            campo.removeClass("borda-vermelha");
        } else {
            campo.addClass("borda-vermelha");
            campo.removeClass("borda-verde");
        }
    });
}

function inicializaCronometro() {
    var tempoRestante = $("#tempo-digitacao").text();
    campo.one("focus", function() {
        var cronometroID = setInterval(function() {
            tempoRestante--;
            $("#tempo-digitacao").text(tempoRestante);
            if (tempoRestante < 1) {
                clearInterval(cronometroID);
                finalizaJogo();
            }
        }, 1000);
    });
}

function finalizaJogo() {
    campo.attr("disabled", true);
    campo.toggleClass("campo-desativado");
    inserePlacar();
}

function reiniciaJogo() {
    campo.attr("disabled", false);
    campo.val("");
    $("#contador-palavras").text(0);
    $("#contador-caracteres").text(0);
    $("#tempo-digitacao").text(tempoInicial);
    inicializaCronometro();
    campo.toggleClass("campo-desativado");
    campo.removeClass("borda-vermelha");
    campo.removeClass("borda-verde");
    ScrolFocoPrincipal("body", ".campo-digitacao");
}

function ScrolFocoPrincipal(elementoFoco, elementoReferencia) {
    $(elementoFoco).animate({ scrollTop: $(elementoReferencia).offset().top + "px" }, 1000);
};



/*
$('#botao-promocao').click(function() {
  var promocoes = $('.promocoes');
  if(promocoes.is(':visible')) {
    promocoes.hide();
  } else {
    promocoes.show();
  }
});

O jQuery possui a função is que permite consultar uma pseudo class. Toda vez que um elemento esta com display diferente de none ele ganha a pseudo classe :visible . A função is retorna true caso o elemento esteja visível. Se ele estiver visível, precisamos escondê-lo e isso é feito através da função hide. Para exibir o elemento, é usada a função show.
Alternativa correta

$('#botao-promocao').click(function() {
  $('.promocoes').toggleClass('invisivel');
});

A função toogleClass (não confundir com a função toggle) adiciona uma classe caso ela não exista no elemento. Se existir, ela remove a classe.
Alternativa correta

$('#botao-promocao').click(function() {
  $('.promocoes').toggle();
});

A função toggle é um atalho para as funções hide e show. Quando ela é chamada para um elemento visível, o elemento fica invisível. Quando é chamada para um elemento invisível, ela torna o elemento visível.
Alternativa correta

$('#botao-promocao').click(function() {
  var promocoes = $('.promocoes');
  if(promocoes.hasClass('invisivel')) {
    promocoes.removeClass('invisivel');
  } else {
    promocoes.addClass('invisivel');
  }
});

Esta correta! o jQuery possui a função hasClass que retorna true se um elemento possui ou não uma classe. Na condição, removemos a classe invisivel caso o elemento já a tenha e a adicionamos caso ele não a tenha. Todo esse processo é feito a cada clique do usuário.


*/