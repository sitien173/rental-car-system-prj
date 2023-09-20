import {CheckListItemStatusEnum, HandoverTypeEnum} from "./vehicle-handover";

export interface UpdateVehicleHandover {
  handoverType?: HandoverTypeEnum;
  rentalContractId?: string;
  checkListItems?: {
    name: string;
    status: CheckListItemStatusEnum;
    comment: string;
  }[];
}
