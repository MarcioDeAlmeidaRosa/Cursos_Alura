class DateHelper {

    constructor() {
        throw new Erro('Esta classe não pode ser instanciada');
    }

    static dateToText(date) {
        // Template Strings
        return `${date.getDate()}/${(date.getMonth() + 1)}/${date.getFullYear()}`;
    }

    static textToDate(text) {
        //fail fast

        if (!(/\d{4}-\d{2}-\d{2}/.test(text)))
            throw new Error('Data deve ser informada no padrão YYYY-MM-DD');

        console.log(typeof(text));
        console.log(text);
        console.log("//1º - Forma - usando aplit");
        console.log(new Date(text.split("-")));
        console.log("-------------------------------------------------------------------------------------------------");
        console.log("//2º - Forma - usando replace com regex");
        console.log(new Date(text.replace(/-/g, ',')));
        console.log("-------------------------------------------------------------------------------------------------");
        console.log("//3º - Forma - usando split recuperando as casas");
        let ano = parseInt(text.split("-")[0]);
        let mes = parseInt(text.split("-")[1]) - 1;
        let dia = parseInt(text.split("-")[2]);
        console.log(new Date(ano, mes, dia));
        console.log("-------------------------------------------------------------------------------------------------");
        console.log("//4º - Forma - usando spread operator");
        console.log(
            new Date(
                text
                .split("-")
                .map(function(item, indice) {
                    if (indice == 1)
                        return item - 1;
                    return item;
                })
            ));
        console.log("-------------------------------------------------------------------------------------------------");
        console.log("//5º - Forma - usando spread operator - e calculo de módulo");
        console.log(
            new Date(
                text
                .split("-")
                .map(function(item, indice) {
                    return item - (item % 2);
                })
            ));
        console.log("-------------------------------------------------------------------------------------------------");
        console.log("//6º - Forma - usando arrow functions - e calculo de módulo");
        console.log(
            new Date(
                text
                .split("-")
                .map((item, indice) => {
                    return item - (item % 2);
                })
            ));
        console.log("-------------------------------------------------------------------------------------------------");

        console.log("//7º - Forma - usando arrow functions simplificada por só ter uma instrução - e calculo de módulo");
        console.log(
            new Date(
                text
                .split("-")
                .map((item, indice) => item - (item % 2))
            ));

        return new Date(text.split("-"));
    }

}