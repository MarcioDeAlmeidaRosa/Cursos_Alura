class MensagemView extends View {

    constructor(elemento){
        super(elemento);
    }

    template(model){
        return `<p class="${(model.texto)?'':'display:none;'}alert alert-info">${model.texto}</p>`;
    }
}