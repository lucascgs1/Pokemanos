// model
import { Usuario } from '../../core/model/usuario';

// service
import { UsuarioService } from '../../core/services/usuario.service';
import { TokenService } from '../../core/services/util/token.service';
import { AutenticacaoService } from '../../core/services/util/autenticacao.service';

// pacote
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  public usuario = new Usuario();

  constructor(
    public usuarioService: UsuarioService,
    public tokenService: TokenService,
    private router: Router,
    private autenticacaoService: AutenticacaoService,
  ) { }

  ngOnInit(): void {
    if (!this.tokenService.isActive())
      this.router.navigate(['login']);

    this.usuario = this.autenticacaoService.getStorageUser() || new Usuario();
  }

}
