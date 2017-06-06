/* IMPORTANTE SOBRE SELETORES
Nos seletores do jQuery

- Nomes de tags (tr, table, etc...)                  --> compatível com CSS
- .classes, #ids e [value='Undo'] atributos/valores  --> compatível com CSS
- Extensóes (visible, hidden)                        --> compatível com jQuery

id = tem que ser único

*/
var umaPropaganda = function() {
    var propagandas = [
        "O que acha de comprar uma motocicleta?",
        "O que acha de comprar uma lancha?",
        "O que acha de comprar uma bicicleta?",
        "O que acha de comprar um carro?"
    ];
    var posicao = Math.floor(propagandas.length * Math.random());
    var texto = propagandas[posicao];
    var tr = $("<tr>").addClass("propaganda").append($("<td>"));
    tr.find("td").attr("colspan", 6).text(texto);
    return tr;
};

var removeMarcacaoUndo = function(objeto) {
    //alert(objeto);
    //Remove classe dinamicamente
    $(objeto).closest(".carrinho").find("tr:visible").removeClass("recuperado");
};

var atualizaDados = function() {
    $(".carrinho").each(function() {
        var carrinho = $(this);

        var itens = carrinho.find(".item-total:visible");
        var total = 0;
        for (var i = 0; i < itens.length; i++) {
            //Recuperando o td e tranformando em um objeto jQeury
            var item = $(itens[i]);
            // gera uma nova variável com a conversào do 
            //texto para ponto flutuante para poder 
            //fazer o calculo.
            //debugger;
            var valor = parseFloat(item.text());
            total += valor;
        }
        console.log("Valor total: " + total);
        carrinho.find(".valor-total").text(total);
        carrinho.find(".total-itens").text(itens.length);
    });
};

//FORMA DE ATRIBUIR UM CSS DINAMICAMENTE - NÃO RECOMENDADO ALTERAR ESTILO DENTRO DO JAVASCRIPT
var darDestaque = function() {
    $(this).css({
        "background": "#CCC",
        "opacity": 0.5
    });
};

//FORMA DE ATRIBUIR UM CSS DINAMICAMENTE - NÃO RECOMENDADO ALTERAR ESTILO DENTRO DO JAVASCRIPT
var tirarDestaque = function() {
    $(this).css({
        "background": "",
        "opacity": 1
    });
};

var darDestaque2 = function() {
    $(this).addClass("hovering");
};

var tirarDestaque2 = function() {
    $(this).removeClass("hovering");
};


