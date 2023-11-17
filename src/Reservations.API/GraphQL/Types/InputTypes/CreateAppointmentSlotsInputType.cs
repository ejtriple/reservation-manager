using Reservations.API.Models;

namespace Reservations.API.InputTypes;

public class CreateAppointmentSlotsInputType : InputObjectType<AppointmentSlotInput>
{
    protected override void Configure(IInputObjectTypeDescriptor<AppointmentSlotInput> descriptor)
    {
        descriptor
            .BindFieldsExplicitly()
            .Name("CreateAppointmentInput")
            .Description("The input type for creating appointments");
        descriptor.Field(t => t.UserName).Type<NonNullType<StringType>>();
        descriptor.Field(t => t.StartTime).Type<NonNullType<DateTimeType>>();
        descriptor.Field(t => t.EndTime).Type<NonNullType<DateTimeType>>();
    }
}