import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Resolve, Router } from '@angular/router';
import { error } from 'protractor';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Apartment } from '../_models/apartment';
import { AlertifyService } from '../_services/alertify.service';
import { ApartmentService } from '../_services/apartment.service';

@Injectable()
export class ApartmentDetailResolver implements Resolve<Apartment> {
    constructor(private apartmentService: ApartmentService,
                private router: Router, private alertify: AlertifyService) {}

    resolve(route: ActivatedRouteSnapshot): Observable<Apartment> {
        return this.apartmentService.getApartment(route.params['id']).pipe(
            catchError(error => {
                this.alertify.error('Problem retrieving data');
                this.router.navigate(['/apartments']);
                return of(null);
            })
        );
    }
}