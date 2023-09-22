import {Component, OnInit} from '@angular/core';
import {ToastrService} from "ngx-toastr";
import {BaseComponent} from "../base.component";
import {AdditionalFees} from "@ptit.rentalcar.data-models";
import {AdditionalFeesService} from "@ptit.rentalcar.shared";

@Component({
  selector: 'app-additional-fees',
  templateUrl: './additional-fees.component.html',
  styleUrls: ['./additional-fees.component.css'],
  providers: [AdditionalFeesService]
})
export class AdditionalFeesComponent extends BaseComponent<AdditionalFees> implements OnInit {
  constructor(private readonly additionalFeesService: AdditionalFeesService,
              private readonly toastService: ToastrService
  ) {
    super();
  }

  ngOnInit(): void {
    this.loadData();
  }

  loadData() {
    this.additionalFeesService.getAll().subscribe((result) => {
      this.dataSources = result;
    });
  }

  delete(data: AdditionalFees[]) {
    const ids = data.map((x) => x.id);
    this.additionalFeesService.delete(ids).subscribe(this.proceedObservable({
      successHandler: () => {
        this.toastService.success(this.deleteSuccessMessage);
      }
    }));
  }

  update(data: AdditionalFees) {
    this.additionalFeesService.update('patch', data.id, {
      name: data.name,
      description: data.description,
      price: data.price,
      unit: data.unit
    }).subscribe(this.proceedObservable({
      successHandler: () => {
        this.toastService.success(this.updateSuccessMessage);
      }
    }));
  };

  add(data: AdditionalFees) {
    this.additionalFeesService.create({
      name: data.name,
      description: data.description,
      price: data.price,
      unit: data.unit
    })
      .subscribe(this.proceedObservable({
        successHandler: (result: any) => {
          data.id = result;
          this.toastService.success(this.addSuccessMessage);
        }
      }));
  };
}
