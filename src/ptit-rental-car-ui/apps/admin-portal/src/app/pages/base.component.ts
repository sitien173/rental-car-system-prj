import {
  ColumnChooser,
  DataSourceChangedEventArgs, Edit,
  EditSettingsModel, ExcelExport, Filter,
  FilterSettingsModel, GridComponent, Group,
  LoadingIndicatorModel, Page,
  PageSettingsModel, PdfExport, Print, Selection,
  SelectionSettingsModel, Toolbar,
  ToolbarItems
} from "@syncfusion/ej2-angular-grids";
import {CheckBoxSelection, MultiSelect} from "@syncfusion/ej2-angular-dropdowns";
import {Component, Inject, ViewChild} from "@angular/core";
import {ToastrService} from "ngx-toastr";

@Component({
  template: ''
})
export abstract class BaseComponent<T> {
  @ViewChild('grid', {static: true})
  protected grid?: GridComponent;

  @Inject('') private _toast: ToastrService;

  protected dataSources: T[] | any;
  protected allowSorting = true;
  protected allowFiltering = true;
  protected allowPaging = true;
  protected allowExcelExport = true;
  protected allowPdfExport = true;
  protected showColumnChooser = true;
  protected allowGrouping = true;
  protected allowReordering = true;
  protected allowResizing = true;
  protected allowTextWrap = true;
  protected enableAutoFill = true;
  protected pageSettings: PageSettingsModel = {pageSize: 10, pageSizes: true, currentPage: 1, pageCount: 4};
  protected editSettings: EditSettingsModel = {
    allowEditing: true,
    allowAdding: true,
    allowDeleting: true,
    mode: 'Dialog',
    showDeleteConfirmDialog: true,
    showConfirmDialog: true,
  };
  protected toolbar: ToolbarItems[] = ['Add', 'Edit', 'Delete', 'Update', 'Search', 'Cancel', 'ColumnChooser', 'Print', 'ExcelExport', 'CsvExport'];
  protected autoFit: true;
  protected allowedImageExtensions = '.jpg, .png, .jpeg, .bmp, .gif, .svg, .ico, .webp';
  protected addSuccessMessage = 'Thêm thành công';
  protected updateSuccessMessage = 'Cập nhật thành công';
  protected deleteSuccessMessage = 'Xóa thành công';
  protected dateFormat = 'dd-MM-yyyy';
  protected dateTimeFormat = 'dd-MM-yyyy HH:mm:ss';
  protected currencyFormat = 'c0';
  protected numericFormat = 'n0';
  protected selectionSettings: SelectionSettingsModel = {
    type: 'Multiple',
    checkboxMode: 'ResetOnRowClick',
    allowColumnSelection: true
  };
  protected filterSettings: FilterSettingsModel = {type: 'Menu'};
  protected loadingIndicator: LoadingIndicatorModel = {indicatorType: 'Shimmer'};
  protected numericEdit = 'numericedit';
  protected datePickerEdit = 'datepickeredit';
  protected dateTimePicker = 'datetimepickeredit';
  protected textEdit = 'textedit';
  protected dropdownEdit = 'dropdownedit';
  get required() {
    return {
      required: [true, 'Trường này không được để trống']
    };
  }

  protected constructor() {
    MultiSelect.Inject(CheckBoxSelection, Edit, Filter, Page, Toolbar, Selection, ColumnChooser, Group, Print, ExcelExport, PdfExport);
  }

  protected abstract loadData(): void;

  protected abstract add(data: T): void;

  protected abstract update(data: T): void;

  protected abstract delete(data: T[]): void;
  protected saveChanges(args: DataSourceChangedEventArgs) {
    if (args.requestType === 'save') {
      if (args.action === 'add') {
        this.add(args.data as T);
      } else if (args.action === 'edit') {
        this.update(args.data as T);
      }
      args.endEdit;
    }

    if (args.requestType === 'delete') {
      this.delete(args.data as T[]);
      args.endEdit;
    }
  }

  protected handleError(args: any) {
    this._toast.error(args.error.error.message);
  }

  protected proceedObservable({
                                successHandler = (result: any): any => null,
                                errorHandler = (err: any): any => null,
                                completeHandler = (): any => null
                              }) {
    return {
      next: (result: any | undefined) => {
        if (successHandler) {
          successHandler(result);
        }
      },
      error: (err: any | undefined) => {
        if (errorHandler) {
          errorHandler(err);
        }
        this.loadData();
      },
      complete: () => {
        if (completeHandler) {
          completeHandler();
        }
      }
    }
  }

  protected async toolbarClick(args: any) {
    console.log(args.item.id);
    //grid_1225435927_0_print
    const action = args.item.id.split('_').pop();
    switch (action) {
      case 'excelexport':
        await this.grid?.excelExport();
        break;
      case 'csvexport':
        await this.grid?.csvExport();
        break;
    }
  }
}
