// service
import { AutenticacaoService } from './services/util/autenticacao.service';
import { CacheService } from './services/util/cache.service';
import { HttpClientService } from './services/util/http-cliente.service';
import { TokenService } from './services/util/token.service';
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
    HttpClientService,
    AutenticacaoService,
    CacheService,
    TokenService,
    UsuarioService,
  ]
})

export class CoreModule { }
