import { Routes } from '@angular/router';
import { ApartmentDetailComponent } from './apartment-detail/apartment-detail.component';
import { ApartmentListComponent } from './apartment-list/apartment-list.component';
import { BuyComponent } from './buy/buy.component';
import { HomeComponent } from './home/home.component';
import { PreventUnsavedChanges } from './_guards/prevent-unsaved-changes.guard';
import { ApartmentDetailResolver } from './_resolvers/apartment-detail.resolver';
import { ApartmentListResolver } from './_resolvers/apartment-list.resolver';
import { BuyResolver } from './_resolvers/buy.resolver';

export const appRoutes: Routes = [
    { path: '', component: HomeComponent },
    {
        path: '',
        runGuardsAndResolvers: 'always',
        children: [
            { path: "apartments", component: ApartmentListComponent, resolve: { 'apartments': ApartmentListResolver} },
            { path: "buy/:id", component: BuyComponent, resolve: { 'buyers': BuyResolver } },
            { path: "apartment/:id", component: ApartmentDetailComponent, 
                resolve: { 'apartment': ApartmentDetailResolver}, canDeactivate: [PreventUnsavedChanges] }
        ]
    },
    { path: '**', redirectTo: '', pathMatch: 'full' }
];