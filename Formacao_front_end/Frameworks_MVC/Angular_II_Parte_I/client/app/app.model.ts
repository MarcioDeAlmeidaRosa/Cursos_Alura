import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { appComponentName } from './path/fileName.component';

@NgModule({
    imports: [ BrowserModule ],//define que vai rodar no browser
    declarations: [ appComponentName ],
    bootstrap:    [ appComponentName ]
})
export class AppModule { }