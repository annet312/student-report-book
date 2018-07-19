import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { HttpModule, XHRBackend } from '@angular/http';
import { AuthenticateXHRBackend } from './authenticate-xhr.backend';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
//import { LoginFormComponent } from './account/login-form/login-form.component';
//import { SpinnerComponent } from './spinner/spinner.component';
import { routing } from './app.routing';
 
import { AccountModule } from './account/account.module';
//import { StudentModule } from '';
import { ConfigService } from './shared/utils/config.service';
import { DecodeService } from './shared/services/decode.service';
import { TeachersComponent } from './teachers/teachers.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    TeachersComponent,
    //LoginFormComponent,
    //SpinnerComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    AccountModule,
    HttpModule,
    FormsModule,
    routing
  ],
  providers: [ConfigService, {
    provide: XHRBackend,
    useClass: AuthenticateXHRBackend
  },
  DecodeService],
  bootstrap: [AppComponent]
})
export class AppModule { }
