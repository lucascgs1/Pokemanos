export class LocalStorageSaveOptions {
  constructor() {
    this.key = '';
  }

  key: string
  data: any
  expirationMins?: number
}

export class HttpOptions {
  constructor() {
    this.url = '';
    this.cacheMins = 15;
  }

  url: string
  body?: any
  cacheMins: number
}

export enum HttpVerbs {
  GET = 'GET',
  PUT = 'PUT',
  POST = 'POST',
  DELETE = 'DELETE'
}
