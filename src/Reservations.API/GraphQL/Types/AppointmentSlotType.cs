using Reservations.API.Models;

namespace Reservations.API.GraphQL.Types;

public class AppointmentSlotType : ObjectType<AppointmentSlotModel>
{
    protected void Configure(IObjectTypeDescriptor<AppointmentSlotModel> descriptor)
    {
        descriptor.Name("AppointmentSlot");
        descriptor.Field(_ => _.Id).Name("id").Type<NonNullType<IdType>>();
        descriptor.Field(_ => _.StartTime).Name("startTime").Type<NonNullType<DateTimeType>>();
        descriptor.Field(_ => _.EndTime).Name("endTime").Type<NonNullType<DateTimeType>>();
        descriptor.Field(_ => _.ProviderId).Name("providerId").Type<NonNullType<IdType>>();
        descriptor.Field(_ => _.Provider).Name("provider").Type<ListType<ProviderType>>();
        descriptor.Field(_ => _.Status).Name("status").Type<AppointmentStatusType>();
    }
}