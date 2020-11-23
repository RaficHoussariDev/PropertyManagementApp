import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Buyer } from '../_models/buyer';
import { AlertifyService } from '../_services/alertify.service';
import { BuyerService } from '../_services/buyer.service';

@Injectable()
export class BuyResolver implements Resolve<Buyer[]> {

    constructor(private buyerService: BuyerService, private router: Router,
        private alertify: AlertifyService) {}

    resolve(route: ActivatedRouteSnapshot): Observable<Buyer[]> {
        return this.buyerService.getBuyers().pipe(
            catchError(error => {
                this.alertify.error('Problem retrieving buyers');
                this.router.navigate(['/home']);
                return of(null);
            })
        );
    }
}