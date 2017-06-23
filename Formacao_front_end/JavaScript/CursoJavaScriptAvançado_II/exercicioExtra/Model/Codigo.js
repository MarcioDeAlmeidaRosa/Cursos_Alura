class Codigo {

    constructor(codigo) {
        if (!this._isValid(codigo))
            throw new Error('O conteúdo informado não esta no formato correto (ZZZ-ZZ-999)');
        this._text = codigo;
    }

    _isValid(codigo) {
        let _expressionRegular = /\D{3}-\D{2}-\d{3}/;
        return _expressionRegular.test(codigo);
    }

    get textValid() {
        return ''.concat(this._text);
    }
}