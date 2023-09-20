import {Component, OnInit, ViewChild} from '@angular/core';
import {
  ColumnChooserService,
  ColumnModel,
  CommandColumnService,
  DataSourceChangedEventArgs,
  EditService,
  ExcelExportService,
  FilterService,
  GridComponent,
  GroupService,
  IEditCell,
  PageService,
  PdfExportService,
  SortService,
  ToolbarItems,
  ToolbarService
} from "@syncfusion/ej2-angular-grids";
import {BaseCRUDComponent} from "../base-crudcomponent";
import {ToastrService} from "ngx-toastr";
import {RentalContractsService} from "./rental-contracts.service";
import {DropDownList} from "@syncfusion/ej2-angular-dropdowns";
import {RentalContract, RentalContractStatusEnumDescriptions, UpdateRentalContract} from "@ptit.rentalcar.data-models";

@Component({
  selector: 'app-rental-contracts',
  templateUrl: './rental-contracts.component.html',
  styleUrls: ['./rental-contracts.component.css'],
  providers: [PageService, GroupService, SortService, FilterService, EditService, ToolbarService, ColumnChooserService, ExcelExportService, PdfExportService, CommandColumnService]
})
export class RentalContractComponent extends BaseCRUDComponent<RentalContract> implements OnInit {
  override toolbar: ToolbarItems[] = ['Edit', 'Delete', 'Update', 'Search', 'Cancel', 'ColumnChooser'];
  editStatusOption: IEditCell;
  statusDropdown: DropDownList;
  statusData: { value: string, name: string }[];

  @ViewChild('grid', {static: true})
  public grid?: GridComponent;
  constructor(private readonly toastService: ToastrService,
              private readonly rentalContractService: RentalContractsService,
  ) {
    super();
  }

  async ngOnInit() {
    this.loadData();

    this.statusData = [];
    Object.entries(RentalContractStatusEnumDescriptions).forEach(([key, value]) => {
      this.statusData.push({value: key, name: value});
    });

    this.editStatusOption = {
      create: () => {
        return document.createElement('input');
      },
      read: () => {
        return this.statusDropdown.value;
      },
      destroy: () => {
        this.statusDropdown.destroy();
      },
      write: (args: any) => {
        this.statusDropdown = new DropDownList({
          dataSource: this.statusData as any,
          placeholder: 'Trạng thái',
          fields: {value: 'value', text: 'name'},
          value: args.rowData.status,
          width: '100%'
        });
        this.statusDropdown.appendTo(args.element);
      }
    }
  }

  loadData() {
    this.rentalContractService.getAll().subscribe((result) => {
      this.dataSources = result;
    });
  }

  add(data: RentalContract): void {
    throw new Error('Method not implemented.');
  }

  delete(data: RentalContract[]): void {
    const ids = data.map((x) => x.id);
    this.rentalContractService.delete(ids).subscribe(this.proceedObservable({
      successHandler: () => {
        this.toastService.success(this.deleteSuccessMessage);
      }
    }))
  }

  update(data: RentalContract): void {
    this.rentalContractService.update('patch', data.id, data as UpdateRentalContract)
      .subscribe(this.proceedObservable({
        successHandler: () => {
          this.toastService.success(this.updateSuccessMessage);
        }
      }))
  }

  actionBegin(event: any) {
    if (event.requestType === 'add' || event.requestType === 'beginEdit') {
      const isEdit = event.requestType === 'beginEdit';
      this.grid!.getColumnByField('status').visible = isEdit;
      this.grid!.getColumnByField('created').visible = isEdit;
      this.grid!.getColumnByField('createdBy').visible = isEdit;
      this.grid!.getColumnByField('lastModified').visible = isEdit;
      this.grid!.getColumnByField('lastModifiedBy').visible = isEdit;
      this.grid!.getColumnByField('cancellationDate').visible = isEdit;
      this.grid!.getColumnByField('cancellationReason').visible = isEdit;
      this.grid!.getColumnByField('accidentDate').visible = isEdit;
      this.grid!.getColumnByField('accidentDescription').visible = isEdit;
      this.grid!.getColumnByField('lateDate').visible = isEdit;
      this.grid!.getColumnByField('lateReason').visible = isEdit;
    }
  }

  statusValueAccessor(field: string, data: RentalContract, column: ColumnModel): string {
    return RentalContractStatusEnumDescriptions[data.status];
  }

  protected override saveChanges(args: DataSourceChangedEventArgs) {
    super.saveChanges(args);

    if (args.requestType === 'save' || args.requestType === 'cancel') {
      this.grid!.getColumnByField('status').visible = true;
      this.grid!.getColumnByField('created').visible = true;
      this.grid!.getColumnByField('createdBy').visible = true;
      this.grid!.getColumnByField('lastModified').visible = true;
      this.grid!.getColumnByField('lastModifiedBy').visible = true;
      this.grid!.getColumnByField('cancellationDate').visible = true;
      this.grid!.getColumnByField('cancellationReason').visible = true;
      this.grid!.getColumnByField('accidentDate').visible = true;
      this.grid!.getColumnByField('accidentDescription').visible = true;
      this.grid!.getColumnByField('lateDate').visible = true;
      this.grid!.getColumnByField('lateReason').visible = true;
      this.grid!.refreshColumns();
    }
  }
}

