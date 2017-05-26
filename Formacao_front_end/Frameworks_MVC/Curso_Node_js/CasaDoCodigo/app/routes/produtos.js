module.exports = function(app) {
    //configurando a rota --> o metodo e conteúdo que iremos atender
    app.get('/produtos', function(req, res) {
        //usando ejs
        res.render("produtos/lista");
        //res.send('<html><head><meta charset="UTF-8"></head><body><h1>Listando os produtos da loja...</h1></body><html>');
    });
}