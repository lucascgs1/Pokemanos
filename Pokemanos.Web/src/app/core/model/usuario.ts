export class Usuario {
  constructor() {
    this.id = 0;
    this.nome = '';
    this.sobrenome = '';
    this.email = '';
    this.senha = '';
    this.telefone = '';
    this.dataCadastro = new Date();
  }

  id: number;
  nome: string;
  sobrenome: string;
  email: string;
  senha: string;
  telefone: string;
  dataCadastro: Date;
}

export class Login {
  constructor() {
    this.email = '';
    this.senha = '';
    this.lembrar = false;
  }

  email: string;
  senha: string;
  lembrar: boolean;
}


export class UsuarioLogadoDTO {
  constructor() {
    this.usuario = new Usuario();
    this.token = '';
  }

  usuario: Usuario;
  token: string;
}

export class RecuperarSenhaDTO {
  constructor() {
    this.email = '';
    this.senhaNova = '';
    this.codigoSeguranca = '';
  }

  email: string;
  senhaNova: string;
  codigoSeguranca: string;
}
