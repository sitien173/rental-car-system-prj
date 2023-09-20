import {PaymentMethodEnum, PaymentTypeEnum} from "./payment";

export interface CreatePayment {
  paymentDate: Date;
  amount: number;
  paymentMethod: PaymentMethodEnum;
  paymentType: PaymentTypeEnum;
  rentalContractId: string;
}
