//model
import { Usuario } from '../../core/model/usuario';

//service
import { UsuarioService } from '../../core/services/usuario.service';

// package
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormBuilder, Validators, FormControl, } from "@angular/forms";

@Component({
  selector: 'app-cadastro',
  templateUrl: './cadastro.component.html',
  styleUrls: ['./cadastro.component.scss']
})

export class CadastroComponent implements OnInit {
  public usarioId: number = 0;
  public usuario: Usuario = new Usuario();
  public isSubmited: boolean = false;
  public isEmailValid: boolean = false;

  public clienteForm: FormGroup = this.formBuilder.group({
    id: [0],
    nome: ["", [Validators.required]],
    sobrenome: ["", [Validators.required]],
    email: ["", [Validators.required, Validators.pattern("^[a-zA-Z0-9._%-]+@[a-zA-Z0-9.-]+.[a-zA-Z]{2,4}$"),],],
    senha: ["", [Validators.required]],
    confirmarSenha: ["", [Validators.required]],
    telefone: ["", [Validators.required]],
    dataCadastro: [new Date],
  });

  constructor(
    private activatedRoute: ActivatedRoute,
    private usuarioService: UsuarioService,
    private formBuilder: FormBuilder,
    private router: Router,
  ) { }

  ngOnInit(): void {
  }

}
