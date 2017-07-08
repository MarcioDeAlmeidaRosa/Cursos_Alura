import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { FotoModule } from './foto/foto.module';

@NgModule({
    //BrowserModule ->define que vai rodar no , import somente no módulo pricipal da app
    imports: [ BrowserModule, FotoModule ],//declara os modulos que serão utilizados pela aplicação principal
    declarations: [ AppComponent ],
    bootstrap:    [ AppComponent ]
})
export class AppModule { }