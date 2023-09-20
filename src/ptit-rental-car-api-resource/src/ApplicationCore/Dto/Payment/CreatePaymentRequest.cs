using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NGOT.Common.Enums;
using NGOT.Common.Interfaces;

namespace NGOT.ApplicationCore.Dto.Payment;

public record CreatePaymentRequest(
        DateTime PaymentDate,
        decimal Amount,
        [property: JsonConverter(typeof(StringEnumConverter))]
        PaymentMethodEnum PaymentMethod,
        [property: JsonConverter(typeof(StringEnumConverter))]
        PaymentTypeEnum PaymentType,
        string RentalContractId
    )
    : ISimpleMap<CreatePaymentRequest, Entities.Payment>;