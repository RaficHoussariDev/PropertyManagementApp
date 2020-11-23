import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Apartment } from '../_models/apartment';
import { PaginatedResult } from '../_models/pagination';

@Injectable({
  providedIn: 'root'
})
export class ApartmentService {
baseUrl = environment.apiUrl;

constructor(private http: HttpClient) { }
  
getApartments(page?: any, itemsPerPage?: any, apartmentParams?: any): Observable<PaginatedResult<Apartment[]>>{
    const paginatedResult: PaginatedResult<Apartment[]> = new PaginatedResult<Apartment[]>();
    let params = new HttpParams();

    if(page != null && itemsPerPage != null) {
      params = params.append('pageNumber', page);
      params = params.append('pageSize', itemsPerPage);
    }

     if(apartmentParams != null) {
      if(apartmentParams.nbOfRooms != null) {
        params = params.append('nbOfRooms', apartmentParams.nbOfRooms);
      }
      if(apartmentParams.minPrice != null) {
        params = params.append('minPrice', apartmentParams.minPrice);
      }
      if(apartmentParams.maxPrice != null) {
        params = params.append('maxPrice', apartmentParams.maxPrice);
      }
      if(apartmentParams.address != null) {
        params = params.append('address', apartmentParams.address);
      }
    } 
    
    return this.http.get<Apartment[]>(this.baseUrl + 'apartments', { observe: 'response', params })
      .pipe(
        map( response => {
          paginatedResult.result = response.body;
          if(response.headers.get('Pagination') != null) {
            paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
          }
          return paginatedResult;
        })
      );
  }

  getApartment(id: number): Observable<Apartment> {
    return this.http.get<Apartment>(this.baseUrl + 'apartments/' + id);
  }

  createApartment(model: any): Observable<Apartment> {
    return this.http.post<Apartment>(this.baseUrl + 'apartments/createApartment', model);
  }

  updateApartment(id: number, model: any): Observable<Apartment> {
    return this.http.put<Apartment>(this.baseUrl + 'apartments/' + id, model);
  }
}