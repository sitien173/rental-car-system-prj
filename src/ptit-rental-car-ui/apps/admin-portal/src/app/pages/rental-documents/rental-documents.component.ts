import {Component, OnInit} from '@angular/core';
import {
  ColumnChooserService,
  EditService,
  ExcelExportService,
  FilterService,
  GroupService,
  PageService,
  PdfExportService,
  SortService,
  ToolbarService
} from "@syncfusion/ej2-angular-grids";
import {ToastrService} from "ngx-toastr";
import {BaseCRUDComponent} from "../base-crudcomponent";
import {RentalDocumentsService} from "./rental-documents.service";
import {CreateRentalDocuments, RentalDocuments, UpdateRentalDocuments} from "@ptit.rentalcar.data-models";

@Component({
  selector: 'app-rental-documents',
  templateUrl: './rental-documents.component.html',
  styleUrls: ['./rental-documents.component.css'],
  providers: [PageService, GroupService, SortService, FilterService, EditService, ToolbarService, ColumnChooserService, ExcelExportService, PdfExportService]
})
export class RentalDocumentsComponent extends BaseCRUDComponent<RentalDocuments> implements OnInit {
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
