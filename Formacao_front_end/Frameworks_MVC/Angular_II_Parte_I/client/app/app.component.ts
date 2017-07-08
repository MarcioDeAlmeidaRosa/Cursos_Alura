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

    _http;
    //@Inject(Http) => orienta ao angular que antes de criar a instância de AppComponent
    // é preciso contruir o Http para ser injetado no contrutor
    // como essa classe tem o construtor complexo, o Angular prove um Provider que já
    // faz essa construção, para isso precisamos importar para o projeto
    // então no módulo principal da aplicação, importamos HttpModule para servir
    // as classes que precisam ser injetadas. 

    // constructor(@Inject(Http) http){
    
    //Temos outra forma para tipar uma variável com TS, usando :Type na frente da variável
    constructor(http: Http){
        // _http = http;
    }
    
}