import { Component, Input, OnInit } from '@angular/core';
import { TdoItem } from 'src/app/models/TodItem';

@Component({
  selector: 'app-todo-item',
  templateUrl: './todo-item.component.html',
  styleUrls: ['./todo-item.component.css'],
})
export class TodoItemComponent implements OnInit {
  constructor() {}

  ngOnInit(): void {}
}
