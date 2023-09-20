import {Brand} from "../brands/brand";
import {Cartype} from "../cartype/cartype";
import {Feature} from "../features/feature";
import {AdditionalFees} from "../additional-fees/additional-fees";
import {RentalDocuments} from "../rental-documents/rental-documents";
import {Image} from "../common/image";

export enum CarStatusEnum {
  Available = "Available",
  Rented = "Rented",
  UnderMaintenance = "UnderMaintenance",
  Damaged = "Damaged",
  Cancelled = "Cancelled",
  Locked = "Locked",
  Unavailable = "Unavailable"
}

export const CarStatusEnumDescriptions: Record<CarStatusEnum, string> = {
  [CarStatusEnum.Available]: "Có sẵn",
  [CarStatusEnum.Rented]: "Đã cho thuê",
  [CarStatusEnum.UnderMaintenance]: "Đang bảo trì",
  [CarStatusEnum.Damaged]: "Đã hỏng",
  [CarStatusEnum.Cancelled]: "Đã hủy",
  [CarStatusEnum.Locked]: "Đã khóa",
  [CarStatusEnum.Unavailable]: "Không có sẵn"
}

export interface CarSpecificity {
  transmission: string;
  fuel: string;
  seat: number;
  fuelConsumption: string;
}

export interface Car {
  id: string;
  name: string;
  specificity: CarSpecificity;
  description: string;
  price: number;
  rule: string;
  status: CarStatusEnum;
  brand: Brand,
  carType: Cartype,
  features: Feature[],
  images: Image[];
  additionalFees: AdditionalFees[];
  rentalDocuments: RentalDocuments[];
}
