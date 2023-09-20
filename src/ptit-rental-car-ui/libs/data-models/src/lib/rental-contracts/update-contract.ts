import {RentalContractStatusEnum} from "./rental-contract";

export interface UpdateRentalContract {
  cancellationDate?: Date | null;
  cancellationReason?: string | null;
  accidentDate?: Date | null;
  accidentDescription?: string | null;
  lateDate?: Date | null;
  lateReason?: string | null;
  status?: RentalContractStatusEnum;
}
