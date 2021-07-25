import { Injectable } from '@angular/core';
//! Http Model
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
//! Model
import { TdoItem } from '../models/TodItem';
//! HttpOption
const httpOpt = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
};

@Injectable({
  providedIn: 'root',
})
export class DataService {
  //! Url For The Backend
  TodUrl: string = 'http://localhost:5000/api/Tasks';

  constructor(private _http: HttpClient) {}
  //! To get All TodoS
  getTasks(): Observable<TdoItem[]> {
    return this._http.get<TdoItem[]>(this.TodUrl);
  }
  //! This For Adding Task  = > POST request
  saveTask(task: TdoItem): Observable<TdoItem> {
    return this._http.post<TdoItem>(this.TodUrl + '/AddTask  ', task, httpOpt);
  }

  //! This For Deleting A Task = > Delete request
  deltTask(item: TdoItem): Observable<TdoItem> {
    console.log(`${this.TodUrl}/${item.id}`);

    return this._http.delete<TdoItem>(`${this.TodUrl}/${item.id}`, httpOpt);
  }

  //! This For Updating A Task = > Put request
  UpdatingTask(item: TdoItem): Observable<TdoItem> {
    return this._http.put<TdoItem>(`${this.TodUrl}/${item.id}`, item, httpOpt);
  }
}
