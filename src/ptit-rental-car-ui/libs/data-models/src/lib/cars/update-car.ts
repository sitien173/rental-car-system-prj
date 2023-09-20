import {CarSpecificity} from "./car";

export interface UpdateCar {
  name?: string;
  specificity?: CarSpecificity;
  description?: string;
  brandId?: string;
  carTypeId?: string;
  price?: number;
  rule?: string;
  featureIds?: string[];
  additionalFeeIds?: string[];
  rentalDocumentIds?: string[];
}
