class ProxyFactory {
    static create(objeto, props, acao) {
        return new Proxy(objeto, {
            get(target, prop, receiver) {
                // get: function(target, prop, receiver) {
                if ((props.includes(prop)) && (ProxyFactory._isFunction(target[prop]))) {
                    return function() {
                        console.log(`interceptado o metodo ${prop}`);
                        let rotorno = Reflect.apply(target[prop], target, arguments);
                        //self._negociacoesView.update(target);
                        acao(target);
                        return rotorno;
                    }
                }
                console.log(`interceptado a propriedade ${prop}`);
                return Reflect.get(target, prop, receiver);
            },
            set(target, prop, value, receiver) {
                console.log(`Inserceptando ${prop}`);
                let retorno = Reflect.set(target, prop, value, receiver);
                if (props.includes(prop)) acao(target);
                return retorno;
            }
        });
    }

    static _isFunction(func) {
        return (typeof(func) == typeof(Function));
    }
}