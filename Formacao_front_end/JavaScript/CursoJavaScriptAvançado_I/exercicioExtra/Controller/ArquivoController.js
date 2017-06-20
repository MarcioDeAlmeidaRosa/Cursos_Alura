class ArquivoController {
    constructor() {
        this._imputDados = document.querySelector('.dados-arquivo');
        this._tboty = document.querySelector('tbody');
    }

    get arquivos() {
        return [].concat(this._listaArquivo);
    }

    enviar() {
        this._adicionaLinha(new Arquivo(...this._imputDados.value.split('|')));
        this._limparFormulario();
    }

    _limparFormulario() {
        this._imputDados.value = "";
        this._imputDados.focus();
    }

    _adicionaLinha(arquivo) {
        console.log(`Dados do arquivo: ${arquivo.nome}, ${arquivo.tamanho}, ${arquivo.tipo}`);
        let tr = document.createElement("tr");
        let nome = document.createElement("td");
        nome.appendChild(document.createTextNode(arquivo.nome));
        let tamanho = document.createElement("td");
        tamanho.appendChild(document.createTextNode(arquivo.tamanho));
        let tipo = document.createElement("td");
        tipo.appendChild(document.createTextNode(arquivo.tipo));

        tr.appendChild(nome);
        tr.appendChild(tamanho);
        tr.appendChild(tipo);
        this._tboty.appendChild(tr);
        console.log(this._tboty);
    }
}