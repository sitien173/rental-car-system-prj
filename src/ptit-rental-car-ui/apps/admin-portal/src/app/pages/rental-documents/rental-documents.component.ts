import {Component, OnInit} from '@angular/core';
import {ToastrService} from "ngx-toastr";
import {BaseComponent} from "../base.component";
import {CreateRentalDocuments, RentalDocuments, UpdateRentalDocuments} from "@ptit.rentalcar.data-models";
import {RentalDocumentsService} from "@ptit.rentalcar.shared";

@Component({
  selector: 'app-rental-documents',
  templateUrl: './rental-documents.component.html',
  styleUrls: ['./rental-documents.component.css'],
  providers: [RentalDocumentsService]
})
export class RentalDocumentsComponent extends BaseComponent<RentalDocuments> implements OnInit {
  constructor(private readonly documentsService: RentalDocumentsService,
              private readonly toastService: ToastrService
  ) {
    super();
  }

  ngOnInit(): void {
    this.loadData();
  }

  loadData() {
    this.documentsService.getAll().subscribe((result) => {
      this.dataSources = result;
    });
  }

  add(data: RentalDocuments): void {
    this.documentsService
      .create(data as CreateRentalDocuments)
      .subscribe(this.proceedObservable({
        successHandler: (result: any) => {
          data.id = result;
          this.toastService.success(this.addSuccessMessage);
        }
      }));
  }

  delete(data: RentalDocuments[]): void {
    const ids = data.map((x) => x.id);
    this.documentsService.delete(ids).subscribe(this.proceedObservable({
      successHandler: () => {
        this.toastService.success(this.deleteSuccessMessage);
      }
    }))
  }

  update(data: RentalDocuments): void {
    this.documentsService.update('patch', data.id, data as UpdateRentalDocuments)
      .subscribe(this.proceedObservable({
        successHandler: () => {
          this.toastService.success(this.updateSuccessMessage);
        }
      }))
  }
}
