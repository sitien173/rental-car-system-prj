using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NGOT.Common.Enums;
using NGOT.Common.Interfaces;

namespace NGOT.ApplicationCore.Dto.Payment;

public record PaymentResponse(Guid Id,
        DateTime PaymentDate,
        decimal Amount,
        [property: JsonConverter(typeof(StringEnumConverter))]
        PaymentMethodEnum PaymentMethod,
        [property: JsonConverter(typeof(StringEnumConverter))]
        PaymentStatusEnum Status,
        [property: JsonConverter(typeof(StringEnumConverter))]
        PaymentTypeEnum PaymentType,
        string RentalContractId)
    : ISimpleMap<Entities.Payment, PaymentResponse>;