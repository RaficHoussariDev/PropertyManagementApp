import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { FormsModule } from '@angular/forms';
import { TabsModule } from 'ngx-bootstrap/tabs';


import { AppComponent } from './app.component';
import { ApartmentListComponent } from './apartment-list/apartment-list.component';
import { ApartmentService } from './_services/apartment.service';
import { HomeComponent } from './home/home.component';
import { appRoutes } from './routes';
import { ApartmentListResolver } from './_resolvers/apartment-list.resolver';
import { BuyComponent } from './buy/buy.component';
import { BuyerService } from './_services/buyer.service';
import { BuyResolver } from './_resolvers/buy.resolver';
import { ErrorInterceptorProvider } from './_services/error.interceptor';
import { AlertifyService } from './_services/alertify.service';
import { ApartmentDetailComponent } from './apartment-detail/apartment-detail.component';
import { ApartmentDetailResolver } from './_resolvers/apartment-detail.resolver';
import { PreventUnsavedChanges } from './_guards/prevent-unsaved-changes.guard';




@NgModule({
  declarations: [					
    AppComponent,
    ApartmentListComponent,
      HomeComponent,
      BuyComponent,
      ApartmentDetailComponent
   ],
  imports: [
    BrowserModule,
    HttpClientModule,
    RouterModule.forRoot(appRoutes),
    PaginationModule.forRoot(),
    FormsModule,
    TabsModule.forRoot(),
  ],
  providers: [
    ApartmentService,
    ApartmentListResolver,
    BuyerService,
    BuyResolver,
    ErrorInterceptorProvider,
    AlertifyService,
    ApartmentDetailResolver,
    PreventUnsavedChanges
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
