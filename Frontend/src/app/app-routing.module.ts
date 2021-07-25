import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
//! Component
import { TodosComponent } from './componets/Todo/todos/todos.component';
import { AboutComponent } from './componets/about/about.component';
import { NotfoundComponent } from './componets/layout/notfound/notfound.component';
const routes: Routes = [
  { path: '', component: TodosComponent },
  { path: 'about', component: AboutComponent },
  { path: '**', component: NotfoundComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
