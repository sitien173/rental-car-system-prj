import {Component, OnInit} from '@angular/core';
import {ColumnModel, EditSettingsModel, IEditCell, ToolbarItems} from "@syncfusion/ej2-angular-grids";
import {BaseComponent} from "../base.component";
import {ToastrService} from "ngx-toastr";
import {DropDownList} from "@syncfusion/ej2-angular-dropdowns";
import {firstValueFrom} from "rxjs";
import {
  CreatePayment,
  Payment,
  PaymentMethodEnumDescriptions,
  PaymentStatusEnumDescriptions,
  PaymentTypeEnumDescriptions,
  RentalContract
} from "@ptit.rentalcar.data-models";
import {PaymentService, RentalContractsService} from "@ptit.rentalcar.shared";

@Component({
  selector: 'app-payments',
  templateUrl: './payments.component.html',
  styleUrls: ['./payments.component.css'],
  providers: [PaymentService, RentalContractsService]
})
export class PaymentsComponent extends BaseComponent<Payment> implements OnInit {
  override toolbar: ToolbarItems[] = ['Add', 'Delete', 'Update', 'Search', 'Cancel', 'ColumnChooser'];
  override editSettings: EditSettingsModel = {
    allowEditing: false,
    allowAdding: true,
    allowDeleting: true,
    mode: 'Normal'
  };
  editPaymentTypeOption: IEditCell;
  paymentTypeDropdown: DropDownList;
  paymentTypeData: { value: string, name: string }[];

  editPaymentMethodOption: IEditCell;
  paymentMethodDropdown: DropDownList;
  paymentMethodData: { value: string, name: string }[];

  editRentalContractIdOption: IEditCell;
  rentalContractDropdown: DropDownList;
  rentalContractData: RentalContract[];
  constructor(private readonly toastService: ToastrService,
              private readonly paymentService: PaymentService,
              private readonly rentalContractService: RentalContractsService,
  ) {
    super();
  }

  async ngOnInit() {
    this.loadData();

    this.paymentTypeData = [];
    Object.entries(PaymentTypeEnumDescriptions).forEach(([key, value]) => {
      this.paymentTypeData.push({value: key, name: `Loại thanh toán: ${value}`});
    });

    this.paymentMethodData = [];
    Object.entries(PaymentMethodEnumDescriptions).forEach(([key, value]) => {
      this.paymentMethodData.push({value: key, name: `Phương thức thanh toán: ${value}`});
    });

    this.editPaymentTypeOption = {
      create: () => {
        return document.createElement('input');
      },
      read: () => {
        return this.paymentTypeDropdown.value;
      },
      destroy: () => {
        this.paymentTypeDropdown.destroy();
      },
      write: (args: any) => {
        this.paymentTypeDropdown = new DropDownList({
          dataSource: this.paymentTypeData as any,
          placeholder: 'Loại thanh toán',
          fields: {value: 'value', text: 'name'},
          value: args.rowData.paymentType,
          width: '100%'
        });
        this.paymentTypeDropdown.appendTo(args.element);
      }
    }

    this.editPaymentMethodOption = {
      create: () => {
        return document.createElement('input');
      },
      read: () => {
        return this.paymentMethodDropdown.value;
      },
      destroy: () => {
        this.paymentMethodDropdown.destroy();
      },
      write: (args: any) => {
        this.paymentMethodDropdown = new DropDownList({
          dataSource: this.paymentMethodData as any,
          placeholder: 'Phương thức thanh toán',
          fields: {value: 'value', text: 'name'},
          value: args.rowData.paymentMethod,
          width: '100%'
        });
        this.paymentMethodDropdown.appendTo(args.element);
      }
    }

    this.rentalContractData = await firstValueFrom(this.rentalContractService.getAll());
    this.editRentalContractIdOption = {
      create: () => {
        return document.createElement('input');
      },
      read: () => {
        return this.rentalContractDropdown.value;
      },
      destroy: () => {
        this.rentalContractDropdown.destroy();
      },
      write: (args: any) => {
        this.rentalContractDropdown = new DropDownList({
          dataSource: this.rentalContractData as any,
          placeholder: 'Hợp đồng thuê xe',
          fields: {value: 'id', text: 'id'},
          value: args.rowData.rentalContractId,
          width: '100%'
        });
        this.rentalContractDropdown.appendTo(args.element);
      }
    }
  }
  protected add(data: Payment): void {
    this.paymentService.create(data as CreatePayment)
      .subscribe(this.proceedObservable({
        successHandler: () => {
          this.loadData();
          this.toastService.success(this.addSuccessMessage);
        }
      }));
  }

  protected delete(data: Payment[]): void {
    const ids = data.map((x) => x.id);
    this.paymentService.delete(ids)
      .subscribe(this.proceedObservable({
        successHandler: () => {
          this.toastService.success(this.deleteSuccessMessage);
        }
      }));
  }

  protected loadData(): void {
    this.paymentService.getAll().subscribe(data => {
      this.dataSources = data;
    });
  }

  protected update(data: Payment): void {
    throw new Error('Method not implemented.');
  }

  statusValueAccessor(field: string, data: Payment, column: ColumnModel): string {
    return PaymentStatusEnumDescriptions[data.status];
  }

  paymentMethodValueAccessor(field: string, data: Payment, column: ColumnModel): string {
    return PaymentMethodEnumDescriptions[data.paymentMethod];
  }

  paymentTypeValueAccessor(field: string, data: Payment, column: ColumnModel): string {
    return PaymentTypeEnumDescriptions[data.paymentType];
  }

}
