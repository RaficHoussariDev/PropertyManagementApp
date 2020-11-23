import { Component, EventEmitter, HostListener, Input, OnInit, Output, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Apartment } from '../_models/apartment';
import { AlertifyService } from '../_services/alertify.service';
import { ApartmentService } from '../_services/apartment.service';

@Component({
  selector: 'app-apartment-detail',
  templateUrl: './apartment-detail.component.html',
  styleUrls: ['./apartment-detail.component.css']
})
export class ApartmentDetailComponent implements OnInit {
  apartment: Apartment;
  newApartment: any = {};
  @Output() cancelCreate = new EventEmitter();
  @Input() item: boolean;
  @ViewChild('editForm', {static: true}) editForm: NgForm;
  @HostListener('window: beforeunload', ['$event'])
  unloadNotification($event: any) {
    if (this.editForm.dirty) {
      $event.returnValue = true;
    }
  }

  constructor(private route: ActivatedRoute, private alertify: AlertifyService,
    private apartmentService: ApartmentService) { }

  ngOnInit() {
    if(!this.item) {
      this.route.data.subscribe(data => {
        this.apartment = data['apartment'];
      });
    }
  }

  manageApartment() {
    if(!this.item) {
      this.apartmentService.updateApartment(this.apartment.id, this.apartment).subscribe(
        next => {
          this.alertify.success('Profile updated successfully');
        }, error => {
          this.alertify.error(error);
        }
      );
    }
    else {
      this.apartmentService.createApartment(this.newApartment).subscribe(
        next => {
          this.alertify.success('Successfully added new apartment');
        }, error => {
          this.alertify.error(error);
        }
      );
      this.editForm.reset(this.apartment);
    }
  }
}