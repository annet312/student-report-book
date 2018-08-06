import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { HttpModule, XHRBackend } from '@angular/http';
import { AuthenticateXHRBackend } from './authenticate-xhr.backend';

import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { TokenInterceptor } from './auth/token.interceptor';

//import { AgGridModule } from 'ag-grid-angular';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { DataTablesModule } from 'angular-datatables';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { routing } from './app.routing';
 
import { AccountModule } from './account/account.module';
import { StartWorkModule } from './start-work/start-work.module';
import { ConfigService } from './shared/utils/config.service';
import { DecodeService } from './shared/services/decode.service';
import { AuthService } from '../app/auth/auth.service';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,    
  ],
  imports: [
    NgbModule.forRoot(),
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    AccountModule,
    HttpModule,
    FormsModule,
    NgxDatatableModule,
    DataTablesModule,
    StartWorkModule,
    routing
  ],
  providers: [ConfigService,
    {
    provide: XHRBackend,
    useClass: AuthenticateXHRBackend
  },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptor,
      multi: true
    },
    DecodeService,
    AuthService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
