import {
  Component,
  OnInit,
  ViewChild,
  EventEmitter, // this For Event
  Output, // This for  Macking the value Availbel in Other Comp
  Input, // This is For getting  A value from Above (Parent Comp )
} from '@angular/core';

import { TdoItem } from 'src/app/models/TodItem';
import { NgForm } from '@angular/Forms';
import { DataService } from '../../services/data.service';
@Component({
  selector: 'app-from',
  templateUrl: './from.component.html',
  styleUrls: ['./from.component.css'],
})
//? This Comp Use template Deriven Form
//? To Catch The Submit Event In The Parent Comp you Must Add Event Emmeter
export class FromComponent implements OnInit {
  //! I used Template Dervien From
  //? This For 2 Way Binding
  // task: TdoItem = {
  //   id: undefined,
  //   name: '',
  //   status: false,
  // };
  //? this for Event And Emmting The Value Above For A Parent Comp
  @Output() newTask: EventEmitter<TdoItem> = new EventEmitter();
  //? This is for Catching A Value From Above
  @Input() isEdit: boolean = false;
  //? This is For Catching A value From Above
  @Input() CurrentItem: TdoItem = {
    id: 0,
    name: '',
    status: false,
  };

  //? This For Template Deriven Form Validation
  @ViewChild('taskForm') form: any;

  constructor(private _dataservice: DataService) {}

  ngOnInit(): void {}

  //! This Inculeds The Client Side Validation

  OnSubmit({ value, valid }: NgForm) {
    if (valid) {
      //? Atatch every value To It Pair ex : Not necessary
      this.CurrentItem.name = value.name;
      // this.CurrentItem.status = false;
      this.CurrentItem.id = 0;
      //! This To Call The Service For Adding Task
      this._dataservice.saveTask(this.CurrentItem).subscribe((Item) => {
        this.newTask.emit(Item); // sending the event Up
      });

      //? This To Set The values To Its Normal state
      this.CurrentItem = {
        id: 0,
        name: '',
        status: false,
      };
      //? This To Clear The Form
      this.form.reset();
    }
  }

  update({ value, valid }: NgForm): void {
    this._dataservice.UpdatingTask(this.CurrentItem).subscribe((item) => item);
    //? This To Set The values To Its Normal state
    this.CurrentItem = {
      id: 0,
      name: '',
      status: false,
    };
    this.isEdit = false;
    //? This To Clear The Form
    this.form.reset();
  }
}
