import {Component, OnInit} from '@angular/core';
import {BaseComponent} from "../base.component";
import {ToastrService} from "ngx-toastr";
import {
  ColumnModel,
  CommandClickEventArgs,
  CommandModel,
  EditSettingsModel,
  IEditCell,
  ToolbarItems
} from "@syncfusion/ej2-angular-grids";
import {DropDownList} from "@syncfusion/ej2-angular-dropdowns";
import {firstValueFrom} from "rxjs";
import {
  CreateRentalContract,
  RentalRequest,
  RentalRequestEnum,
  RentalRequestEnumDescriptions
} from "@ptit.rentalcar.data-models";
import {DatetimeUtils} from "@ptit.rentalcar.utils";
import {CarService, RentalContractsService, RentalRequestService} from "@ptit.rentalcar.shared";

@Component({
  selector: 'app-rental-request',
  templateUrl: './rental-request.component.html',
  styleUrls: ['./rental-request.component.css'],
  providers: [RentalRequestService, RentalContractsService, CarService]
})
export class RentalRequestComponent extends BaseComponent<RentalRequest> implements OnInit {
  statusDropdown: DropDownList;
  statusData: object[];
  editStatusOption?: IEditCell;
  override toolbar: ToolbarItems[] = ['Delete', 'Update', 'Cancel', 'Search', 'ColumnChooser'];
  override editSettings: EditSettingsModel = {
    allowEditing: true,
    allowAdding: false,
    allowDeleting: true,
    mode: 'Normal'
  };
  commands: CommandModel[];
  constructor(private readonly rentalRequestService: RentalRequestService,
              private readonly toastService: ToastrService,
              private readonly rentalContractService: RentalContractsService,
              private readonly carService: CarService
  ) {
    super();
  }

  ngOnInit(): void {
    this.loadData();

    this.commands = [
      {
        title: 'Tạo hợp đồng',
        buttonOption: {
          content: 'Tạo hợp đồng',
          isPrimary: true
        }
      }
    ];
    this.statusData = [];
    Object.entries(RentalRequestEnumDescriptions).forEach(([key, value]) => {
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
    this.rentalRequestService.getAll().subscribe((result: any) => {
      this.dataSources = result;
    });
  }

  statusValueAccessor(field: string, data: RentalRequest, column: ColumnModel): string {
    return RentalRequestEnumDescriptions[data.status];
  }

  async commandClick(args: CommandClickEventArgs) {
    if (args.commandColumn!.buttonOption?.content === 'Tạo hợp đồng') {
      const argsData = args.rowData as RentalRequest;
      if (argsData.status !== RentalRequestEnum.Pending) {
        this.toastService.error('Yêu cầu thuê xe đã được xử lý');
        return;
      }

      const car = await firstValueFrom(this.carService.getById(argsData.carId));
      const price = car.price;
      const amount = DatetimeUtils.diffDay(new Date(argsData.startDate), new Date(argsData.endDate)) * price;
      const createRentContract = {
        rentalRequestId: argsData.id,
        amount: amount,
        prepaidAmount: amount * 0.5,
        startDate: argsData.startDate,
        endDate: argsData.endDate
      } as CreateRentalContract;
      this.rentalContractService.create(createRentContract).subscribe(this.proceedObservable({
        successHandler: () => {
          this.toastService.success('Tạo hợp đồng thành công');
        }
      }));
    }
  }

  protected add(data: RentalRequest): void {
    throw new Error('Method not implemented.');
  }

  protected delete(data: RentalRequest[]): void {
    this.rentalRequestService.delete(data.map(d => d.id))
      .subscribe(this.proceedObservable({
        successHandler: () => {
          this.toastService.success(this.deleteSuccessMessage);
        }
      }));
  }

  protected update(data: RentalRequest): void {
    this.rentalRequestService.update('PATCH', data.id, data)
      .subscribe(this.proceedObservable({
        successHandler: () => {
          this.toastService.success(this.updateSuccessMessage);
        }
      }));
  }
}
