export enum RentalContractStatusEnum {
  Active = "Active",
  Completed = "Completed",
  Cancelled = "Cancelled",
}

export const RentalContractStatusEnumDescriptions: Record<RentalContractStatusEnum, string> = {
  [RentalContractStatusEnum.Active]: "Đang hoạt động",
  [RentalContractStatusEnum.Completed]: "Đã hoàn thành",
  [RentalContractStatusEnum.Cancelled]: "Đã hủy",
}

export interface RentalContract {
  id: string;
  rentalRequestId: string;
  cancellationDate?: Date | null;
  cancellationReason?: string | null;
  accidentDate?: Date | null;
  accidentDescription?: string | null;
  lateDate?: Date | null;
  lateReason?: string | null;
  amount: number;
  prepaidAmount: number;
  startDate: Date;
  endDate: Date;
  status: RentalContractStatusEnum;
  created: Date;
}
