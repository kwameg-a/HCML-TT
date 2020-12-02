import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { IStarship } from '../interfaces/starship';

@Injectable({
  providedIn: 'root'
})
export class SharedService {

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  getStarships() {
    return this.http.get<IStarship[]>(this.baseUrl + 'starships');
  }

  getShuffledStarships() {
    return this.http.get<IStarship[]>(this.baseUrl + 'starships/load');
  }
}
