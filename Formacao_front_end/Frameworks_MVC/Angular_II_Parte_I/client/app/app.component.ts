//se usado constructor(@Inject(Http) http), precisamo importar o Inject
// import { Component, Inject } from '@angular/core';
import { Component } from '@angular/core';
import { Http } from '@angular/http';

@Component({
    // quando usamos moduleId: module.id, no templateUrl não 
    // precisa ser passado o caminho completo do template
    // o angular já busca o arqivo na mesma pasta do
    // componente
    moduleId: module.id,
    selector: 'app',
    // templateUrl: './app/app.component.html'
    templateUrl: './app.component.html'
})
export class AppComponent{

    fotos: Object[] = [];

    //@Inject(Http) => orienta ao angular que antes de criar a instância de AppComponent
    // é preciso contruir o Http para ser injetado no contrutor
    // como essa classe tem o construtor complexo, o Angular prove um Provider que já
    // faz essa construção, para isso precisamos importar para o projeto
    // então no módulo principal da aplicação, importamos HttpModule para servir
    // as classes que precisam ser injetadas. 

    // constructor(@Inject(Http) http){
    
    //Temos outra forma para tipar uma variável com TS, usando :Type na frente da variável
    constructor(http: Http){
        //O metodo get do Angular 2 devolve um Observable<Response> 
        // onde você tem que se inscrever para ser notificado quando
        // a resposta estiver pronta
        //let stream = http.get('v1/fotos');
        //Metodo de inscrição que recebe um callback, este só cherá chamado
        // quando o servidor responder a requisição get
        // stream.subscribe(res => {
        //     // A resposta é um objeto do tipo Response
        //     // nele precisamos acessar alguns métodos/propriedades
        //     // para verificar o que foi respondido pelo servidor
        //     // this.fotos = res.text();
        //     this.fotos = res.json();
        // });

        // Podemos reduzir o código desta forma
        http
        .get('v1/fotos')
        //funcionalidade map liberada depois do --> import 'rxjs/add/operator/map'; ---> feito em app.module.ts
        .map( res=> res.json() )
        .subscribe(foto => {
            //Com a utilização de map, o metodo subscribe já receberá um objeto foto
            this.fotos = foto;
        }, erro => {
            console.log(erro);
        });
    }
    
}