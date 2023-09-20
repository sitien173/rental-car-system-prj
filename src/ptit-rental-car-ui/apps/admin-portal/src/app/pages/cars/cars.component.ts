import {Component, OnInit, ViewChild} from '@angular/core';
import {BaseCRUDComponent} from "../base-crudcomponent";
import {CarService} from "./car.service";
import {ToastrService} from "ngx-toastr";
import {ColumnModel, DataSourceChangedEventArgs, GridComponent, IEditCell} from "@syncfusion/ej2-angular-grids";
import {BrandService} from "../brands/brand.service";
import {FeatureService} from "../features/feature.service";
import {firstValueFrom} from "rxjs";
import {
  CheckBoxSelectionService,
  DropDownList,
  MultiSelect
} from "@syncfusion/ej2-angular-dropdowns";
import {CartypeService} from "../cartype/cartype.service";
import {AdditionalFeesService} from "../additional-fees/additional-fees.service";
import {RentalDocumentsService} from "../rental-documents/rental-documents.service";
import {FilesPropModel, Uploader} from "@syncfusion/ej2-angular-inputs";
import {
  AdditionalFees,
  Brand,
  Car, CarStatusEnum,
  CarStatusEnumDescriptions,
  Cartype, CreateCar,
  Feature,
  Image,
  RentalDocuments, UpdateCar, UploadFileResponse
} from '@ptit.rentalcar.data-models';
import {environment} from "@ptit.rentalcar.app-config";

@Component({
  selector: 'app-cars',
  templateUrl: './cars.component.html',
  styleUrls: ['./cars.component.css'],
  providers: [CheckBoxSelectionService]
})
export class CarsComponent extends BaseCRUDComponent<Car> implements OnInit {
  editBrandOption?: IEditCell;
  editCarTypeOption?: IEditCell;
  editFeaturesOption?: IEditCell;
  editRentalDocumentsOption?: IEditCell;
  editAdditionalFeesOption?: IEditCell;
  imagesUploadOption?: IEditCell;
  editStatusOption?: IEditCell;

  @ViewChild('grid', {static: true})
  public grid?: GridComponent;

  brandData: Brand[];
  brandDropdown: DropDownList;

  carTypeData: Cartype[];
  carTypeDropdown: DropDownList;

  featuresData: Feature[];
  featuresDropdown: MultiSelect;

  rentalDocumentData: RentalDocuments[];
  rentalDocumentDropdown: MultiSelect;

  statusDropdown: DropDownList;
  statusData: object[];

  additionalFeeData: AdditionalFees[];
  additionalFeeDropdown: MultiSelect;

  uploader?: Uploader;
  maxImageFileSize = 20_000_000; // 20MB

  imagesMap: Map<string, Image>;

  constructor(private readonly carService: CarService,
              private readonly toastrService: ToastrService,
              private readonly brandService: BrandService,
              private readonly featureService: FeatureService,
              private readonly carTypeService: CartypeService,
              private readonly rentalDocumentService: RentalDocumentsService,
              private readonly additionalFeeService: AdditionalFeesService
  ) {
    super();
  }

