namespace Reservations.API.GraphQL.Types;

public class MutationType : ObjectType<Mutation>
{
    protected override void Configure(IObjectTypeDescriptor<Mutation> descriptor)
    {
        // descriptor
        //     .Name("Query")
        //     .Field("me")
        //     .Type<NonNullType<UserType>>()
        //     .Resolve(ctx => ctx.Service<UserRepository>().GetUserByIdAsync("1"));

        //Appointments
        descriptor
            .Field(f => f.CreateProviderAppointments(default, default, default))
            .Name("createProviderAppointments")
            .Description("Creates a set of 15 minute interval appointments between the provided start and end date")
            .Type<NonNullType<ListType<AppointmentSlotType>>>();

        descriptor
            .Field(f => f.ReserveAppointment(default, default))
            .Name("reserveAppointment")
            .Description("Reserve an appointment slot")
            .Type<NonNullType<AppointmentSlotType>>();

        descriptor
            .Field(f => f.ConfirmAppointment(default, default))
            .Name("confirmAppointment")
            .Description("Confirm an appointment slot")
            .Type<NonNullType<AppointmentSlotType>>();

        descriptor
            .Field(f => f.ExpireAppointmentSlot())
            .Name("expireAppointments")
            .Description("Clear the status of all expired appointments")
            .Type<NonNullType<ListType<AppointmentSlotType>>>();
    }
}