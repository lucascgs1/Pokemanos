// model
import { Usuario } from '../model/usuario';

// module
import { environment } from '../../../environments/environment';
import { HttpClientService } from './util/http-cliente.service';

// package
import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { retry, catchError, map } from 'rxjs/operators';
import { Observable } from 'rxjs/internal/Observable';
import { throwError } from 'rxjs/internal/observable/throwError';
import { useAnimation } from '@angular/animations';

@Injectable({
  providedIn: 'root'
})
export class UsuarioService {
  constructor(
    private httpClient: HttpClient,
    private _http: HttpClientService
  ) { }

  getUsuarioById(id: number): Observable<Usuario> {

    return this._http.get<Usuario>({ url: environment.endPoints.usuario + '/' + id, cacheMins: 10 })
  }

  getAllUsuario(): Observable<Usuario[]> {

    return this.httpClient.get<Usuario[]>(environment.endPoints.usuario)
      .pipe(
        retry(2),
        catchError(this.handleError));
  }


  postUsuario(usuario: Usuario): Observable<any> {

    let url = environment.endPoints.usuario;

    if (usuario.id == 0) {
      url += '/cadastro'
    }
    return this._http.get<any>({ url: 'https://example-api/products', cacheMins: 5 })
  }


  putCliente(usuario: Usuario): Observable<any> {

    return this.httpClient.put(environment.endPoints.usuario, usuario)
      .pipe(
        retry(2),
        catchError(this.handleError));
  }

  deleteClienteById(id: number): Observable<Usuario> {

    return this.httpClient.delete<Usuario>(environment.endPoints.usuario + '/' + id)
      .pipe(
        retry(2),
        catchError(this.handleError));
  }

  // Manipulação de erros
  handleError(error: HttpErrorResponse): Observable<any> {
    let errorMessage = '';
    if (error.error instanceof ErrorEvent) {
      // Erro ocorreu no lado do client
      errorMessage = error.error.message;
    } else {
      // Erro ocorreu no lado do servidor
      errorMessage = `Código do erro: ${error.status}, ` + `menssagem: ${error.message}`;
    }
    console.error(errorMessage);
    return throwError(errorMessage);
  }

}




