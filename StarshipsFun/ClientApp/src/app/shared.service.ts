import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IStarship } from '../interfaces/starship';

@Injectable({
  providedIn: 'root'
})
export class SharedService {

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  getStarships(): Observable<IStarship[]> {
    return this.http.get<IStarship[]>(this.baseUrl + 'starships');
  }

  getAllStarships(): Observable<IStarship[]> {
    return this.http.get<IStarship[]>(this.baseUrl + 'starships/all');
  }
}
