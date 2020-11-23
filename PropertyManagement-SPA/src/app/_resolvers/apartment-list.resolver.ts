import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Apartment } from '../_models/apartment';
import { AlertifyService } from '../_services/alertify.service';
import { ApartmentService } from '../_services/apartment.service';

@Injectable()
export class ApartmentListResolver implements Resolve<Apartment[]> {
    pageNumber = 1;
    pageSize = 5;

    constructor(private apartmentService: ApartmentService, private router: Router,
        private alertify: AlertifyService) {}

    resolve(route: ActivatedRouteSnapshot): Observable<Apartment[]> {
        return this.apartmentService.getApartments(this.pageNumber, this.pageSize).pipe(
            catchError(error => {
                this.alertify.error('Problem retrieving data');
                this.router.navigate(['/home']);
                return of(null);
            })
        );
    }
}