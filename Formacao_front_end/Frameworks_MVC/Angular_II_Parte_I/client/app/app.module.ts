import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpModule } from '@angular/http'

import { AppComponent } from './app.component';
import { FotoModule } from './foto/foto.module';

//Importa a funcionalidade de map para o objeto Observable<Response>
import 'rxjs/add/operator/map';

@NgModule({
    //BrowserModule ->define que vai rodar no , import somente no módulo pricipal da app
    // importando HttpModule para atender a injeção de dependência para chamadas ajax
    imports: [ BrowserModule, FotoModule, HttpModule ],//declara os modulos que serão utilizados pela aplicação principal
    declarations: [ AppComponent ],
    bootstrap:    [ AppComponent ]
})
export class AppModule { }