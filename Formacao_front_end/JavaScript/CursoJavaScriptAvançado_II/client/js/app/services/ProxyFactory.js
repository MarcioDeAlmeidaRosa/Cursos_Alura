class ProxyFactory {
    static create(objeto, props, acao) {
        return new Proxy(objeto, {
            get(target, prop, receiver) {
                // get: function(target, prop, receiver) {
                if ((props.includes(prop)) && (ProxyFactory._isFunction(target[prop]))) {
                    return function() {
                        console.log(`interceptado o metodo ${prop}`);
                        Reflect.apply(target[prop], target, arguments);
                        //self._negociacoesView.update(target);
                        return acao(target);
                    }
                }
                console.log(`interceptado a propriedade ${prop}`);
                return Reflect.get(target, prop, receiver);
            },
            set(target, prop, value, receiver) {
                if (props.includes(prop)) {
                    acao(target);
                }
                return Reflect.set(target, prop, value, receiver);
            }
        });
    }

    static _isFunction(func) {
        return (typeof(func) == typeof(Function));
    }
}