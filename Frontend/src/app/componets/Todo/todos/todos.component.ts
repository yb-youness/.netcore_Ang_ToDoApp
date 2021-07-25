import { Component, OnInit } from '@angular/core';
import { TdoItem } from 'src/app/models/TodItem';
//? This For The Service Import
import { DataService } from '../../../services/data.service';
@Component({
  selector: 'app-todos',
  templateUrl: './todos.component.html',
  styleUrls: ['./todos.component.css'],
})
export class TodosComponent implements OnInit {
  TodItems: TdoItem[] = [];
  Loading: boolean = false;
  //! This For The Edit Btn  CurrentItem Must Be The Same On The Html Side
  CurrentItem: TdoItem = {
    id: 0,
    name: '',
    status: false,
  };
  //! Edit State
  isEdit: boolean = false;

  constructor(private _dataservice: DataService) {}

  ngOnInit(): void {
    setTimeout(() => {
      this._dataservice
        .getTasks()
        .subscribe((tasks) => (this.TodItems = tasks));
      this.Loading = true;
    }, 2000);
  }
  //! This Fonction Is To Add To The List The Request Is Done In The FromComp ...
  onNewtask(task: TdoItem) {
    //! This Fonction Is From The FormComp And It Submited Here  see the Html
    //! <app-from (newTask)="onNewtask($event)"></app-from> ===> This Is Catching The Value Here
    // ! () == Output comming from A comp

    this.TodItems.push(task);
  }

  //! This Function Is To Edit the Item the Request Is Done In the FromComp
  editTask(item: TdoItem): void {
    //! Here We Must Pass Down  The Item To The FromComp
    //! <app-from (newTask)="onNewtask($event)"></app-from>
    //! [] == inptput from this To Other Comp
    this.CurrentItem = item;
    this.isEdit = true; //! This For The Edit Btn
    //! Its Passed To The From
    //! For The State Passed As Input
  }

  delTask(item: TdoItem) {
    if (confirm('Are You Sure ?')) {
      this._dataservice.deltTask(item).subscribe(() => {
        this.TodItems = this.TodItems.filter((t) => t.id !== item.id);
        console.log(this.TodItems);
      });
    }
  }
}
