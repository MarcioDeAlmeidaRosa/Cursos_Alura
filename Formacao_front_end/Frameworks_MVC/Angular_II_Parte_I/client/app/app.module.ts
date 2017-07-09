import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpModule } from '@angular/http'

//Importa a funcionalidade de map para o objeto Observable<Response>
import 'rxjs/add/operator/map';

import { routing } from './app.route';

import { AppComponent } from './app.component';
import { FotoModule } from './foto/foto.module';
import { PainelModel } from './painel/painel.module';
import {CadastroComponent} from './cadastro/cadastro.component';
import {ListagemComponent} from './listagem/listagem.component';


@NgModule({
    //BrowserModule ->define que vai rodar no , import somente no módulo pricipal da app
    // importando HttpModule para atender a injeção de dependência para chamadas ajax
    imports: [ 
        BrowserModule, 
        FotoModule, 
        HttpModule, 
        PainelModel, 
        routing 
    ],//declara os modulos que serão utilizados pela aplicação principal
    declarations: [ AppComponent, CadastroComponent, ListagemComponent ],
    bootstrap:    [ AppComponent ]
})
export class AppModule { }