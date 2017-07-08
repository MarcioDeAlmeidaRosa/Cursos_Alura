import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';

@NgModule({
    imports: [ BrowserModule ],//define que vai rodar no browser
    declarations: [ AppComponent ],
    bootstrap:    [ AppComponent ]
})
export class AppModule { }