  async ngOnInit() {
    this.imagesMap = new Map<string, Image>();

    this.loadData();

    this.brandData = await firstValueFrom(
      this.brandService.getAll()
    );

    this.carTypeData = await firstValueFrom(
      this.carTypeService.getAll()
    );

    this.featuresData = await firstValueFrom(
      this.featureService.getAll()
    );

    this.rentalDocumentData = await firstValueFrom(
      this.rentalDocumentService.getAll()
    );

    this.additionalFeeData = await firstValueFrom(
      this.additionalFeeService.getAll()
    );

    this.statusData = [];
    Object.entries(CarStatusEnumDescriptions).forEach(([key, value]) => {
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

    this.editCarTypeOption = {
      create: () => {
        return document.createElement('input');
      },
      read: () => {
        return this.carTypeData.find(c => c.id === this.carTypeDropdown.value);
      },
      destroy: () => {
        this.carTypeDropdown.destroy();
      },
      write: (args: any) => {
        this.carTypeDropdown = new DropDownList({
          dataSource: this.carTypeData as any,
          fields: {value: 'id', text: 'name'},
          placeholder: 'Chọn loại xe',
          value: args.rowData.carType?.id,
          width: '100%'
        });
        this.carTypeDropdown.appendTo(args.element);
      }
    }

    this.editBrandOption = {
      create: () => {
        return document.createElement('input');
      },
      read: () => {
        return this.brandData.find(b => b.id === this.brandDropdown.value);
      },
      destroy: () => {
        this.brandDropdown.destroy();
      },
      write: (args: any) => {
        this.brandDropdown = new DropDownList({
          dataSource: this.brandData as any,
          fields: {value: 'id', text: 'name'},
          placeholder: 'Chọn hãng xe',
          value: args.rowData.brand?.id,
          width: '100%'
        });
        this.brandDropdown.appendTo(args.element);
      }
    }

    this.editFeaturesOption = {
      create: () => {
        return document.createElement('input');
      },
      read: (): Feature[] => {
        return this.featuresData.filter(f => this.featuresDropdown.value.some(v => v === f.id));
      },
      destroy: () => {
        this.featuresDropdown.destroy();
      },
      write: (args: any) => {
        this.featuresDropdown = new MultiSelect({
          dataSource: this.featuresData as any,
          fields: {value: 'id', text: 'name'},
          placeholder: 'Chọn tính năng',
          value: args.rowData.features?.map((f: Feature) => f.id),
          mode: 'CheckBox',
          showClearButton: true,
          showDropDownIcon: true,
          enableSelectionOrder: true,
          width: '100%',
          showSelectAll: true,
          allowFiltering: true,
        });
        this.featuresDropdown.appendTo(args.element);
      }
    }

    this.editAdditionalFeesOption = {
      create: () => {
        return document.createElement('input');
      },
      read: (): AdditionalFees[] => {
        return this.additionalFeeData.filter(f => this.additionalFeeDropdown.value.some(v => v === f.id));
      },
      destroy: () => {
        this.additionalFeeDropdown.destroy();
      },
      write: (args: any) => {
        this.additionalFeeDropdown = new MultiSelect({
          dataSource: this.additionalFeeData as any,
          fields: {value: 'id', text: 'name'},
          placeholder: 'Chọn phụ phí',
          value: args.rowData.additionalFees?.map((f: AdditionalFees) => f.id),
          mode: 'CheckBox',
          showClearButton: true,
          showDropDownIcon: true,
          enableSelectionOrder: true,
          width: '100%',
          showSelectAll: true,
          allowFiltering: true,
        });
        this.additionalFeeDropdown.appendTo(args.element);
      }
    }
    this.editRentalDocumentsOption = {
      create: () => {
        return document.createElement('input');
      },
      read: (): RentalDocuments[] => {
        return this.rentalDocumentData.filter(f => this.rentalDocumentDropdown.value.some(v => v === f.id));
      },
      destroy: () => {
        this.rentalDocumentDropdown.destroy();
      },
      write: (args: any) => {
        this.rentalDocumentDropdown = new MultiSelect({
          dataSource: this.rentalDocumentData as any,
          fields: {value: 'id', text: 'name'},
          placeholder: 'Chọn giấy tờ',
          value: args.rowData.rentalDocuments?.map((f: RentalDocuments) => f.id) || [],
          mode: 'CheckBox',
          showClearButton: true,
          showDropDownIcon: true,
          enableSelectionOrder: true,
          width: '100%',
          showSelectAll: true,
          allowFiltering: true,
        });
        this.rentalDocumentDropdown.appendTo(args.element);
      }
    }
    this.imagesUploadOption = {
      create: () => {
        const input = document.createElement('input');
        input.type = 'file';
        input.accept = 'image/*';
        return input;
      },
      read: () => {
        return [...this.imagesMap.values()];
      },
      write: (args: any) => {
        this.uploader = this.getUploader(args);
        this.uploader.appendTo(args.element);
      },
      destroy: () => {
        this.uploader?.destroy();
      }
    };
  }

  brandValueAccessor(field: string, data: Car, column: ColumnModel): string {
    return data.brand?.name;
  }

  carTypeValueAccessor(field: string, data: Car, column: ColumnModel): string {
    return data.carType?.name;
  }

  featuresValueAccessor(field: string, data: Car, column: ColumnModel): string {
    return data.features?.map(f => f.name).join(', ');
  }

  rentalDocumentsValueAccessor(field: string, data: Car, column: ColumnModel): string {
    return data.rentalDocuments?.map(f => f.name).join(', ');
  }

  additionalFeesValueAccessor(field: string, data: Car, column: ColumnModel): string {
    return data.additionalFees?.map(f => f.name).join(', ');
  }

  statusValueAccessor(field: string, data: Car, column: ColumnModel): string {
    return CarStatusEnumDescriptions[data.status];
  }

  actionBegin(args: any) {
    if (args.requestType === 'add' || args.requestType === 'beginEdit') {
      this.grid!.getColumnByField('images').visible = true;
      this.grid!.getColumnByField('status').visible = args.requestType === 'beginEdit';
    }
  }

  protected override saveChanges(args: DataSourceChangedEventArgs) {
    super.saveChanges(args);

    if (args.requestType === 'save' || args.requestType === 'cancel') {
      this.grid!.getColumnByField('images').visible = false;
      this.grid!.getColumnByField('status').visible = true;
      this.grid!.refreshColumns();
    }
  }

  protected add(data: Car): void {
    let request = data as CreateCar;
    request.featureIds = data.features?.map(f => f.id) || [];
    request.additionalFeeIds = data.additionalFees?.map(f => f.id) || [];
    request.rentalDocumentIds = data.rentalDocuments?.map(f => f.id) || [];
    request.brandId = data.brand?.id;
    request.carTypeId = data.carType?.id;
    request.images = [...this.imagesMap.values()];
    data.status = CarStatusEnum.Available;

    this.carService.create(request)
      .subscribe(this.proceedObservable({
        successHandler: () => {
          this.toastrService.success(this.addSuccessMessage);
        }
      }));
  }

  protected delete(data: Car[]): void {
    this.carService.delete(data.map(d => d.id))
      .subscribe(this.proceedObservable({
        successHandler: () => {
          this.toastrService.success(this.deleteSuccessMessage);
        }
      }));
  }

  protected loadData(): void {
    this.carService.getAll().subscribe((data: Car[]) => {
      this.dataSources = data;
    });
  }

  protected update(data: Car): void {
    let request = data as UpdateCar;
    request.featureIds = data.features?.map(f => f.id) || [];
    request.additionalFeeIds = data.additionalFees?.map(f => f.id) || [];
    request.rentalDocumentIds = data.rentalDocuments?.map(f => f.id) || [];
    request.brandId = data.brand?.id;
    request.carTypeId = data.carType?.id;

    this.carService.update('PATCH', data.id, request)
      .subscribe(this.proceedObservable({
        successHandler: () => {
          this.toastrService.success(this.updateSuccessMessage);
        }
      }));
  }

  private getUploader(args: any) {

    return new Uploader({
      asyncSettings: {
        saveUrl: environment.apiUrl + 'upload',
        removeUrl: environment.apiUrl + 'upload/delete',
      },
      autoUpload: false,
      multiple: true,
      allowedExtensions: this.allowedImageExtensions,
      maxFileSize: this.maxImageFileSize,
      sequentialUpload: true,
      files: args.rowData.images?.map((i: Image) => {
        return {
          name: i.fileName.replace(i.type, ''),
          size: i.size,
          type: i.type
        } as FilesPropModel;
      }) || [],
      buttons: {browse: 'Tải ảnh xe'},
      uploading: (args: any) => {
        args.customFormData = [{shouldCreateThumbnail: 'true'}];
      },
      removing: (args: any) => {
        args.postRawFile = false;
        const fileName = args.filesData[0].name;
        const removeFile = this.imagesMap.has(fileName) ? this.imagesMap.get(fileName)?.fileName : fileName;
        args.customFormData = [{filename: removeFile}];
      },
      success: (response: any) => {
        if (response.operation == 'upload') {
          const fileUploadResponse = (JSON.parse(response.e.currentTarget.responseText) as UploadFileResponse[])[0];
          this.imagesMap.set(response.file.name, fileUploadResponse as Image);
        } else if (response.operation == 'remove') {
          const fileNameToRemove = response.file.name;
          this.imagesMap.delete(fileNameToRemove);
        }
      }
    });
  }
}
