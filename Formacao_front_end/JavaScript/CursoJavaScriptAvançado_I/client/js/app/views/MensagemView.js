class MensagemView{
    constructor(elemento){
        this._elemento = elemento;
    }

    _template(model){
        return `<p class="${(model.texto)?'':'display:none;'}alert alert-info">${model.texto}</p>`;
    }

    update(model){
        this._elemento.innerHTML = this._template(model);
    }

}