// Arquivo necessário pelo loader de módulo para 
// definir qual o módulo principal da aplicação
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
import { AppModule } from './app.module';
const platform = platformBrowserDynamic();
//defini o módulo inicial que será carregado
platform.bootstrapModule(AppModule);