class Impressao{
    constructor(elemento){
        this._elemento = elemento;
    }

    update(data){
        this._elemento.innerHTML = this._template(data);
    }

    _template(data){
        return `
            <table>
                <thead>
                    <tr>
                        <th>Nome</th>
                        <th>Endereço</th>
                        <th>Salário</th>
                    </tr>
                </thead>

                <tbody>

                    ${
                        data.map(d => `
                          <tr>
                            <td>${d.nome}</td>
                            <td>${d.endereco}</td>
                            <td>${d.salario}</td>
                          </tr>
                        `).join('')
                    }
                <tbody>
            </table>
        `;
    }
}