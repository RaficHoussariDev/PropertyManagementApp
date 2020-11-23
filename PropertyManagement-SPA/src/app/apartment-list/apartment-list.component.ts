import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Apartment } from '../_models/apartment';
import { PaginatedResult, Pagination } from '../_models/pagination';
import { AlertifyService } from '../_services/alertify.service';
import { ApartmentService } from '../_services/apartment.service';

@Component({
  selector: 'app-apartment-list',
  templateUrl: './apartment-list.component.html',
  styleUrls: ['./apartment-list.component.css']
})
export class ApartmentListComponent implements OnInit {
  apartments: Apartment[];
  nbOfRoomsList:any[] = [];
  apartmentParams: any = {};
  pagination: Pagination;
  createMode = false;

  constructor(private apartmentService: ApartmentService, private route: ActivatedRoute,
    private alertify: AlertifyService)  { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.apartments = data['apartments'].result;
      this.pagination = data['apartments'].pagination;
    });
    this.setNbOfRooms();
  }

  pageChanged(event: any): void {
    this.pagination.currentPage = event.page;
    this.loadApartments();
  }

  resetFilters() {
    this.apartmentParams.minPrice = null;
    this.apartmentParams.maxPrice = null;
    this.apartmentParams.address = null;
    this.apartmentParams.nbOfRooms = null;
    this.loadApartments();
  }

  loadApartments() {
    this.apartmentService.getApartments(this.pagination.currentPage, this.pagination.itemsPerPage,this.apartmentParams)
      .subscribe((res: PaginatedResult<Apartment[]>) => {
        this.apartments = res.result;
        this.pagination = res.pagination;
      }, error => {
        this.alertify.error(error);
      })
  }

  setNbOfRooms() {
    for (let index = 1; index <= 8; index++) {
      this.nbOfRoomsList.push(Number(index));
    }
  }

  createToggle() {
    this.createMode = true;
  }
}