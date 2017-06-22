class DominandoMapp{

    constructor(){
        console.log('constructor');
        let numeros = [1, 4, 9, 16, 25, 36, 49, 64, 81, 100, 121];

        console.log('Números = ' + numeros);

        //Qual das funções abaixo aproveita-se dos recursos da função 
        //map para obter arrays com os valores dobrados, com valores 
        //pela metade e com raiz quadrada de todos os números abaixo, 
        //mantendo a ordem apresentada:

        console.log('Dobro = ' + numeros.map(function(num) {
            return num * 2;
        }));

        console.log('Metade = ' + numeros.map(function(num) {
            return num/2;
        }));

        console.log('Raiz Quadrada = ' + numeros.map(function(num) {
            return Math.sqrt(num);
        }));



    }

}