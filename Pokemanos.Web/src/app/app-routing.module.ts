// page
import { HomeComponent } from './pages/home/home.component';
import { LoginComponent } from './pages/Account/login/login.component';
import { CadastroComponent } from './pages/Account/cadastro/cadastro.component';

// package
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    component: HomeComponent
  },
  {
    path: 'login',
    component: LoginComponent
  },
  {
    path: 'cadastro',
    component: CadastroComponent
  },
  {
    path: 'editar',
    component: CadastroComponent
  },


];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
