import { Component } from '@angular/core';
@Component({
    // quando usamos moduleId: module.id, no templateUrl não 
    // precisa ser passado o caminho completo do template
    // o angular já busca o arqivo na mesma pasta do
    // componente
    moduleId: module.id,
    selector: 'foto',
    // templateUrl: './app/foto/foto.component.html'
    templateUrl: './foto.component.html'
})
export class FotoComponent{

}