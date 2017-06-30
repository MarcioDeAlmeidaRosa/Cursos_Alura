class ProxyFactory {
    static create(objeto, props, acao) {
        return new Proxy(objeto, {
            get(target, prop, receiver) {
                // get: function(target, prop, receiver) {
                if ((props.includes(prop)) && (typeof(target[prop]) == typeof(Function))) {
                    return function() {
                        console.log(`interceptado o metodo ${prop}`);
                        Reflect.apply(target[prop], target, arguments);
                        //self._negociacoesView.update(target);
                        return acao(target);
                    }
                }
                console.log(`interceptado a propriedade ${prop}`);
                return Reflect.get(target, prop, receiver);
            }
        });
    }
}