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
import {BrandService} from "./brand.service";
import {ToastrService} from "ngx-toastr";
import {BaseCRUDComponent} from "../base-crudcomponent";
import {Brand, CreateBrand, UpdateBrand} from "@ptit.rentalcar.data-models";

@Component({
  selector: 'app-brands',
  templateUrl: './brands.component.html',
  styleUrls: ['./brands.component.css'],
  providers: [PageService, GroupService, SortService, FilterService, EditService, ToolbarService, ColumnChooserService, ExcelExportService, PdfExportService]
})
export class BrandsComponent extends BaseCRUDComponent<Brand> implements OnInit {
  constructor(private readonly brandService: BrandService,
              private readonly toastService: ToastrService
  ) {
    super();
  }

  ngOnInit(): void {
    this.loadData();
  }

  loadData() {
    this.brandService.getAll().subscribe(this.proceedObservable({
      successHandler: (result: Brand[]) => {
        this.dataSources = result;
      }
    }));
  }

  add(data: Brand): void {
    this.brandService
      .create(data as CreateBrand)
      .subscribe(this.proceedObservable({
        successHandler: (result: any) => {
          data.id = result;
          this.toastService.success(this.addSuccessMessage);
        }
      }));
  }

  delete(data: Brand[]): void {
    const ids = data.map((x) => x.id);
    this.brandService.delete(ids).subscribe(this.proceedObservable({
      successHandler: () => {
        this.toastService.success(this.deleteSuccessMessage);
      }
    }))
  }

  update(data: Brand): void {
    this.brandService.update('patch', data.id, data as UpdateBrand)
      .subscribe(this.proceedObservable({
        successHandler: () => {
          this.toastService.success(this.updateSuccessMessage);
        }
      }))
  }
}
