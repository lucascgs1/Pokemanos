//service

// pacote
import { Component, OnInit } from '@angular/core';
import { ThemePalette } from '@angular/material/core';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AutenticacaoService } from '../../../core/services/util/autenticacao.service';

@Component({
  selector: 'app-recuperar-senha',
  templateUrl: './recuperar-senha.component.html',
  styleUrls: ['./recuperar-senha.component.scss']
})
export class RecuperarSenhaComponent implements OnInit {
  public color: ThemePalette = 'primary';
  public isSubmited = false;
  public isEmailValid = false;
  public hasToken = false;

  public senhaForm: FormGroup = this.formBuilder.group({
    email: ['', [Validators.required, Validators.pattern('^[a-zA-Z0-9._%-]+@[a-zA-Z0-9.-]+.[a-zA-Z]{2,4}$'),],],
  });

  constructor(
    private activatedRoute: ActivatedRoute,
    private formBuilder: FormBuilder,
    private router: Router,
    private autenticacaoService: AutenticacaoService,

  ) { }

  ngOnInit(): void {
  }


  validateEmail(event: any): void {
    const re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    this.isEmailValid = !re.test(this.senhaForm.controls['email'].value);
  }

  atualizarFormulario(event: any): void {
    let email = this.senhaForm.value.email;

    this.hasToken = event.checked;

    if (this.hasToken) {
      this.senhaForm = this.formBuilder.group({
        email: [email, [Validators.required, Validators.pattern('^[a-zA-Z0-9._%-]+@[a-zA-Z0-9.-]+.[a-zA-Z]{2,4}$'),],],
        codigoSeguranca: ['', [Validators.required]],
        senha: ['', [Validators.required],],
        confirmarSenha: ['', [Validators.required],],
      });
    } else {
      this.senhaForm = this.formBuilder.group({
        email: [email, [Validators.required, Validators.pattern('^[a-zA-Z0-9._%-]+@[a-zA-Z0-9.-]+.[a-zA-Z]{2,4}$'),],],
      });
    }
  }


  onSubmit(): void {
    this.isSubmited = true;


    if (this.senhaForm.invalid) {
      return;
    }

    console.log(this.senhaForm);

    //if (this.usuario != null && this.usuario.id > 0) {

    //  this.usuarioService.postUsuario(this.senhaForm.value)
    //    .subscribe(
    //      result => {
    //        this.router.navigate(['']);
    //      }
    //    );

    //} else {
    //  this.autenticacaoService.cadastro(this.usuarioForm.value)
    //    .subscribe(
    //      result => {
    //        this.autenticacaoService.setStorageUserToken(result.usuario, result.token);

    //        this.router.navigate(['']);
    //      }
    //    )
    //}

  }
}
