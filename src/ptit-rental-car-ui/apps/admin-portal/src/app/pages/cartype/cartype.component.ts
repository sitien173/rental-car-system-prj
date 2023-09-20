import {Component, OnInit, ViewEncapsulation} from '@angular/core';
import {
  ColumnChooserService,
  EditService,
  ExcelExportService,
  FilterService,
  GroupService,
  IEditCell,
  PageService,
  PdfExportService,
  SortService,
  ToolbarService
} from "@syncfusion/ej2-angular-grids";
import {ToastrService} from "ngx-toastr";
import {Uploader} from "@syncfusion/ej2-angular-inputs";
import {BaseCRUDComponent} from "../base-crudcomponent";
import {Cartype, CreateCartype, Icon, UpdateCartype, UploadFileResponse} from "@ptit.rentalcar.data-models";
import {CartypeService} from "./cartype.service";
import {environment} from "@ptit.rentalcar.app-config";

@Component({
  selector: 'app-feature',
  templateUrl: './cartype.component.html',
  styleUrls: ['./cartype.component.css'],
  providers: [
    PageService, GroupService, SortService, FilterService, EditService,
    ToolbarService, ColumnChooserService, ExcelExportService, PdfExportService
  ],
  encapsulation: ViewEncapsulation.None,
})
export class CartypeComponent extends BaseCRUDComponent<Cartype> implements OnInit {
  dpParams: IEditCell;
  uploader?: Uploader;
  iconUploaded?: Icon;
  isProcessIcon = false;
  maxImageFileSize = 1000000; // 1MB

  constructor(
    private readonly cartypeService: CartypeService,
    private readonly toastService: ToastrService,
  ) {
    super();
  }

  ngOnInit(): void {
    this.loadData();
    this.dpParams = {
      create: () => {
        const input = document.createElement('input');
        input.type = 'file';
        input.accept = 'image/*';
        return input;
      },
      read: () => {
        return this.iconUploaded;
      },
      write: (args: any) => {
        this.iconUploaded = args.rowData.icon;
        this.uploader = this.getUploader();
        this.uploader.appendTo(args.element);

        // disable remove button
        const removeButton = document.getElementsByClassName('e-file-delete-btn').item(0);
        removeButton?.classList.add('d-none');
      },
      destroy: () => {
        if (this.uploader) {
          this.uploader.destroy();
          this.iconUploaded = undefined;
        }
      }
    };
  }

  loadData() {
    this.cartypeService.getAll().subscribe(result => {
      this.dataSources = result;
    });
  }

  actionBegin(event: any) {
    if (event.requestType === 'save') {
      if (event.action === 'add' && this.isProcessIcon) {
        this.toastService.info('Đang tải lên icon, vui lòng đợi');
        event.cancel = true;
      }

      if (this.uploader?.filesData.length == 0) {
        this.toastService.error('Vui lòng tải lên icon');
        event.cancel = true;
      }
    }
  }

  delete(data: Cartype[]) {
    const ids = data.map((x) => x.id);
    this.cartypeService.delete(ids)
      .subscribe(this.proceedObservable({
        successHandler: () => {
          this.toastService.success(this.deleteSuccessMessage);
        }
      }));
  }

  update(data: Cartype) {
    this.cartypeService.update('patch', data.id, data as UpdateCartype)
      .subscribe(this.proceedObservable({
        successHandler: () => {
          this.toastService.success(this.updateSuccessMessage);
        }
      }));
  }

  add(data: Cartype) {
    this.cartypeService.create(data as CreateCartype)
      .subscribe(this.proceedObservable({
        successHandler: (id: string) => {
          data.id = id;
          this.toastService.success(this.addSuccessMessage);
        }
      }));
  }

  private getUploader() {
    return new Uploader({
      asyncSettings: {
        saveUrl: environment.apiUrl + 'upload'
      },
      autoUpload: true,
      multiple: false,
      allowedExtensions: this.allowedImageExtensions,
      maxFileSize: this.maxImageFileSize,
      files: this.iconUploaded ? [{
        name: this.iconUploaded.fileName,
        size: this.iconUploaded.size,
        type: this.iconUploaded.type
      }] : [],
      buttons: {browse: 'Tải lên'},
      uploading: () => {
        this.isProcessIcon = true;
      },
      success: (response: any) => {
        if (response.operation == 'upload') {
          const fileUploadResponse: UploadFileResponse[] = JSON.parse(response.e.currentTarget.responseText);
          this.iconUploaded = fileUploadResponse[0] as Icon;

          // disable remove button
          const removeButton = document.getElementsByClassName('e-file-delete-btn')[0];
          removeButton?.classList.add('d-none');
        } else {
          this.iconUploaded = undefined;
        }
      },
      actionComplete: () => {
        this.isProcessIcon = false;
      }
    });
  }
}
