import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Buyer } from '../_models/buyer';

@Injectable({
  providedIn: 'root'
})
export class BuyerService {
baseUrl = environment.apiUrl;

constructor(private http: HttpClient) { }

getBuyers(): Observable<Buyer[]> {
  return this.http.get<Buyer[]>(this.baseUrl + 'buyers');
}

buy(model: any) {
  return this.http.post<string>(this.baseUrl + 'buyers/buy', model);
}

}
