// service
import { UsuarioService } from '../../core/services/usuario.service';

//pacote
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  constructor(
    public usuarioService: UsuarioService
  ) { }

  ngOnInit(): void {
    this.usuarioService.getUsuarioById(1)
      .subscribe(
        result => {
          console.log(result);
        }
      )
  }

}