var removeItem = function(event) {
    // remove marcação ds itens que foram recuperados anteriormente
    removeMarcacaoUndo(this);
    //parâmetro enviado no click
    //podemos executar a função preventDefault
    //que privine o comportamente default
    //que é enviar para alguma página depois de clicar no link
    event.preventDefault();
    //Quado uma função é invocada, via callback é passado
    //para a função, e essa refência é o this
    //o this aqui é o link
    var self = $(this);
    //para remover a linha da tabela, precisamos chegar 
    //no pai dele, para isso precisamos chegar ele pela função parent
    //self.parent().parent().remove();

    self.closest("tr").hide();
    //Criando função centralizadora que atualiza os totais da tela
    atualizaDados();
    //força a saida da função para não executar 
    //os códigos abaixo, serão mantidos somente para exemplos
    return;

    //Não é trivial sair acessando parent após parent
    //para isso temos o seletor closest
    //que sai navegando na arvore, procurando a primeira 
    //incidência do seletor passado
    self.closest("tr").remove();



    var totalItem = parseInt($("#total-itens").text());
    var novaQtd = totalItem - 1;
    $("#total-itens").text(novaQtd);


    //FORMA DE CALCULO 1 - DEIXA A ESTRUTURA FIXA
    //para recuperar o preço do item que estamos removendo e
    //subtrair to total, para isso acessamos o elemento pai do link
    //e quando chegamos nele, mudamos a seleção para o elemento anterior
    //desta forma chegamos no td de preço
    var precoItem = parseFloat(self.parent().prev().text());
    //acessamos a informação de valor total gravado na tela
    //tranformamos o texto em float
    var valorTotal = parseFloat($("#valor-total").text());
    //calcula o novo valor
    var precoFinal = valorTotal - precoItem;
    //entào subtraímos do valor total, o valor do item
    //que estamos removendo
    $("#valor-total").text(precoFinal);


    //FORMA DE CALCULO 2 - DEIXA A ESTRUTURA UM POUCO MAIS FLEXÍVEL
    // outra forma de chegar nesse valor é não usar a estrutura fixa
    // para isso podemos usar algo similar feito antes
    // acessar um determinado elemento mais perto do meu self
    // e dele procuramos e um dos irmãos que tem a classe item-total
    var precoItem = parseFloat(self.closest("td").siblings(".item-total").first().text());
    //então fazemos o calculo
    precoFinal = valorTotal - precoItem;
    //e atualizamos o valor total
    $("#valor-total").text(precoFinal);


    //FORMA 3 - DEIXA A ESTRUTURA BEM MAIS FLEXÍVEL
    // uma outra forma de chegar nesse valor é subir até o elemento da linha
    // quando chegarmos nele, pegamos um elemento filho/neto/tataranete, 
    //etc cujo tenha a classe item-total configurada no elemento
    //dessa forma deixamos nosso código um pouco mais desacoplado
    //da estrutura do nosso HTML, permitindo a qq momento que a ordem
    //das colunas sejam alteradas, não quebrando nossa lógica da somatória
    var precoAgoraVai = parseFloat(self.closest("tr").find(".item-total").text());
    //então fazemos o calculo
    precoFinal = valorTotal - precoAgoraVai;
    //e atualizamos o valor total
    $("#valor-total").text(precoFinal);
};

var undo = function() {
    //var carrinho = this.closest(".carrinho");
    //alert('undo');
    removeMarcacaoUndo(this);
    //Recupera somente os trs que estão com a classe hidden ativada
    //e adiciona classe dinamicamente
    $(this).closest(".carrinho").find("tr:hidden").addClass("recuperado").show();
    //Configura para ser executado 1 vez 
    //para remover automático a classe nos itens 
    //que foram retornados para a tela
    //setTimeout(removeMarcacaoUndo(this), 9000);

    // Atualiza os totalizadores
    atualizaDados();
};

var aposInicializado = function() {
    //Criando função centralizadora que atualiza os totais da tela
    atualizaDados();
    //registra o evento click para todos os itens que estiverem a
    //classe remove-item configurada 
    //assim quando clicada, chama a função de remover
    $(".remove-item").click(removeItem);
    $("[value='Undo']").click(undo);

    // $("tr:nth-child(3n)").each(function() {
    //     umaPropaganda().insertAfter($(this));
    // });

    // $("tr:nth-child(3n)").each(function() {
    //     umaPropaganda().insertBefore($(this));
    // });

    // $("tr:nth-child(even)").each(function() {
    //     umaPropaganda().insertAfter($(this));
    // });

    // $("tr:nth-child(odd)").each(function() {
    //     umaPropaganda().insertAfter($(this));
    // });

    $(".carrinho").find("tbody tr:nth-child(2n), tr:last").each(function() {
        umaPropaganda().insertBefore($(this));
    });

    //1 FORMA DE MUDAR O CSS DINAMICAMENTE - NÃO RECOMENDADO
    //                    quando enrta, quando sai
    //$('tbody tr').hover(darDestaque, tirarDestaque);
    //                    quando enrta, quando sai
    //$('tbody tr').hover(darDestaque2, tirarDestaque2);


    //vincula evento ao passar o mouse por cima da tr
    $("tbody tr").on("mouseenter", darDestaque2);
    //vincula evento ao sair com o mouse de cima da tr
    $("tbody tr").on("mouseleave", tirarDestaque2);

};
//Toda função passada desta forma para o 
//jQuery só vai ser executada após o total 
//carregamento da página
$(aposInicializado);
$(alert('Outras bibliotecas javascript: Prototype, Ext JS, JQuery UI, Mustache, Prime UI'));