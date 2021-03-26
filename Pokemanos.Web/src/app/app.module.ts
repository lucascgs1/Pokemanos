//page
import { AppComponent } from './app.component';
import { HomeComponent } from './pages/home/home.component';

//module
import { AppRoutingModule } from './app-routing.module';
import { SharedModule } from './shared/shared.module';
import { CoreModule } from './core/core.module';

//package
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { IConfig, NgxMaskModule } from 'ngx-mask';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    SharedModule,
    CoreModule,
    NgxMaskModule.forRoot({ dropSpecialCharacters: false }),
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
