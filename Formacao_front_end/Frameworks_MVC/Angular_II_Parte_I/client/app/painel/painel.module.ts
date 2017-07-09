import {NgModule} from '@angular/core';

import {PainelComponent} from './painel.component';

@NgModule({
    imports:[],
    declarations:[PainelComponent],
    //exportando o painal para permitir ser utilizado fora deste módulo
    exports:[PainelComponent]
})
export class PainelModel{

}