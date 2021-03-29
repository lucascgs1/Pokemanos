// model
import { HttpOptions, HttpVerbs } from '../../model/cache';

// service
import { CacheService } from './cache.service';

// pacote
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs'
import { switchMap } from 'rxjs/operators'
import { TokenService } from './token.service';

@Injectable({
  providedIn: 'root'
})

export class HttpClientService {
  public headers = new HttpHeaders().set("Content-Type", "application/json");
  

  constructor(
    private http: HttpClient,
    private _cacheService: CacheService,
    private tokenService: TokenService,
  ) { }

  getAnonimo<T>(options: HttpOptions): Observable<T> {
    return this.httpCall(HttpVerbs.GET, options)
  }

  postAnonimo<T>(options: HttpOptions): Observable<T> {
    return this.httpCall(HttpVerbs.POST, options)
  }

  get<T>(options: HttpOptions): Observable<T> {
    return this.httpCall(HttpVerbs.GET, options, false);
  }

  post<T>(options: HttpOptions): Observable<T> {
    return this.httpCall(HttpVerbs.POST, options, false);
  }

  put<T>(options: HttpOptions): Observable<T> {
    return this.httpCall(HttpVerbs.PUT, options, false);
  }

  delete<T>(options: HttpOptions): Observable<T> {
    return this.httpCall(HttpVerbs.DELETE, options, false);
  }

  private httpCall<T>(verb: HttpVerbs, options: HttpOptions, anonimo: boolean = true): Observable<T> {

    // Setup default values
    options.body = options.body || null
    options.cacheMins = options.cacheMins || 0

    if (options.cacheMins > 0) {
      // Get data from cache
      const data = this._cacheService.load(options.url)
      // Return data from cache
      if (data !== null) {
        return of<T>(data)
      }
    }

    if (!anonimo && this.tokenService.isActive()) {
      this.headers = this.headers.append(
        "Authorization",
        "Bearer " + this.tokenService.getToken()
      );
    }

    return this.http.request<T>(verb, options.url, {
      body: options.body,
      headers: this.headers
    })
      .pipe(
        switchMap(response => {
          if (options.cacheMins > 0) {
            // Data will be cached
            this._cacheService.save({
              key: options.url,
              data: response,
              expirationMins: options.cacheMins
            })
          }
          return of<T>(response)
        })
      )
  }
}
