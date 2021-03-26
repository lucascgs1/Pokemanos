//model
import { Usuario } from '../model/usuario';

//module
import { environment } from '../../../environments/environment';

//package
import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { retry, catchError, map } from 'rxjs/operators';
import { Observable } from 'rxjs/internal/Observable';
import { throwError } from 'rxjs/internal/observable/throwError';

@Injectable({
  providedIn: 'root'
})
export class UsuarioService {
  constructor(
    private httpClient: HttpClient,
  ) { }

  // Headers
  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  }

  getClienteById(id: number): Observable<Usuario> {
    var url = environment.endPoints.usuario;

    return this.httpClient.get<Usuario>(url + '/' + id)
      .pipe(
        retry(2),
        catchError(this.handleError))
  }

  getAllClientes(): Observable<Usuario[]> {
    var url = environment.endPoints.usuario;

    return this.httpClient.get<Usuario[]>(url)
      .pipe(
        retry(2),
        catchError(this.handleError))
  }


  postCliente(cliente: Usuario): Observable<any> {
    var url = environment.endPoints.usuario;

    return this.httpClient.post(url, cliente, this.httpOptions)
      .pipe(
        retry(2),
        catchError(this.handleError))
  }


  putCliente(usuario: Usuario): Observable<any> {
    var url = environment.endPoints.usuario;

    return this.httpClient.put(url, usuario, this.httpOptions)
      .pipe(
        retry(2),
        catchError(this.handleError))
  }

  deleteClienteById(id: number): Observable<Usuario> {
    var url = environment.endPoints.usuario;

    return this.httpClient.delete<Usuario>(url + '/' + id)
      .pipe(
        retry(2),
        catchError(this.handleError))
  }

  // Manipulação de erros
  handleError(error: HttpErrorResponse) {
    let errorMessage = '';
    if (error.error instanceof ErrorEvent) {
      // Erro ocorreu no lado do client
      errorMessage = error.error.message;
    } else {
      // Erro ocorreu no lado do servidor
      errorMessage = `Código do erro: ${error.status}, ` + `menssagem: ${error.message}`;
    }
    console.log(errorMessage);
    return throwError(errorMessage);
  };

}




