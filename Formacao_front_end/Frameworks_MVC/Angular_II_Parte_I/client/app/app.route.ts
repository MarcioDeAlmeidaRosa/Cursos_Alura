import {RouterModule, Routes} from '@angular/router';
import {CadastroComponent} from './cadastro/cadastro.component';
import {ListagemComponent} from './listagem/listagem.component';

const appRoutes: Routes = [
    { path: '', component: ListagemComponent },
    { path: 'listagem', component: ListagemComponent },
    { path: 'cadastro', component: CadastroComponent },
    // quando usamos ** indicamos que qualquer rota digitada 
    // que não foi declarada aqui, será encaminhado para uma 
    // determinada rota, no caso a listagem de fotos
    { path: '**', component: ListagemComponent }
];

export const routing = RouterModule.forRoot(appRoutes)