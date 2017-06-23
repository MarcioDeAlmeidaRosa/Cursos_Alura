class MensagemView extends View {
    template(model){
        return `<p class="${(model.texto)?'':'display:none;'}alert alert-info">${model.texto}</p>`;
    }
}