// model
import { Login, Usuario, UsuarioLogadoDTO } from '../../model/usuario';

// service
import { environment } from '../../../../environments/environment';
import { CacheService } from './cache.service';
import { HttpClientService } from './http-cliente.service';
import { TokenService } from './token.service';

// pacote
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { BehaviorSubject } from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class AutenticacaoService {
  public userId = 0;
  public user = new BehaviorSubject(new Usuario());

  constructor(
    private _http: HttpClientService,
    private cacheService: CacheService,
    private tokenService: TokenService,
  ) { }

  autenticar(login: Login): Observable<UsuarioLogadoDTO> {

    return this._http.postAnonimo<UsuarioLogadoDTO>({ url: environment.endPoints.conta + '/login', body: login, cacheMins: 120 })
  }

  cadastro(login: Login): Observable<UsuarioLogadoDTO> {

    return this._http.postAnonimo<UsuarioLogadoDTO>({ url: environment.endPoints.conta + '/cadastro', body: login, cacheMins: 120 })
  }

  logout() {
    this.cacheService.remove('currentUser');
    this.cacheService.cleanLocalStorage();
    this.user.next(new Usuario());
  }

  // storage
  setStorageUserToken(usuario: Usuario, token: string) {
    this.setStorageUser(usuario);
    this.setStorageToken(token);
  }

  setStorageToken(token: string) {
    this.tokenService.setToken(token);
  }

  setStorageUser(usuario: Usuario): void {
    this.cacheService.save({ data: usuario, key: 'currentUser', expirationMins: 0 });
    this.userId = usuario.id;
    this.user.next(usuario);
  }

  getStorageUser() {
    return this.user.value;
  }
}
