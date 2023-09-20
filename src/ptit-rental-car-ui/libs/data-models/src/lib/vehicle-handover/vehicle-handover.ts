export enum HandoverTypeEnum {
  Delivery= "Delivery",
  Return = "Return",
}

export const HandoverTypeEnumDescriptions: Record<HandoverTypeEnum, string> = {
  [HandoverTypeEnum.Delivery]: "Giao xe",
  [HandoverTypeEnum.Return]: "Nhận xe",
}

export enum CheckListItemStatusEnum {
  Ok = "Ok",
  Repair = "Repair",
}

export const CheckListItemStatusEnumDescriptions: Record<CheckListItemStatusEnum, string> = {
  [CheckListItemStatusEnum.Ok]: "Đạt",
  [CheckListItemStatusEnum.Repair]: "Sửa chữa",
}

export interface CheckListItem {
  id: string;
  name: string;
  status: CheckListItemStatusEnum;
  comment: string;
  vehicleHandoverId: string;
}

export interface VehicleHandover {
  id: string;
  handoverDate: Date;
  handoverBy: string;
  handoverType: HandoverTypeEnum;
  rentalContractId: string;
  checkListItems: CheckListItem[];
}
