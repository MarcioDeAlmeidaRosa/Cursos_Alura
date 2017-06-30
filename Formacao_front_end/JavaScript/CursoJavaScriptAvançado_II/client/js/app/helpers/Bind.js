class Bind {
    constructor(model, view, ...props) {
        let proxy = ProxyFactory.create(model, props, () => {
            console.log('Recebendo ação');
            view.update(model);
        });

        view.update(model);

        return proxy;
    }
}