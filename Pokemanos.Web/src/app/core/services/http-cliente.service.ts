// model
import { HttpOptions, HttpVerbs } from '../model/cache';

// service
import { CacheService } from './cache.service';

// pacote
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs'
import { switchMap } from 'rxjs/operators'

@Injectable({
  providedIn: 'root'
})

export class HttpClientService {
  constructor(
    private http: HttpClient,
    private _cacheService: CacheService,
  ) { }

  get<T>(options: HttpOptions): Observable<T> {
    return this.httpCall(HttpVerbs.GET, options)
  }

  delete<T>(options: HttpOptions): Observable<T> {
    return this.httpCall(HttpVerbs.DELETE, options)
  }

  post<T>(options: HttpOptions): Observable<T> {
    return this.httpCall(HttpVerbs.POST, options)
  }

  put<T>(options: HttpOptions): Observable<T> {
    return this.httpCall(HttpVerbs.PUT, options)
  }

  private httpCall<T>(verb: HttpVerbs, options: HttpOptions): Observable<T> {

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

    return this.http.request<T>(verb, options.url, {
      body: options.body
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
