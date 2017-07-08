import { Component } from '@angular/core';

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
    
}