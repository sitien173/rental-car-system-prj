export enum PaymentMethodEnum {
    Cash = "Cash",
    CreditCard = "CreditCard", // "Thẻ tín dụng
    DebitCard = "DebitCard", // "Thẻ ghi nợ"
    PayPal = "PayPal",
    BankTransfer = "BankTransfer", // "Chuyển khoản ngân hàng"
}

export const PaymentMethodEnumDescriptions: Record<PaymentMethodEnum, string> = {
    [PaymentMethodEnum.Cash]: "Tiền mặt",
    [PaymentMethodEnum.CreditCard]: "Thẻ tín dụng",
    [PaymentMethodEnum.DebitCard]: "Thẻ ghi nợ",
    [PaymentMethodEnum.PayPal]: "PayPal",
    [PaymentMethodEnum.BankTransfer]: "Chuyển khoản ngân hàng",
}

export enum PaymentStatusEnum {
    Pending = "Pending", // "Đang chờ"
    Completed = "Completed", // "Thành công"
    Failed = "Failed", // "Thất bại"
}

export const PaymentStatusEnumDescriptions: Record<PaymentStatusEnum, string> = {
    [PaymentStatusEnum.Pending]: "Đang chờ",
    [PaymentStatusEnum.Completed]: "Thành công",
    [PaymentStatusEnum.Failed]: "Thất bại",
}

export enum PaymentTypeEnum {
    Prepaid = "Prepaid", // "Trả trước
    Postpaid = "Postpaid", // "Trả sau"
    Refund = "Refund", // "Hoàn tiền"
    LateFee = "LateFee", // "Phí trễ hạn"
    Accident = "Accident", // "Tai nạn"
}

export const PaymentTypeEnumDescriptions: Record<PaymentTypeEnum, string> = {
    [PaymentTypeEnum.Prepaid]: "Trả trước",
    [PaymentTypeEnum.Postpaid]: "Trả sau",
    [PaymentTypeEnum.Refund]: "Hoàn tiền",
    [PaymentTypeEnum.LateFee]: "Phí trễ hạn",
    [PaymentTypeEnum.Accident]: "Tai nạn",
}

export interface Payment {
    id: string;
    paymentDate: Date;
    amount: number;
    paymentMethod: PaymentMethodEnum;
    status: PaymentStatusEnum;
    paymentType: PaymentTypeEnum;
    rentalContractId: string;
}
