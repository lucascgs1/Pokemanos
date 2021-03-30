// model
import { Login } from '../../../core/model/usuario';
import { Usuario } from '../../../core/model/usuario';

// service
import { AutenticacaoService } from '../../../core/services/util/autenticacao.service';
import { UsuarioService } from '../../../core/services/usuario.service';
import { TokenService } from '../../../core/services/util/token.service';

// package
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';


// service

//pacote

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  public login: Login = new Login();
  public isSubmited = false;
  public isEmailValid = false;
  public loginForm: FormGroup = this.formBuilder.group({
    email: ['', [Validators.required, Validators.pattern('^[a-zA-Z0-9._%-]+@[a-zA-Z0-9.-]+.[a-zA-Z]{2,4}$'),],],
    senha: ['', [Validators.required]],
    lembrar: [false],
  });

  constructor(
    public autenticacaoService: AutenticacaoService,
    private activatedRoute: ActivatedRoute,
    private formBuilder: FormBuilder,
    private router: Router,
    private tokenService: TokenService,
    private _snackBar: MatSnackBar
  ) { }

  ngOnInit(): void {

    if (this.tokenService.isActive())
      this.router.navigate(['']);
  }

  validateEmail(event: any): void {
    const re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    this.isEmailValid = !re.test(this.loginForm.controls['email'].value);
  }

  onSubmit(): void {
    this.isSubmited = true;

    if (this.loginForm.invalid) {
      return;
    }

    this.autenticacaoService.autenticar(this.loginForm.value)
      .subscribe(
        (result) => {
          this.autenticacaoService.setStorageUserToken(result.usuario, result.token);

          this.router.navigate(['']);
        }
      );
  }

  irCadastro() {
    this.router.navigate(['cadastro']);
  }
}

