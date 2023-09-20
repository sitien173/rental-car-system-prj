import {CarSpecificity} from "./car";
import {Image} from "../common/image";

export interface CreateCar {
  name: string;
  specificity: CarSpecificity;
  description: string;
  brandId?: string;
  carTypeId?: string;
  price: number;
  rule: string;
  featureIds?: string[];
  additionalFeeIds?: string[];
  rentalDocumentIds?: string[];
  images?: Image[];
}
