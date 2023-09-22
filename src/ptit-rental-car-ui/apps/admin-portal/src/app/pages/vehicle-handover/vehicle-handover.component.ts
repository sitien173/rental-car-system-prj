import {Component, OnInit, ViewEncapsulation} from '@angular/core';
import {ColumnModel, SaveEventArgs} from "@syncfusion/ej2-angular-grids";
import {BaseComponent} from "../base.component";
import {ToastrService} from "ngx-toastr";
import {FormArray, FormBuilder, FormGroup} from "@angular/forms";
import {FormValidators} from '@syncfusion/ej2-angular-inputs';
import {
  CheckListItemStatusEnumDescriptions,
  CreateVehicleHandover,
  HandoverTypeEnumDescriptions,
  RentalContract,
  SelectListItem,
  UpdateVehicleHandover,
  VehicleHandover
} from "@ptit.rentalcar.data-models";
import {RentalContractsService, VehicleHandoverService} from "@ptit.rentalcar.shared";

@Component({
  selector: 'app-vehicle-handover',
  templateUrl: './vehicle-handover.component.html',
  styleUrls: ['./vehicle-handover.component.css'],
  providers: [
    VehicleHandoverService, RentalContractsService
  ],
  encapsulation: ViewEncapsulation.None,
})
export class VehicleHandoverComponent extends BaseComponent<VehicleHandover> implements OnInit {
  rentalContractData?: RentalContract[] | any;
  handoverTypeData?: SelectListItem[] | any;
  checkListItemStatusData?: SelectListItem[] | any;
  vehicleHandoverForm: FormGroup;
  constructor(private readonly toastService: ToastrService,
              private readonly vehicleHandoverService: VehicleHandoverService,
              private readonly rentalContractService: RentalContractsService,
              private readonly formBuilder: FormBuilder
  ) {
    super();
  }
  ngOnInit() {
    this.loadData();

    this.rentalContractService.getAll().subscribe(data => {
      this.rentalContractData = data;
    });

    this.handoverTypeData = [];
    Object.entries(HandoverTypeEnumDescriptions).forEach(([key, value]) => {
      this.handoverTypeData.push({id: key, name: value});
    });

    this.checkListItemStatusData = [];
    Object.entries(CheckListItemStatusEnumDescriptions).forEach(([key, value]) => {
      this.checkListItemStatusData.push({id: key, name: value});
    });
  }

  get checkListItems() {
    return this.vehicleHandoverForm.get('checkListItems') as FormArray;
  }
  protected add(data: VehicleHandover): void {
    this.vehicleHandoverService.create(data as CreateVehicleHandover)
      .subscribe(this.proceedObservable({
        successHandler: () => {
          this.toastService.success(this.addSuccessMessage);
        }
      }));
  }

  protected delete(data: VehicleHandover[]): void {
    this.vehicleHandoverService.delete(data.map(p => p.id))
      .subscribe(this.proceedObservable({
        successHandler: () => {
          this.toastService.success(this.deleteSuccessMessage);
        }
      }));
  }

  protected loadData(): void {
    this.vehicleHandoverService.getAll().subscribe(data => {
      this.dataSources = data;
    });
  }

  protected update(data: VehicleHandover): void {
    this.vehicleHandoverService.update('patch', data.id, data as UpdateVehicleHandover)
      .subscribe(this.proceedObservable({
        successHandler: () => {
          this.toastService.success(this.updateSuccessMessage);
        }
      }));
  }
  handoverTypeValueAccessor(field: string, data: VehicleHandover, column: ColumnModel): string {
    return HandoverTypeEnumDescriptions[data.handoverType];
  }

  checkListItemsValueAccessor(field: string, data: VehicleHandover, column: ColumnModel): string {
    return data.checkListItems.map(p => p.name).join(', ');
  }

  actionBegin(args: SaveEventArgs): void {
    if(args.requestType === 'save')
    {
      if (this.vehicleHandoverForm.invalid) {
        this.toastService.error('Vui lòng điền đầy đủ thông tin');
        args.cancel = true;
        return;
      } else {
        const data = args.data as VehicleHandover;
        data.checkListItems = this.vehicleHandoverForm.value.checkListItems;
        data.handoverType = this.vehicleHandoverForm.value.handoverType;
        data.rentalContractId = this.vehicleHandoverForm.value.rentalContractId;
        args.data = data;
      }
    }
    else if(args.requestType === 'beginEdit' || args.requestType === 'add')
    {
      const data = args.rowData as VehicleHandover;
      this.vehicleHandoverForm = this.getVehicleHandoverForm(data);
    }
  }

  private getVehicleHandoverForm(data: VehicleHandover | null = null) {
    const checkListItems = data?.checkListItems?.map(p => this.formBuilder.group({
      name: [p.name, [FormValidators.required]],
      status: [p.status, [FormValidators.required]],
      comment: [p.comment, [FormValidators.required]],
    })) ?? [];

    return this.formBuilder.group({
      handoverType: [data?.handoverType, [FormValidators.required]],
      rentalContractId: [data?.rentalContractId, [FormValidators.required]],
      checkListItems: this.formBuilder.array(checkListItems, [FormValidators.required]),
    });
  }

  addCheckListItems() {
    const controls = this.formBuilder.group({
      name: ['', [FormValidators.required]],
      status: ['', [FormValidators.required]],
      comment: ['', [FormValidators.required]],
    });

    this.checkListItems.push(controls);
  }

  removeCheckListItems(i: number) {
    this.checkListItems.removeAt(i);
  }
}
