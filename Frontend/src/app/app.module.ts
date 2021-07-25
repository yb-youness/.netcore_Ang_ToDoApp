//! Models
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/Forms'; //? Must Add This For NgModel ToWork
import { HttpClientModule } from '@angular/common/http';
//! Components :
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { TodosComponent } from './componets/Todo/todos/todos.component';
import { TodoItemComponent } from './componets/Todo/todo-item/todo-item.component';
import { FromComponent } from './componets/from/from.component';
import { NavbarComponent } from './componets/layout/navbar/navbar.component';
import { AboutComponent } from './componets/about/about.component';
import { NotfoundComponent } from './componets/layout/notfound/notfound.component';

@NgModule({
  declarations: [
    AppComponent,
    TodosComponent,
    TodoItemComponent,
    FromComponent,
    NavbarComponent,
    AboutComponent,
    NotfoundComponent,
  ],
  imports: [BrowserModule, AppRoutingModule, FormsModule, HttpClientModule],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
