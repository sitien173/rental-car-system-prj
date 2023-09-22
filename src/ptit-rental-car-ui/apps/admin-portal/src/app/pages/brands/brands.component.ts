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
import {BaseComponent} from "../base.component";
import {Brand, CreateBrand, UpdateBrand} from "@ptit.rentalcar.data-models";
import {BrandService} from "@ptit.rentalcar.shared";

@Component({
  selector: 'app-brands',
  templateUrl: './brands.component.html',
  styleUrls: ['./brands.component.css'],
  providers: [BrandService]
})
export class BrandsComponent extends BaseComponent<Brand> implements OnInit {
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
