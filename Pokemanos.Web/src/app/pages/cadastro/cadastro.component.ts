// model
import { Usuario } from '../../core/model/usuario';

// service
import { UsuarioService } from '../../core/services/usuario.service';

// package
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';

@Component({
  selector: 'app-cadastro',
  templateUrl: './cadastro.component.html',
  styleUrls: ['./cadastro.component.scss']
})

export class CadastroComponent implements OnInit {
  public usarioId = 0;
  public usuario: Usuario = new Usuario();
  public isSubmited = false;
  public isEmailValid = false;

  public usuarioForm: FormGroup = this.formBuilder.group({
    id: [0],
    nome: ['', [Validators.required]],
    sobrenome: ['', [Validators.required]],
    email: ['', [Validators.required, Validators.pattern('^[a-zA-Z0-9._%-]+@[a-zA-Z0-9.-]+.[a-zA-Z]{2,4}$'),],],
    telefone: ['', [Validators.required]]
  });

  constructor(
    private activatedRoute: ActivatedRoute,
    private usuarioService: UsuarioService,
    private formBuilder: FormBuilder,
    private router: Router,
  ) { }


  ngOnInit(): void {

    console.log(this.activatedRoute.params);
    this.activatedRoute.params.subscribe(
      (params) => {
        console.log(params['id']);
        this.usarioId = params['id'] != null && params['id'] > 0 ? params['id'] : 0;
        if (this.usarioId > 0) {
          this.iniciarFormulario();
        }
      });
  }

  iniciarFormulario() {
    if (this.usarioId != undefined && this.usarioId > 0) {
      this.usuarioService.getUsuarioById(this.usarioId)
        .subscribe(
          result => {
            this.usuario = result;

            console.log(this.usuario)
            this.usuarioForm = this.formBuilder.group({
              id: [this.usuario.id],
              nome: [this.usuario.nome, [Validators.required]],
              sobrenome: [this.usuario.sobrenome, [Validators.required]],
              email: [this.usuario.email, [Validators.required, Validators.pattern('^[a-zA-Z0-9._%-]+@[a-zA-Z0-9.-]+.[a-zA-Z]{2,4}$'),],],
              telefone: [this.usuario.telefone, [Validators.required]],
            });

            this.validateEmail(null);
          }
        );
    }
  }

  validateEmail(event: any): void {
    const re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    this.isEmailValid = !re.test(this.usuarioForm.controls['email'].value);
    console.log(this.isEmailValid);
  }


  onSubmit(): void {
    console.log(this.usuarioForm);
    console.log('teste');

    this.isSubmited = true;

    if (this.usuarioForm.invalid) {
      return;
    }
    this.usuarioService.postUsuario(this.usuarioForm.value)
      .subscribe(
        result => {
          this.router.navigate(['']);
        }
      );
  }

}
