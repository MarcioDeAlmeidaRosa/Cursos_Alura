function InserePlacar() {
    var corpoTabela = $(".placar").find("tbody");
    var totalPalavras = $(".contador-palavras").text();
    var usuario = "Marcio de Almeida Rosa";
    var botaoRemover = '<i class="small material-icons deletar">delete</i>';
    var linha = "<tr><td>" + usuario + "</td><td>" + totalPalavras + "</td><td>" + botaoRemover + "</td></tr>";

    // corpoTabela.append(NovaLinha(usuario, totalPalavras)); //--> adiciona no final
    corpoTabela.prepend(NovaLinha(usuario, totalPalavras)); //--> adiciona no final
}

function NovaLinha(usuario, totalPalavras) {
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

function AplicarRemover() {
    $(".deletar").click(function() {
        $(this).closest("tr").remove();
    });
}