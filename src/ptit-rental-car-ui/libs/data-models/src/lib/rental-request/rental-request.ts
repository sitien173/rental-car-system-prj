export enum RentalRequestEnum {
  Pending = "Pending",
  Approved = "Approved",
  Active = "Active",
  Rejected = "Rejected",
  Canceled = "Canceled",
  Completed = "Completed"
}

export const RentalRequestEnumDescriptions: Record<RentalRequestEnum, string> = {
  [RentalRequestEnum.Pending]: "Đang chờ duyệt",
  [RentalRequestEnum.Approved]: "Đã được duyệt",
  [RentalRequestEnum.Active]: "Đang thuê",
  [RentalRequestEnum.Rejected]: "Đã bị từ chối",
  [RentalRequestEnum.Canceled]: "Đã hủy",
  [RentalRequestEnum.Completed]: "Đã hoàn thành"
};

export interface RentalRequest {
  id: string;
  userId: string;
  carId: string;
  startDate: Date;
  endDate: Date;
  status: RentalRequestEnum;
}
