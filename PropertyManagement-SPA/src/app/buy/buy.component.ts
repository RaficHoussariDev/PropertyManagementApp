import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, ActivatedRouteSnapshot, Router } from '@angular/router';
import { Buyer } from '../_models/buyer';
import { AlertifyService } from '../_services/alertify.service';
import { BuyerService } from '../_services/buyer.service';

@Component({
  selector: 'app-buy',
  templateUrl: './buy.component.html',
  styleUrls: ['./buy.component.css']
})
export class BuyComponent implements OnInit {
  buyers: Buyer[];
  model: any = {};
  buyerInfo: any = {}
  show: boolean = false;

  constructor(private buyerService: BuyerService, private route: ActivatedRoute,
    private alertify: AlertifyService, private router: Router) { 
    
  }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.buyers = data['buyers'];
    });
  }

  loadBuyers() {
    this.buyerService.getBuyers().subscribe((buyers: Buyer[]) => {
      this.buyers = buyers;
    }, error => {
      this.alertify.error(error);
    }, () => {

    });
  }

  setBuyerInfo() {
    for(let index = 0; index < this.buyers.length; index++) {
      if(this.model.buyerId == this.buyers[index].id) {
        this.buyerInfo.fullName = this.buyers[index].fullName;
        this.buyerInfo.credit = this.buyers[index].credit;
        this.show = true;
      }
    }
  }

  buy() {
    this.route.params.subscribe(params => {
      let id = params['id'];
      this.model.apartmentId = id;
    });
    this.buyerService.buy(this.model).subscribe(() => {
      this.alertify.success("Congratulations, the apartment belongs to the buyer now");
    }, error => {
      this.alertify.error(error);
    })
    this.router.navigate(['/apartments']);
  }
}