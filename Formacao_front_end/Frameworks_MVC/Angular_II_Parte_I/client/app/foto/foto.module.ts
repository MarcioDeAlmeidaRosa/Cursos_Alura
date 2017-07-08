import {NgModule} from '@angular/core';

import {FotoComponent} from './foto.component';
@NgModule({
    declarations:[FotoComponent],//declara tudo o que faz parte do módulo
    exports: [FotoComponent]//declara tudo que o modulo irá exportar, o que será visto por outros modulos
})
export class FotoModule{

}