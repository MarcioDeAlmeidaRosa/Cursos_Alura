class MensagemView extends View {

    constructor(elemento){
        super(elemento);
    }
    
    _template(model){
        return `<p class="${(model.texto)?'':'display:none;'}alert alert-info">${model.texto}</p>`;
    }
}