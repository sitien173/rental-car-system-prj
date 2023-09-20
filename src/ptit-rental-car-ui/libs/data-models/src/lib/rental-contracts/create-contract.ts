export interface CreateRentalContract {
  rentalRequestId: string;
  amount: number;
  prepaidAmount: number;
  startDate: Date;
  endDate: Date;
}
