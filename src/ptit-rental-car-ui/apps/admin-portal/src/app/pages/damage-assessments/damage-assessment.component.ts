import {Component, OnInit, ViewEncapsulation} from '@angular/core';
import {
  IEditCell
} from "@syncfusion/ej2-angular-grids";
import {BaseComponent} from "../base.component";
import {DropDownList} from "@syncfusion/ej2-angular-dropdowns";
import {ToastrService} from "ngx-toastr";
import {firstValueFrom} from "rxjs";
import {
  CreateDamageAssessment,
  DamageAssessment,
  RentalContract,
  UpdateDamageAssessment
} from "@ptit.rentalcar.data-models";
import {DamageAssessmentService, RentalContractsService} from "@ptit.rentalcar.shared";



@Component({
  selector: 'app-damage-assessments',
  templateUrl: './damage-assessment.component.html',
  styleUrls: ['./damage-assessment.component.css'],
  providers: [
    DamageAssessmentService,
    RentalContractsService
  ],
  encapsulation: ViewEncapsulation.None,
})
export class DamageAssessmentComponent extends BaseComponent<DamageAssessment> implements OnInit {
  editRentalContractIdOption: IEditCell;
  rentalContractDropdown: DropDownList;
  rentalContractData: RentalContract[];
  constructor(private readonly toastService: ToastrService,
              private readonly damageAssessmentService: DamageAssessmentService,
              private readonly rentalContractService: RentalContractsService,
  ) {
    super();
  }
  async ngOnInit() {
    this.loadData();

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

  protected add(data: DamageAssessment): void {
    this.damageAssessmentService.create(data as CreateDamageAssessment)
      .subscribe(this.proceedObservable({
        successHandler: () => {
          this.toastService.success(this.addSuccessMessage);
        }
      }));
  }

  protected delete(data: DamageAssessment[]): void {
    this.damageAssessmentService.delete(data.map(p => p.id))
      .subscribe(this.proceedObservable({
        successHandler: () => {
          this.toastService.success(this.deleteSuccessMessage);
        }
      }));
  }

  protected loadData(): void {
    this.damageAssessmentService.getAll().subscribe(data => {
      this.dataSources = data;
    });
  }

  protected update(data: DamageAssessment): void {
    this.damageAssessmentService.update('patch', data.id, data as UpdateDamageAssessment)
      .subscribe(this.proceedObservable({
        successHandler: () => {
          this.toastService.success(this.updateSuccessMessage);
        }
      }));
  }
}
