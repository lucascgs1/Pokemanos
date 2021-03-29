// service
import { AutenticacaoService } from './services/autenticacao.service';
import { CacheService } from './services/cache.service';
import { HttpClientService } from './services/http-cliente.service';
import { UsuarioService } from './services/usuario.service';

// package
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    HttpClientModule
  ],
  exports: [
    HttpClientModule
  ],
  providers: [
    AutenticacaoService,
    HttpClientService,
    CacheService,
    UsuarioService,
  ]
})

export class CoreModule { }
